import React, { useEffect, useState } from "react";
import {
  Badge,
  IconButton,
  Menu,
  MenuItem,
  Typography,
  CircularProgress,
  Card,
  CardContent,
  Popover,
  Box,
  Link as MuiLink,
} from "@mui/material";
import NotificationsIcon from "@mui/icons-material/Notifications";
import axios from "axios";

interface NotificationItem {
  id: string;
  recipientId: string;
  title: string;
  message: string;
  link?: string | null;
  actionText?: string | null;
  icon?: string | null;
  isRead: boolean;
  isDeleted: boolean;
  readAt?: string | null;
  createdAt: string;
}

const NotificationBell: React.FC = () => {
  const token = localStorage.getItem("token");
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [detailAnchorEl, setDetailAnchorEl] = useState<null | HTMLElement>(null);
  const [selectedNotif, setSelectedNotif] = useState<NotificationItem | null>(null);
  const [notifications, setNotifications] = useState<NotificationItem[]>([]);
  const [loading, setLoading] = useState(true);

  const open = Boolean(anchorEl);
  const detailOpen = Boolean(detailAnchorEl);

  const fetchNotifications = async () => {
    if (!token) return;
    setLoading(true);
    try {
      const res = await axios.get("https://localhost:5001/api/Notification", {
        headers: { Authorization: `Bearer ${token}` },
      });
      const notifs = Array.isArray(res.data) ? res.data : [];
      const visibleNotifs = notifs.filter((n: NotificationItem) => !n.isDeleted);
      setNotifications(visibleNotifs);
    } catch (error) {
      console.error("Gagal memuat notifikasi:", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchNotifications();
  }, [token]);

  const handleOpen = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
    setSelectedNotif(null);
    setDetailAnchorEl(null);
  };

  const handleNotifClick = async (
    notif: NotificationItem,
    event: React.MouseEvent<HTMLElement>
  ) => {
    if (!notif.isRead) {
      try {
        await axios.patch(
          `https://localhost:5001/api/Notification/${notif.id}/read`,
          {},
          {
            headers: { Authorization: `Bearer ${token}` },
          }
        );
        setNotifications((prev) =>
          prev.map((n) => (n.id === notif.id ? { ...n, isRead: true } : n))
        );
      } catch (err) {
        console.error("Gagal menandai sebagai dibaca:", err);
      }
    }
    setSelectedNotif(notif);
    setDetailAnchorEl(event.currentTarget);
  };

  const unreadCount = notifications.filter((n) => !n.isRead).length;

  return (
    <>
      <IconButton color="inherit" onClick={handleOpen}>
        <Badge badgeContent={unreadCount} color="error">
          <NotificationsIcon />
        </Badge>
      </IconButton>

      <Menu
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
        anchorOrigin={{ vertical: "bottom", horizontal: "right" }}
        transformOrigin={{ vertical: "top", horizontal: "right" }}
        PaperProps={{ sx: { width: 360, maxHeight: 400 } }}
      >
        {loading ? (
          <MenuItem disabled>
            <CircularProgress size={20} />
            <Typography ml={2}>Memuat notifikasi...</Typography>
          </MenuItem>
        ) : notifications.length === 0 ? (
          <MenuItem>Tidak ada notifikasi</MenuItem>
        ) : (
          notifications.map((notif) => (
            <MenuItem
              key={notif.id}
              disableGutters
              onClick={(e) => handleNotifClick(notif, e)}
            >
              <Card
                sx={{
                  width: "100%",
                  backgroundColor: notif.isRead ? "background.paper" : "action.hover",
                  boxShadow: "none",
                  cursor: "pointer",
                }}
              >
                <CardContent sx={{ py: 1.5, px: 2 }}>
                  <Typography fontSize={14} fontWeight={notif.isRead ? "normal" : "bold"}>
                    {notif.title}
                  </Typography>
                  <Typography variant="caption" color="text.secondary">
                    {new Date(notif.createdAt).toLocaleString()}
                  </Typography>
                </CardContent>
              </Card>
            </MenuItem>
          ))
        )}
      </Menu>

      <Popover
        open={detailOpen}
        anchorEl={detailAnchorEl}
        onClose={() => setDetailAnchorEl(null)}
        anchorOrigin={{
          vertical: "bottom",
          horizontal: "left",
        }}
        transformOrigin={{
          vertical: "top",
          horizontal: "left",
        }}
        PaperProps={{ sx: { p: 2, maxWidth: 400 } }}
      >
        {selectedNotif && (
          <Box>
            <Typography variant="h6" gutterBottom>
              {selectedNotif.title}
            </Typography>
            <Typography variant="body2" gutterBottom>
              {selectedNotif.message}
            </Typography>
            {selectedNotif.link && (
              <MuiLink href={selectedNotif.link} underline="hover" target="_blank" rel="noopener noreferrer">
                {selectedNotif.actionText ?? "Lihat detail"}
              </MuiLink>
            )}
            <Typography variant="caption" display="block" mt={1}>
              {new Date(selectedNotif.createdAt).toLocaleString()}
            </Typography>
          </Box>
        )}
      </Popover>
    </>
  );
};

export default NotificationBell;
