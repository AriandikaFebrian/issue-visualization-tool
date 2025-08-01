import React, { useState } from "react";
import {
  Avatar,
  Box,
  Button,
  Chip,
  CircularProgress,
  Divider,
  IconButton,
  List,
  ListItem,
  ListItemAvatar,
  ListItemText,
  Popover,
  Tooltip,
  Typography,
} from "@mui/material";
import GroupIcon from "@mui/icons-material/Group";
import axios from "axios";
import AddMemberForm from "./AddMemberForm";

type ProjectMember = {
  userNRP: string;
  username: string;
  email: string;
  role: string;
};

interface Props {
  projectCode: string;
  asChip?: boolean;
  memberCount?: number;
}

const ProjectMembersPopover: React.FC<Props> = ({
  projectCode,
  asChip = false,
  memberCount = 0,
}) => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [members, setMembers] = useState<ProjectMember[]>([]);
  const [loading, setLoading] = useState(false);
  const [fetched, setFetched] = useState(false);
  const [showForm, setShowForm] = useState(false);

  const handleOpen = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
    if (!fetched) {
      fetchMembers();
    }
  };

  const handleClose = () => {
    setAnchorEl(null);
    setShowForm(false);
  };

  const fetchMembers = async () => {
    setLoading(true);
    try {
      const res = await axios.get(
        `https://localhost:5001/api/Project/${projectCode}/members`,
        {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        }
      );
      setMembers(res.data);
      setFetched(true);
    } catch (err) {
      console.error("Gagal mengambil anggota project", err);
    } finally {
      setLoading(false);
    }
  };

  const open = Boolean(anchorEl);

  return (
    <>
      {asChip ? (
        <Chip
          icon={<GroupIcon />}
          label={`${memberCount} Members`}
          size="small"
          onClick={handleOpen}
          clickable
          sx={{ cursor: "pointer" }}
        />
      ) : (
        <Tooltip title="Lihat anggota">
          <IconButton size="small" onClick={handleOpen}>
            <GroupIcon />
          </IconButton>
        </Tooltip>
      )}

      <Popover
        open={open}
        anchorEl={anchorEl}
        onClose={handleClose}
        anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
        transformOrigin={{ vertical: "top", horizontal: "center" }}
        PaperProps={{ sx: { p: 2, maxWidth: 350, width: "100%" } }}
      >
        <Typography variant="subtitle1" fontWeight="bold" mb={1}>
          Anggota Proyek
        </Typography>

        {loading ? (
          <Box display="flex" alignItems="center" gap={2} p={1}>
            <CircularProgress size={20} />
            <Typography variant="body2">Memuat anggota...</Typography>
          </Box>
        ) : (
          <>
            {members.length === 0 ? (
              <Typography variant="body2" color="text.secondary" mb={2}>
                Tidak ada anggota.
              </Typography>
            ) : (
              <List dense disablePadding>
                {members.map((member) => (
                  <ListItem key={member.userNRP} disableGutters>
                    <ListItemAvatar>
                      <Avatar>{member.username?.charAt(0).toUpperCase()}</Avatar>
                    </ListItemAvatar>
                    <ListItemText
                      primary={`${member.username} (${member.userNRP})`}
                      secondary={member.email}
                    />
                    <Chip label={member.role} size="small" color="primary" />
                  </ListItem>
                ))}
              </List>
            )}

            <Divider sx={{ my: 1 }} />

            {!showForm ? (
              <Button
                size="small"
                variant="outlined"
                fullWidth
                onClick={() => setShowForm(true)}
              >
                + Tambah Anggota
              </Button>
            ) : (
              <AddMemberForm
                projectCode={projectCode}
                onSuccess={() => {
                  fetchMembers();
                  setShowForm(false);
                }}
              />
            )}
          </>
        )}
      </Popover>
    </>
  );
};

export default ProjectMembersPopover;
