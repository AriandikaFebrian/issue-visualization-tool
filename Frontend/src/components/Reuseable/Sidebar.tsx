import React from "react";
import {
  Drawer,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  ListItemButton,
  Box,
  IconButton,
  Typography,
  Divider,
} from "@mui/material";
import HomeIcon from "@mui/icons-material/Home";
import BugReportIcon from "@mui/icons-material/BugReport";
import WorkspacesIcon from "@mui/icons-material/Workspaces";
import LogoutIcon from "@mui/icons-material/Logout";
import CloseIcon from "@mui/icons-material/Close";
import { useNavigate } from "react-router-dom";
import RecentProjectFeed from "../RecentProjectFeed";

type SidebarProps = {
  open: boolean;
  onClose: () => void;
};

const Sidebar: React.FC<SidebarProps> = ({ open, onClose }) => {
  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.removeItem("token");
    navigate("/login");
  };

  return (
    <Drawer
      variant="temporary"
      open={open}
      onClose={onClose}
      ModalProps={{
        keepMounted: true,
        sx: {
          zIndex: (theme) => theme.zIndex.drawer + 10,
        },
      }}
      PaperProps={{
        sx: {
          width: 300,
          bgcolor: "#1c1c1c",
          color: "#fff",
          p: 2,
        },
      }}
    >
      <Box>
        {/* Header Close Button */}
        <Box
          sx={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
            mb: 2,
          }}
        >
          <Typography variant="h6" sx={{ color: "#fff" }}>
            Menu
          </Typography>
          <IconButton onClick={onClose} sx={{ color: "#fff" }}>
            <CloseIcon />
          </IconButton>
        </Box>

        {/* Menu */}
        <List>
          <ListItem disablePadding>
            <ListItemButton onClick={() => { navigate("/"); onClose(); }}>
              <ListItemIcon sx={{ color: "#fff" }}><HomeIcon /></ListItemIcon>
              <ListItemText primary="Home" />
            </ListItemButton>
          </ListItem>

          <ListItem disablePadding>
            <ListItemButton onClick={() => { navigate("/bugs"); onClose(); }}>
              <ListItemIcon sx={{ color: "#fff" }}><BugReportIcon /></ListItemIcon>
              <ListItemText primary="Bugs" />
            </ListItemButton>
          </ListItem>

          <ListItem disablePadding>
            <ListItemButton onClick={() => { navigate("/my-project"); onClose(); }}>
              <ListItemIcon sx={{ color: "#fff" }}><WorkspacesIcon /></ListItemIcon>
              <ListItemText primary="My Project" />
            </ListItemButton>
          </ListItem>

          <ListItem disablePadding>
            <ListItemButton onClick={() => { handleLogout(); onClose(); }}>
              <ListItemIcon sx={{ color: "#fff" }}><LogoutIcon /></ListItemIcon>
              <ListItemText primary="Logout" />
            </ListItemButton>
          </ListItem>
        </List>

        <Divider sx={{ my: 2 }} />

        {/* Recent Issues Dipindah ke Sidebar */}
        <Typography variant="subtitle1" sx={{ color: "#fff", mb: 1 }}>
          Recent Issues
        </Typography>
        <RecentProjectFeed />

      </Box>
    </Drawer>
  );
};

export default Sidebar;
