import React, { useEffect, useState } from "react";
import axios from "axios";
import {
  Box,
  Typography,
  Grid,
  Card,
  CardContent,
  CardActions,
  Button,
  Chip,
  CircularProgress,
  Tooltip,
  Stack,
  Modal,
  IconButton,
  Snackbar,
  Alert,
} from "@mui/material";
import {
  Folder,
  Person,
  BugReport,
  Visibility,
  Close,
} from "@mui/icons-material";
import CreateProjectForm from "./CreateProjectForm";
import ProjectMembersPopover from "./ProjectMembersPopover";
import ProjectDetailsModal from "./ProjectDetailsModal";

// Tipe data project
interface Project {
  id: string;
  name: string;
  description: string;
  projectCode: string;
  repositoryUrl: string;
  documentationUrl: string;
  status: string;
  visibility: string;
  createdAt: string;
  updatedAt: string | null;
  ownerId: string;
  ownerName: string;
  ownerEmail: string;
  ownerProfilePictureUrl: string;
  memberCount: number;
  issueCount: number;
  members: string[];
  issues: string[];
}

const MyProjects: React.FC = () => {
  const [projects, setProjects] = useState<Project[]>([]);
  const [loading, setLoading] = useState(true);
  const [openModal, setOpenModal] = useState(false);
  const [successSnackbar, setSuccessSnackbar] = useState(false);
  const [selectedProjectCode, setSelectedProjectCode] = useState<string | null>(null);
  const [detailsModalOpen, setDetailsModalOpen] = useState(false);

  const fetchProjects = async () => {
    try {
      const response = await axios.get("https://localhost:5001/api/Project/mine", {
        withCredentials: true,
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
      });
      setProjects(response.data);
    } catch (error) {
      console.error("Gagal mengambil project:", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchProjects();
  }, []);

  const handleModalClose = () => {
    setOpenModal(false);
  };

  const handleSuccessCreate = () => {
    handleModalClose();
    fetchProjects();
    setSuccessSnackbar(true);
  };

  const handleOpenDetails = (projectCode: string) => {
    setSelectedProjectCode(projectCode);
    setDetailsModalOpen(true);
  };

  const handleCloseDetails = () => {
    setSelectedProjectCode(null);
    setDetailsModalOpen(false);
  };

  return (
    <Box p={4}>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
        <Typography variant="h5" fontWeight="bold">
          My Projects
        </Typography>
        <Button variant="contained" onClick={() => setOpenModal(true)}>
          + New Project
        </Button>
      </Box>

      {loading ? (
        <Box display="flex" justifyContent="center" alignItems="center" height="200px">
          <CircularProgress />
          <Typography ml={2}>Memuat data proyek...</Typography>
        </Box>
      ) : projects.length === 0 ? (
        <Typography>Tidak ada proyek.</Typography>
      ) : (
        <Grid container spacing={3}>
          {projects.map((project) => (
            <Grid item xs={12} sm={6} md={4} key={project.id}>
              <Card
                variant="outlined"
                sx={{ height: "100%", display: "flex", flexDirection: "column" }}
              >
                <CardContent>
                  <Stack direction="row" alignItems="center" justifyContent="space-between">
                    <Typography variant="h6" gutterBottom noWrap>
                      {project.name}
                    </Typography>
                    <Tooltip title={project.visibility}>
                      <Visibility fontSize="small" />
                    </Tooltip>
                  </Stack>

                  <Typography variant="body2" color="text.secondary" gutterBottom>
                    {project.description || "No description."}
                  </Typography>

                  <Chip
                    icon={<Folder />}
                    label={project.projectCode}
                    size="small"
                    sx={{ mb: 1 }}
                  />

                  <Typography variant="caption" color="text.secondary">
                    Created: {new Date(project.createdAt).toLocaleDateString()}
                  </Typography>

                  <Stack direction="row" spacing={1} mt={2}>
                    <Chip icon={<Person />} label={`${project.memberCount} Members`} size="small" />
                    <Chip icon={<BugReport />} label={`${project.issueCount} Issues`} size="small" />
                  </Stack>
                </CardContent>

                <CardActions sx={{ mt: "auto", justifyContent: "space-between", px: 2 }}>
                  <Button
                    size="small"
                    onClick={() => handleOpenDetails(project.projectCode)}
                  >
                    View Project
                  </Button>

                  <Stack direction="row" spacing={1}>
                    <ProjectMembersPopover projectCode={project.projectCode} />
                    {project.repositoryUrl && (
                      <Button
                        size="small"
                        href={project.repositoryUrl}
                        target="_blank"
                        rel="noopener noreferrer"
                      >
                        GitHub
                      </Button>
                    )}
                  </Stack>
                </CardActions>
              </Card>
            </Grid>
          ))}
        </Grid>
      )}

      {/* Modal Create Project */}
      <Modal open={openModal} onClose={handleModalClose}>
        <Box
          sx={{
            maxWidth: 600,
            bgcolor: "background.paper",
            p: 4,
            mx: "auto",
            mt: "5%",
            borderRadius: 2,
            boxShadow: 24,
            position: "relative",
          }}
        >
          <IconButton
            onClick={handleModalClose}
            sx={{ position: "absolute", top: 10, right: 10 }}
          >
            <Close />
          </IconButton>

          <CreateProjectForm onSuccess={handleSuccessCreate} />
        </Box>
      </Modal>

      {/* Snackbar di luar modal */}
      <Snackbar
        open={successSnackbar}
        autoHideDuration={4000}
        onClose={() => setSuccessSnackbar(false)}
        anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
      >
        <Alert
          onClose={() => setSuccessSnackbar(false)}
          severity="success"
          sx={{ width: "100%" }}
        >
          Project berhasil dibuat!
        </Alert>
      </Snackbar>

      {/* Modal Detail Project */}
      {selectedProjectCode && (
        <ProjectDetailsModal
          open={detailsModalOpen}
          onClose={handleCloseDetails}
          projectCode={selectedProjectCode}
        />
      )}
    </Box>
  );
};

export default MyProjects;
