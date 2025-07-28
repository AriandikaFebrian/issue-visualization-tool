import React, { useEffect, useState } from "react";
import axios from "axios";
import {
  Box,
  Typography,
  Button,
  Chip,
  CircularProgress,
  Tooltip,
  Stack,
  Modal,
  IconButton,
  Snackbar,
  Alert,
  Tabs,
  Tab,
  Divider,
} from "@mui/material";
import {
  Folder,
  Person,
  BugReport,
  Visibility,
  Lock,
  LockOpen,
  Close,
} from "@mui/icons-material";
import { useLocation, useNavigate } from "react-router-dom";

import CreateProjectForm from "./CreateProjectForm";
import ProjectMembersPopover from "./ProjectMembersPopover";
import ProjectDetailsModal from "./ProjectDetailsModal";

const projectStatusTabs = [
  "Planning",
  "InProgress",
  "Completed",
  "OnHold",
  "Archived",
];

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
  const [activeTab, setActiveTab] = useState("Planning");

  const location = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchProjects = async () => {
      try {
        const response = await axios.get("https://localhost:5001/api/Project/mine", {
          headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
        });
        setProjects(response.data);
      } catch (error) {
        console.error("Gagal mengambil project:", error);
      } finally {
        setLoading(false);
      }
    };
    fetchProjects();
  }, []);

  useEffect(() => {
    const params = new URLSearchParams(location.search);
    if (params.get("create") === "true") {
      setOpenModal(true);
    }
  }, [location.search]);

  const handleModalClose = () => {
    setOpenModal(false);
    const params = new URLSearchParams(location.search);
    if (params.has("create")) {
      params.delete("create");
      navigate({ pathname: location.pathname, search: params.toString() }, { replace: true });
    }
  };

  const handleSuccessCreate = () => {
    setSuccessSnackbar(true);
    setLoading(true);
    axios
      .get("https://localhost:5001/api/Project/mine", {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      })
      .then((res) => setProjects(res.data))
      .finally(() => setLoading(false));
  };

  const handleOpenDetails = (projectCode: string) => {
    setSelectedProjectCode(projectCode);
    setDetailsModalOpen(true);
  };

  const handleCloseDetails = () => {
    setSelectedProjectCode(null);
    setDetailsModalOpen(false);
  };

  const filteredProjects = projects.filter((p) => p.status === activeTab);

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

      <Tabs
        value={activeTab}
        onChange={(e, newValue) => setActiveTab(newValue)}
        textColor="primary"
        indicatorColor="primary"
        sx={{ mb: 3 }}
      >
        {projectStatusTabs.map((status) => (
          <Tab key={status} label={status} value={status} />
        ))}
      </Tabs>

      {loading ? (
        <Box display="flex" alignItems="center" justifyContent="center" height="200px">
          <CircularProgress />
          <Typography ml={2}>Memuat proyek...</Typography>
        </Box>
      ) : filteredProjects.length === 0 ? (
        <Typography>Tidak ada proyek dengan status {activeTab}.</Typography>
      ) : (
        <Box>
          {filteredProjects.map((project, index) => (
            <Box key={project.id} py={2}>
              <Stack direction="row" spacing={2} alignItems="center" flexWrap="wrap">
  <Folder color="action" />
  <Typography fontWeight="bold" sx={{ flexGrow: 1 }}>
    {project.name}
  </Typography>
  <Tooltip title={project.visibility}>
    {project.visibility === "Private" ? (
      <Lock fontSize="small" />
    ) : (
      <LockOpen fontSize="small" />
    )}
  </Tooltip>
<ProjectMembersPopover
  projectCode={project.projectCode}
  asChip
  memberCount={project.memberCount}
/>

  <Chip icon={<BugReport />} label={`${project.issueCount}`} size="small" />
</Stack>


              <Typography variant="body2" color="text.secondary" mt={0.5}>
                {project.description || "No description."}
              </Typography>

              <Stack direction="row" spacing={1} mt={1}>
                <Button
                  size="small"
                  variant="outlined"
                  onClick={() => handleOpenDetails(project.projectCode)}
                >
                  View
                </Button>
                {project.repositoryUrl && (
                  <Button
                    size="small"
                    variant="outlined"
                    href={project.repositoryUrl}
                    target="_blank"
                    rel="noopener noreferrer"
                  >
                    GitHub
                  </Button>
                )}
              </Stack>

              {index < filteredProjects.length - 1 && <Divider sx={{ mt: 2 }} />}
            </Box>
          ))}
        </Box>
      )}

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
          <IconButton onClick={handleModalClose} sx={{ position: "absolute", top: 10, right: 10 }}>
            <Close />
          </IconButton>

          <CreateProjectForm onSuccess={handleSuccessCreate} />
        </Box>
      </Modal>

      <Snackbar
        open={successSnackbar}
        autoHideDuration={4000}
        onClose={() => setSuccessSnackbar(false)}
        anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
      >
        <Alert onClose={() => setSuccessSnackbar(false)} severity="success" sx={{ width: "100%" }}>
          Project berhasil dibuat!
        </Alert>
      </Snackbar>

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
