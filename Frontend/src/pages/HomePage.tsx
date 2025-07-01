import { Box, Typography, Paper, Button, SwipeableDrawer, IconButton, Divider } from "@mui/material";
import ActivityFeed from "../components/ActivityFeed";
import useScrollDirection from "../hooks/useScrollDirection";
import PublicProjectFeed from "../components/PublicProjectFeed";
import RecentIssues from "../components/RecentIssues";
import BugReportIcon from '@mui/icons-material/BugReport';
import CloseIcon from '@mui/icons-material/Close';
import { useEffect, useState } from "react";

interface UserProfile {
  id: string;
  username: string;
  email: string;
  role: number;
  nrp: string;
  fullName: string;
  profilePictureUrl: string;
  phoneNumber?: string;
  department: number;
  position: number;
}


export default function HomePage() {
  const scrollDirection = useScrollDirection();
  const [drawerOpen, setDrawerOpen] = useState(false);

  const [isAtTop, setIsAtTop] = useState(true);

  const [user, setUser] = useState<UserProfile | null>(null);
const [loadingUser, setLoadingUser] = useState(true);
const [subtext, setSubtext] = useState('');

useEffect(() => {
  setSubtext(getRandomSubtext());
}, []);


useEffect(() => {
  const token = localStorage.getItem("token");
  if (!token) return;

  fetch("https://localhost:5001/api/Auth/me", {
    headers: {
      Authorization: `Bearer ${token}`,
      Accept: "application/json",
    },
  })
    .then((res) => {
      if (!res.ok) throw new Error("Failed to fetch user data");
      return res.json();
    })
    .then((data: UserProfile) => setUser(data))
    .catch(() => setUser(null))
    .finally(() => setLoadingUser(false));
}, []);


useEffect(() => {
  const handleScroll = () => {
    setIsAtTop(window.scrollY === 0);
  };

  window.addEventListener("scroll", handleScroll);
  return () => window.removeEventListener("scroll", handleScroll);
}, []);

const getGreetingTime = () => {
  const hour = new Date().getHours();
  if (hour < 12) return 'morning';
  if (hour < 17) return 'afternoon';
  if (hour < 21) return 'evening';
  return 'night';
};

const getRandomSubtext = () => {
  const options = [
    "Let’s squash some bugs today! 🐛🔥",
    "Ready to build something awesome? 🚀",
    "Keep pushing — you're doing great! 💪",
    "Every issue resolved is a victory! 🏆",
    "Small steps make big progress. 👣",
    "Let’s clean up some code today! 🧹",
    "One bug at a time. You got this! 🐞",
    "Keep calm and debug on. 😎",
    "Push code, not stress. 💻✨",
    "Make it work, make it right, make it fast. 🔧",
  ];
  return options[Math.floor(Math.random() * options.length)];
};


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
        <Box
  display="flex"
  flexWrap="wrap"
  alignItems="center"
  justifyContent="space-between"
  mb={2}
  gap={2}
>
  <Typography variant="h5" fontWeight={600}>
  {loadingUser
    ? 'Loading...'
    : `Good ${getGreetingTime()}, ${user?.fullName?.split(' ')[0] || 'there'} 👋`}
</Typography>

<Typography variant="body2" color="text.secondary" mt={0.5}>
  {subtext}
</Typography>

</Box>


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