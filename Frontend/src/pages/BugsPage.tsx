import {
  Box,
  Typography,
  Paper,
  Button,
  SwipeableDrawer,
  IconButton,
  Divider,
} from "@mui/material";
import ActivityFeed from "../components/ActivityFeed";
import useScrollDirection from "../hooks/useScrollDirection";
import PublicProjectFeed from "../components/PublicProjectFeed";
import RecentIssues from "../components/RecentIssues";
import BugReportIcon from "@mui/icons-material/BugReport";
import CloseIcon from "@mui/icons-material/Close";
import { useEffect, useState } from "react";
import FileTreeViewer from "../components/FileTreeViewer";

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
  const [subtext, setSubtext] = useState("");

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
    if (hour < 12) return "morning";
    if (hour < 17) return "afternoon";
    if (hour < 21) return "evening";
    return "night";
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
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        width: "100%",
        padding: { xs: 2, md: 3 },
      }}
    >
      {/* Optional greeting */}
      {!loadingUser && user && (
        <>
          <Typography variant="h5">
            Good {getGreetingTime()}, {user.fullName}!
          </Typography>
          <Typography variant="subtitle1" color="text.secondary" mt={1}>
            {subtext}
          </Typography>
        </>
      )}

      {/* Main content */}
      <Paper
        sx={{
          p: { xs: 2, md: 3 },
          mt: 2,
          width: "100%",
        }}
      >
        <FileTreeViewer projectId="11650229-19e2-4683-8d84-215cb58bc2ba" />
      </Paper>
    </Box>
  );
}
