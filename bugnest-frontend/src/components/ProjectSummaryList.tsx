import React, { useEffect, useState } from "react";
import {
  Box,
  CircularProgress,
  Typography,
  List,
  ListItem,
  ListItemText,
  Tooltip,
  Popover,
  Avatar,
  Divider,
} from "@mui/material";
import axios from "axios";

type Issue = {
  id: string;
  title: string;
  status: string;
  priority: string;
  createdAt: string;
};

type ProjectSummary = {
  projectCode: string;
  totalIssues: number;
  issueStatusCounts: Record<string, number>;
  memberCount: number;
  lastActivityAt: string | null;
  progressPercentage: number;
};

type ProjectDetail = {
  projectCode: string;
  name: string;
  description: string;
  repositoryUrl: string;
  documentationUrl: string;
  status: string;
  visibility: string;
  createdAt: string;
  updatedAt: string | null;
  totalIssues: number;
  issueStatusCounts: Record<string, number>;
  progressPercentage: number;
  recentIssues: Issue[];
  members: {
    nrp: string;
    username: string;
    fullName: string;
    profilePictureUrl: string;
  }[];
  owner: {
    nrp: string;
    username: string;
    fullName: string;
    profilePictureUrl: string;
  };
};

const statusColors: Record<string, string> = {
  Open: "#9e9e9e",
  InProgress: "#2196f3",
  Review: "#ffc107",
  Resolved: "#00bcd4",
  Closed: "#4caf50",
  Reopened: "#f44336",
};

const statusOrder = ["Open", "InProgress", "Review", "Resolved", "Closed", "Reopened"];

const ProjectSummaryList: React.FC = () => {
  const [summaries, setSummaries] = useState<ProjectSummary[]>([]);
  const [loading, setLoading] = useState(true);
  const [anchorEl, setAnchorEl] = useState<HTMLElement | null>(null);
  const [selectedProject, setSelectedProject] = useState<ProjectDetail | null>(null);

  useEffect(() => {
    const fetchSummary = async () => {
      try {
        const response = await axios.get("https://localhost:5001/api/Project/summaries", {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
          withCredentials: true,
        });
        setSummaries(response.data);
      } catch (error) {
        console.error("Gagal mengambil ringkasan proyek:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchSummary();
  }, []);

  const handleProjectClick = async (event: React.MouseEvent<HTMLElement>, code: string) => {
    setAnchorEl(event.currentTarget);
    try {
      const response = await axios.get(`https://localhost:5001/api/Project/${code}/details`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
        withCredentials: true,
      });
      setSelectedProject(response.data);
    } catch (error) {
      console.error("Gagal mengambil detail proyek:", error);
    }
  };

  const handlePopoverClose = () => {
    setAnchorEl(null);
    setSelectedProject(null);
  };

  if (loading) {
    return (
      <Box display="flex" justifyContent="center" alignItems="center" mt={2}>
        <CircularProgress size={24} />
      </Box>
    );
  }

  return (
    <Box>
      <Typography variant="h6" gutterBottom>
        📊 Ringkasan Proyek
      </Typography>
      <List dense disablePadding>
        {summaries.slice(0, 2).map((project) => (
          <ListItem
            key={project.projectCode}
            onClick={(e) => handleProjectClick(e, project.projectCode)}
            sx={{
              display: "flex",
              flexDirection: "column",
              alignItems: "flex-start",
              p: 1,
              mb: 1,
              borderRadius: 1,
              cursor: "pointer",
              backgroundColor: "#0d1117",
              "&:hover": {
                backgroundColor: "#1f2937",
              },
            }}
          >
            <ListItemText
              primary={
                <Typography sx={{ fontSize: "0.85rem", fontWeight: 600, color: "#c9d1d9" }}>
                  {project.projectCode}
                </Typography>
              }
              secondary={
                <Typography sx={{ fontSize: "0.7rem", color: "gray" }}>
                  {project.totalIssues} isu • {project.memberCount} anggota
                </Typography>
              }
            />
            <Box
              sx={{
                width: "100%",
                mt: 1,
                height: 8,
                borderRadius: 4,
                display: "flex",
                overflow: "hidden",
                backgroundColor: "#2d333b",
              }}
            >
              {statusOrder.map((status) => {
                const count = project.issueStatusCounts[status] || 0;
                const color = statusColors[status] || "#888";
                const percent = project.totalIssues > 0 ? (count / project.totalIssues) * 100 : 0;
                return count > 0 ? (
                  <Tooltip key={status} title={`${status}: ${count}`}>
                    <Box
                      sx={{
                        width: `${percent}%`,
                        backgroundColor: color,
                        height: "100%",
                      }}
                    />
                  </Tooltip>
                ) : null;
              })}
            </Box>
            <Typography variant="caption" sx={{ fontSize: "0.65rem", color: "gray", mt: 0.5 }}>
              Progres: {Math.round(project.progressPercentage)}%
            </Typography>
          </ListItem>
        ))}
      </List>

      <Popover
        open={Boolean(anchorEl) && Boolean(selectedProject)}
        anchorEl={anchorEl}
        onClose={handlePopoverClose}
        anchorOrigin={{ vertical: "bottom", horizontal: "left" }}
        PaperProps={{
          sx: { p: 2, maxWidth: 400, backgroundColor: "#0d1117", color: "#fff" },
        }}
      >
        {selectedProject ? (
          <Box>
            <Typography variant="h6">{selectedProject.name}</Typography>
            <Typography variant="body2" gutterBottom>
              {selectedProject.description}
            </Typography>

            <Divider sx={{ my: 1, borderColor: "#333" }} />

            <Typography variant="caption" sx={{ display: "block" }}>
              Status: {selectedProject.status} • Visibility: {selectedProject.visibility}
            </Typography>
            <Typography variant="caption" sx={{ display: "block", mt: 0.5 }}>
              Dibuat: {new Date(selectedProject.createdAt).toLocaleString()}
            </Typography>
            <Divider sx={{ my: 1, borderColor: "#333" }} />

            {selectedProject.recentIssues?.length > 0 && (
              <Box>
                <Typography variant="subtitle2">🧩 Isu Terbaru:</Typography>
                <List dense disablePadding>
                  {selectedProject.recentIssues.map((issue) => (
                    <ListItem key={issue.id} sx={{ px: 0 }}>
                      <ListItemText
                        primary={
                          <Typography fontSize="0.75rem" fontWeight={600}>
                            {issue.title}
                          </Typography>
                        }
                        secondary={
                          <Typography fontSize="0.7rem" color="gray">
                            {issue.status} • {issue.priority}
                          </Typography>
                        }
                      />
                    </ListItem>
                  ))}
                </List>
              </Box>
            )}

            <Divider sx={{ my: 1, borderColor: "#333" }} />

            <Typography variant="subtitle2" gutterBottom>
              👤 Pemilik Proyek:
            </Typography>
            <Box display="flex" alignItems="center" gap={1}>
              <Avatar
                src={selectedProject.owner.profilePictureUrl}
                sx={{ width: 24, height: 24 }}
              />
              <Typography variant="body2" fontSize={12}>
                {selectedProject.owner.fullName} ({selectedProject.owner.username})
              </Typography>
            </Box>
          </Box>
        ) : (
          <Typography variant="body2">Memuat...</Typography>
        )}
      </Popover>
    </Box>
  );
};

export default ProjectSummaryList;
