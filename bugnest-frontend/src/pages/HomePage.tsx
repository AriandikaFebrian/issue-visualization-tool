import { Box, Typography, Paper, Button, SwipeableDrawer, IconButton, Divider } from "@mui/material";
import ActivityFeed from "../components/ActivityFeed";
import useScrollDirection from "../hooks/useScrollDirection";
import PublicProjectFeed from "../components/PublicProjectFeed";
import RecentIssues from "../components/RecentIssues";
import BugReportIcon from '@mui/icons-material/BugReport';
import CloseIcon from '@mui/icons-material/Close';
import { useEffect, useState } from "react";

export default function HomePage() {
  const scrollDirection = useScrollDirection();
  const [drawerOpen, setDrawerOpen] = useState(false);

  const [isAtTop, setIsAtTop] = useState(true);

useEffect(() => {
  const handleScroll = () => {
    setIsAtTop(window.scrollY === 0);
  };

  window.addEventListener("scroll", handleScroll);
  return () => window.removeEventListener("scroll", handleScroll);
}, []);

  return (
    <Box
      sx={{
        maxWidth: 1400,
        mx: "auto",
        px: { xs: 2, md: 4 },
        pt: 4,
        display: "flex",
        flexDirection: { xs: "column", md: "row" },
        gap: 4,
      }}
    >
      {/* Sidebar Kiri */}
      <Box
        sx={{
          display: { xs: "none", md: "flex" },
          flexDirection: "column",
          flexBasis: "25%",
          minWidth: 240,
          maxWidth: 300,
          position: "sticky",
          top: isAtTop ? 88 : scrollDirection === "down" ? 32 : 64,

          transition: "top 0.3s ease",
          alignSelf: "flex-start",
          gap: 2,
        }}
      >
        <PublicProjectFeed />

        <Button
          variant="outlined"
          startIcon={<BugReportIcon />}
          onClick={() => setDrawerOpen(true)}
          sx={{ mt: 1 }}
        >
          Isu Terbaru
        </Button>
      </Box>

      {/* Konten Utama */}
      <Box
        sx={{
          flexGrow: 1,
          flexBasis: "75%",
          minWidth: 0,
        }}
      >
        <Typography variant="h4" gutterBottom>
          Home
        </Typography>

        <Paper
          sx={{
            p: { xs: 2, md: 3 },
            mt: 2,
            width: "100%",
          }}
        >
          <ActivityFeed />
        </Paper>
      </Box>

      {/* Drawer Isu Terbaru */}
      <SwipeableDrawer
        anchor="right"
        open={drawerOpen}
        onClose={() => setDrawerOpen(false)}
        onOpen={() => setDrawerOpen(true)}
        PaperProps={{
          sx: {
            width: 360,
            bgcolor: '#0d1117',
            color: '#c9d1d9',
            borderLeft: '1px solid #30363d',
          },
        }}
      >
        <Box display="flex" alignItems="center" justifyContent="space-between" p={2}>
          <Typography variant="subtitle1" fontWeight={600}>
            🐞 Isu Terbaru
          </Typography>
          <IconButton onClick={() => setDrawerOpen(false)} sx={{ color: '#c9d1d9' }}>
            <CloseIcon />
          </IconButton>
        </Box>
        <Divider sx={{ borderColor: '#30363d' }} />

        <Box p={2}>
          <RecentIssues />
        </Box>
      </SwipeableDrawer>
    </Box>
  );
}