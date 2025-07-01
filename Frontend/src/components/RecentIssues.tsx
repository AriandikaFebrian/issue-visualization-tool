import React, { useEffect, useState } from "react";
import {
  Box,
  Typography,
  List,
  ListItem,
  Chip,
  CircularProgress,
  IconButton,
  Tooltip,
} from "@mui/material";
import VisibilityIcon from "@mui/icons-material/Visibility";
import axios from "axios";
import dayjs from "dayjs";

const statusColors: Record<string, "default" | "info" | "warning" | "success" | "error" | "primary" | "secondary"> = {
  Open: "default",
  InProgress: "info",
  Review: "warning",
  Resolved: "success",
  Closed: "success",
  Reopened: "error",
};

type Issue = {
  issueCode: string;
  title: string;
  projectCode: string;
  status: string;
  createdAt: string;
};

const RecentIssues: React.FC = () => {
  const [issues, setIssues] = useState<Issue[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchRecentIssues = async () => {
      try {
        const response = await axios.get("https://localhost:5001/api/Issue/recent?count=5", {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
          withCredentials: true,
        });
        setIssues(response.data);
      } catch (error) {
        console.error("Gagal memuat isu terbaru:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchRecentIssues();
  }, []);

  if (loading) {
    return (
      <Box display="flex" justifyContent="center" mt={1}>
        <CircularProgress size={20} />
      </Box>
    );
  }

  return (
  <Box
    mt={1}
    p={2}
    borderRadius={2}
    bgcolor="#161b22"
    border="1px solid #30363d"
  >
    <Box display="flex" justifyContent="space-between" alignItems="center" mb={1}>
      <Tooltip title="Lihat semua isu">
        <IconButton size="small" href="/issues" sx={{ color: "lightgray" }}>
          <VisibilityIcon fontSize="small" />
        </IconButton>
      </Tooltip>
    </Box>

    <List dense sx={{ p: 0 }}>
      {issues.length === 0 ? (
        <Typography variant="body2" color="text.secondary">
          Tidak ada isu terbaru.
        </Typography>
      ) : (
        issues.map((issue) => (
          <ListItem
            key={issue.issueCode}
            sx={{
              flexDirection: "column",
              alignItems: "flex-start",
              borderRadius: 1,
              backgroundColor: "#0d1117",
              mb: 1,
              px: 1,
              py: 1,
            }}
          >
            <Typography fontSize="0.7rem" color="gray" fontWeight={500}>
              Proyek: {issue.projectCode}
            </Typography>

            <Box display="flex" justifyContent="space-between" width="100%" alignItems="flex-start">
              <Box>
                <Typography
                  fontSize="0.7rem"
                  fontWeight={600}
                  sx={{ lineHeight: 1.2 }}
                >
                </Typography>
                <Typography
                  fontSize="0.75rem"
                  fontWeight={500}
                  sx={{
                    whiteSpace: "nowrap",
                    overflow: "hidden",
                    textOverflow: "ellipsis",
                    maxWidth: "180px",
                  }}
                  title={issue.title}
                >
                  {issue.title}
                </Typography>
              </Box>
              <Chip
                label={issue.status}
                size="small"
                color={statusColors[issue.status] || "default"}
                variant="outlined"
                sx={{ ml: 1 }}
              />
            </Box>

            <Typography fontSize="0.65rem" color="gray" mt={0.5}>
              {dayjs(issue.createdAt).format("DD MMM YYYY HH:mm")}
            </Typography>
          </ListItem>
        ))
      )}
    </List>
  </Box>
);

};

export default RecentIssues;
