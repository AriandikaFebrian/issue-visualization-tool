import React from "react";
import {
  Box,
  Typography,
  LinearProgress,
  Stack,
  Chip,
  Paper,
  Divider,
} from "@mui/material";
import GroupIcon from "@mui/icons-material/Group";
import BugReportIcon from "@mui/icons-material/BugReport";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import HourglassEmptyIcon from "@mui/icons-material/HourglassEmpty";

interface ProjectSummary {
  projectCode: string;
  totalIssues: number;
  issueStatusCounts: Record<string, number>;
  memberCount: number;
  lastActivityAt: string | null;
  progressPercentage: number;
}

interface Props {
  summary: ProjectSummary;
}

const getProgressColor = (percentage: number): string => {
  if (percentage >= 75) return "#4caf50"; // Green
  if (percentage >= 40) return "#ff9800"; // Orange
  return "#f44336"; // Red
};

const getProgressLabel = (percentage: number): string => {
  if (percentage >= 75) return "Selesai";
  if (percentage >= 40) return "Sedang Berjalan";
  return "Belum Selesai";
};

const ProjectSummaryCard: React.FC<Props> = ({ summary }) => {
  const progressColor = getProgressColor(summary.progressPercentage);
  const progressLabel = getProgressLabel(summary.progressPercentage);

  return (
    <Paper elevation={2} sx={{ p: 3, borderRadius: 2 }}>
      <Typography variant="h6" gutterBottom>
        Ringkasan Proyek ({summary.projectCode})
      </Typography>

      <Stack spacing={2}>
        <Box>
          <Typography variant="subtitle2">Progress</Typography>
          <LinearProgress
            variant="determinate"
            value={summary.progressPercentage}
            sx={{
              height: 10,
              borderRadius: 5,
              mt: 1,
              backgroundColor: "#e0e0e0",
              "& .MuiLinearProgress-bar": {
                backgroundColor: progressColor,
              },
            }}
          />
          <Stack direction="row" justifyContent="space-between">
            <Typography variant="caption" color="text.secondary">
              {summary.progressPercentage.toFixed(2)}%
            </Typography>
            <Typography variant="caption" color="text.secondary">
              {progressLabel}
            </Typography>
          </Stack>
        </Box>

        <Divider />

        <Stack direction="row" spacing={2} alignItems="center">
          <BugReportIcon color="action" />
          <Typography variant="body2">
            Total Issues: {summary.totalIssues}
          </Typography>
        </Stack>

        <Stack direction="row" spacing={1} flexWrap="wrap">
          {Object.entries(summary.issueStatusCounts).map(([status, count]) => (
            <Chip
              key={status}
              label={`${status}: ${count}`}
              icon={
                status.toLowerCase() === "closed" ||
                status.toLowerCase() === "resolved" ? (
                  <CheckCircleIcon fontSize="small" />
                ) : (
                  <HourglassEmptyIcon fontSize="small" />
                )
              }
              size="small"
              color={
                status.toLowerCase() === "closed" ||
                status.toLowerCase() === "resolved"
                  ? "success"
                  : "warning"
              }
              sx={{ mb: 0.5 }}
            />
          ))}
        </Stack>

        <Stack direction="row" spacing={2} alignItems="center">
          <GroupIcon color="action" />
          <Typography variant="body2">
            Member: {summary.memberCount}
          </Typography>
        </Stack>

        <Stack direction="row" spacing={2} alignItems="center">
          <Typography variant="body2" color="text.secondary">
            Last Activity: {summary.lastActivityAt || "-"}
          </Typography>
        </Stack>
      </Stack>
    </Paper>
  );
};

export default ProjectSummaryCard;
