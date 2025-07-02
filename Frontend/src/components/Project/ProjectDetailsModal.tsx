import React, { useEffect, useState } from "react";
import {
  Modal,
  Box,
  Typography,
  CircularProgress,
  Divider,
  Avatar,
  Stack,
  Chip,
} from "@mui/material";
import axios from "axios";
import ProjectSummaryCard from "./ProjectSummaryCard ";

interface ProjectDetailsModalProps {
  open: boolean;
  onClose: () => void;
  projectCode: string;
}

interface Member {
  nrp: string;
  username: string;
  fullName: string;
  profilePictureUrl: string;
}

interface AssignedUser {
  userId: string;
  nrp: string;
  username: string;
  fullName: string;
  email: string;
  role: string;
  position: string | null;
  department: string | null;
  profilePictureUrl: string;
}

interface Issue {
  id: string;
  issueCode: string;
  title: string;
  status: string;
  priority: string;
  projectCode: string | null;
  description: string | null;
  createdAt: string;
  deadline: string | null;
  estimatedFixHours: number | null;
  creator: Member | null;
  tags: string[];
  assignedUsers: AssignedUser[];
}

interface ProjectDetails {
  projectCode: string;
  name: string;
  description: string;
  repositoryUrl: string;
  documentationUrl: string;
  status: string;
  visibility: string;
  createdAt: string;
  updatedAt: string | null;
  owner: Member;
  members: Member[];
  totalIssues: number;
  issueStatusCounts: Record<string, number>;
  progressPercentage: number;
  recentIssueSummaries: string[];
  recentIssues: Issue[];
  lastActivityAt?: string | null;
}

const statusColors: Record<string, string> = {
  Open: "#9e9e9e",
  InProgress: "#2196f3",
  Review: "#ffc107",
  Resolved: "#00bcd4",
  Closed: "#4caf50",
  Reopened: "#f44336",
};

const ProjectDetailsModal: React.FC<ProjectDetailsModalProps> = ({ open, onClose, projectCode }) => {
  const [loading, setLoading] = useState(true);
  const [details, setDetails] = useState<ProjectDetails | null>(null);

  useEffect(() => {
    if (open) {
      fetchDetails();
    }
  }, [open]);

  const fetchDetails = async () => {
    setLoading(true);
    try {
      const res = await axios.get(`https://localhost:5001/api/Project/${projectCode}/details`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
      });
      setDetails(res.data);
    } catch (err) {
      console.error("Gagal mengambil detail proyek", err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <Modal open={open} onClose={onClose}>
      <Box
        sx={{
          maxWidth: 700,
          maxHeight: "90vh",
          overflowY: "auto",
          bgcolor: "background.paper",
          p: 4,
          mx: "auto",
          mt: "5%",
          borderRadius: 2,
          boxShadow: 24,
        }}
      >
        {loading || !details ? (
          <Stack direction="row" spacing={2} alignItems="center">
            <CircularProgress size={20} />
            <Typography>Memuat detail proyek...</Typography>
          </Stack>
        ) : (
          <>
            <Typography variant="h6" gutterBottom>
              {details.name} ({details.projectCode})
            </Typography>
            <Typography variant="body2" gutterBottom>
              {details.description}
            </Typography>

            <Divider sx={{ my: 2 }} />

            <Typography variant="subtitle2" gutterBottom>
              Owner
            </Typography>
            <Stack direction="row" spacing={2} alignItems="center" mb={2}>
              <Avatar src={details.owner.profilePictureUrl} />
              <Typography>{details.owner.fullName} ({details.owner.username})</Typography>
            </Stack>

            <Typography variant="subtitle2" gutterBottom>
              Member
            </Typography>
            <Stack direction="row" spacing={1} flexWrap="wrap" mb={2}>
              {details.members.map((member) => (
                <Chip key={member.nrp} label={member.fullName} />
              ))}
            </Stack>

            <ProjectSummaryCard
              summary={{
                projectCode: details.projectCode,
                totalIssues: details.totalIssues,
                issueStatusCounts: details.issueStatusCounts,
                memberCount: details.members.length,
                progressPercentage: details.progressPercentage,
                lastActivityAt: details.lastActivityAt || null,
              }}
              statusColors={statusColors}
            />

            {details.recentIssues?.length > 0 && (
              <Box mt={3}>
                <Typography variant="subtitle2" gutterBottom>
                  Detail Issue Terbaru
                </Typography>
                {details.recentIssues.map((issue) => (
                  <Box key={issue.id} mb={2} p={2} sx={{ border: "1px solid #ccc", borderRadius: 1 }}>
                    <Typography variant="body1" fontWeight="bold">
                      {issue.title}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                      Status: <span style={{ color: statusColors[issue.status] || "#000" }}>{issue.status}</span> | Priority: {issue.priority}
                    </Typography>
                    <Typography variant="body2">Dibuat: {new Date(issue.createdAt).toLocaleString()}</Typography>
                  </Box>
                ))}
              </Box>
            )}
          </>
        )}
      </Box>
    </Modal>
  );
};

export default ProjectDetailsModal;
