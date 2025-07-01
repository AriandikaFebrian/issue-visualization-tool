import React, { useEffect, useState } from "react";
import {
  AppBar,
  Avatar,
  Box,
  CircularProgress,
  IconButton,
  Menu,
  MenuItem,
  Toolbar,
  Typography,
  Divider,
} from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import ArrowDropDownIcon from "@mui/icons-material/ArrowDropDown";
import { useNavigate } from "react-router-dom";
import NotificationBell from "./NotificationBell";
import useScrollDirection from "../../hooks/useScrollDirection";
import AssignedToMePopover from "../AssignedToMe";

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

interface NavbarProps {
  token: string;
  onOpenSidebar: () => void;
}

const Navbar: React.FC<NavbarProps> = ({ token, onOpenSidebar }) => {
  const [user, setUser] = useState<UserProfile | null>(null);
  const [loading, setLoading] = useState(true);
  const [anchorElUser, setAnchorElUser] = useState<null | HTMLElement>(null);
  const navigate = useNavigate();
  const scrollDirection = useScrollDirection();

  const API_URL = "https://localhost:5001/api/Auth/me";
  const UPLOAD_BASE_URL = "https://localhost:5001";

  useEffect(() => {
    if (!token) return;
    fetch(API_URL, {
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
      .finally(() => setLoading(false));
  }, [token]);

  const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElUser(event.currentTarget);
  };

  const handleCloseUserMenu = () => setAnchorElUser(null);

  const handleLogout = () => {
    localStorage.removeItem("token");
    handleCloseUserMenu();
    navigate("/login");
  };

  const handleProfileClick = () => {
    handleCloseUserMenu();
    navigate("/profile");
  };

  const avatarSrc = user?.profilePictureUrl
    ? user.profilePictureUrl.startsWith("http")
      ? user.profilePictureUrl
      : `${UPLOAD_BASE_URL}${user.profilePictureUrl}`
    : undefined;

  if (loading) {
    return (
      <AppBar position="static" color="primary">
        <Toolbar>
          <CircularProgress color="inherit" size={24} />
          <Typography sx={{ ml: 2 }}>Loading user...</Typography>
        </Toolbar>
      </AppBar>
    );
  }

  return (
    <AppBar
      position="fixed"
      sx={{
        bgcolor: "#0d1117",
        zIndex: (theme) => theme.zIndex.drawer + 1,
        transition: "transform 0.3s ease",
        transform: scrollDirection === "down" ? "translateY(-100%)" : "translateY(0)",
      }}
    >
      <Toolbar
        sx={{
          display: "flex",
          justifyContent: "space-between",
          minHeight: 56,
          px: 2,
        }}
      >
        {/* Kiri: Sidebar Toggle + Judul */}
        <Box sx={{ display: "flex", alignItems: "center", gap: 2 }}>
          <IconButton color="inherit" onClick={onOpenSidebar}>
            <MenuIcon />
          </IconButton>
          <Typography variant="h6" noWrap>
            MyApp
          </Typography>
        </Box>

        {/* Kanan: Notifikasi, AssignedToMe, Profil */}
        {user && (
          <Box sx={{ display: "flex", alignItems: "center", gap: 2 }}>
            {/* 🔔 Notifikasi */}
            <NotificationBell token={token} />

            {/* ✅ AssignedToMe */}
            <AssignedToMePopover />

            {/* 👤 Profil */}
            <Box
              onClick={handleOpenUserMenu}
              sx={{ display: "flex", alignItems: "center", gap: 1, cursor: "pointer" }}
            >
              <Typography
                fontSize={14}
                fontWeight={500}
                color="white"
                sx={{ display: { xs: "none", sm: "block" } }}
              >
                {user.fullName}
              </Typography>
              <ArrowDropDownIcon sx={{ color: "white" }} />
              <Avatar src={avatarSrc} alt={user.fullName} sx={{ width: 32, height: 32 }}>
                {!avatarSrc && user.fullName.charAt(0).toUpperCase()}
              </Avatar>
            </Box>
          </Box>
        )}
      </Toolbar>

      {/* Menu Dropdown Profil */}
      <Menu
        anchorEl={anchorElUser}
        open={Boolean(anchorElUser)}
        onClose={handleCloseUserMenu}
        anchorOrigin={{ vertical: "bottom", horizontal: "right" }}
        transformOrigin={{ vertical: "top", horizontal: "right" }}
      >
        <MenuItem disabled>
          <Typography variant="subtitle2">{user?.nrp}</Typography>
        </MenuItem>
        <Divider />
        <MenuItem onClick={handleProfileClick}>Lihat Profil</MenuItem>
        <MenuItem onClick={() => alert("Preference belum diimplementasi")}>
          Preference
        </MenuItem>
        <MenuItem onClick={handleLogout}>Logout</MenuItem>
      </Menu>
    </AppBar>
  );
};

export default Navbar;
