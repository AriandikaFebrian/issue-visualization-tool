import React, { useEffect, useState, useMemo } from "react";
import {
  Box,
  List,
  ListItem,
  Avatar,
  Typography,
  Link,
  Tooltip,
  Skeleton,
} from "@mui/material";
import axios from "axios";

type RecentProject = {
  projectCode: string;
  name: string;
  repositoryUrl: string;
  accessedAt: string;
};

type UserProfile = {
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
};

const RecentProjectFeed: React.FC = () => {
  const [projects, setProjects] = useState<RecentProject[]>([]);
  const [loadingProjects, setLoadingProjects] = useState(true);

  const [user, setUser] = useState<UserProfile | null>(null);
  const [loadingUser, setLoadingUser] = useState(true);

  const UPLOAD_BASE_URL = "https://localhost:5001";

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const token = localStorage.getItem("token");
        if (!token) throw new Error("Token tidak tersedia");

        const response = await axios.get("https://localhost:5001/api/Auth/me", {
          headers: {
            Authorization: `Bearer ${token}`,
            Accept: "application/json",
          },
        });

        setUser(response.data);
      } catch (error) {
        console.error("Gagal mengambil data user:", error);
      } finally {
        setLoadingUser(false);
      }
    };

    const fetchProjects = async () => {
      try {
        const token = localStorage.getItem("token");
        if (!token) throw new Error("Token tidak tersedia");

        const response = await axios.get("https://localhost:5001/api/Project/recent", {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });

        setProjects(response.data);
      } catch (error) {
        console.error("Gagal mengambil proyek terbaru:", error);
      } finally {
        setLoadingProjects(false);
      }
    };

    fetchUser();
    fetchProjects();
  }, []);

  const latestProjects = useMemo(
    () =>
      [...projects]
        .sort((a, b) => new Date(b.accessedAt).getTime() - new Date(a.accessedAt).getTime())
        .slice(0, 3),
    [projects]
  );

  const userAvatar = useMemo(() => {
    if (!user) return null;

    const avatarSrc = user.profilePictureUrl?.startsWith("http")
      ? user.profilePictureUrl
      : `${UPLOAD_BASE_URL}${user.profilePictureUrl}`;

    return (
      <Avatar
        src={avatarSrc}
        alt={user.fullName}
        sx={{ width: 28, height: 28 }}
      >
        {!user.profilePictureUrl && user.fullName.charAt(0).toUpperCase()}
      </Avatar>
    );
  }, [user]);

  return (
    <Box

    >
      <Typography
        variant="subtitle2"
        sx={{ color: "gray", fontSize: "0.7rem", mb: 1 }}
      >
        Recent Projects
      </Typography>

      {loadingUser || loadingProjects ? (
        <List dense disablePadding>
          {[1, 2, 3].map((i) => (
            <ListItem key={i} sx={{ gap: 1, py: 0.5 }}>
              <Skeleton variant="circular" width={28} height={28} />
              <Skeleton width="100%" height={16} />
            </ListItem>
          ))}
        </List>
      ) : (
        <List dense disablePadding>
          {latestProjects.map((project) => (
            <ListItem
              key={project.projectCode}
              sx={{
                px: 0.5,
                py: 0.5,
                mb: 0.5,
                borderRadius: 1,
                display: "flex",
                alignItems: "center",
                gap: 1,
                "&:hover": {
                  backgroundColor: "#1f2937",
                },
              }}
            >
              {userAvatar}
              <Tooltip
                title={`${project.projectCode} / ${project.name}`}
                arrow
                placement="right"
              >
                <Link
                  href={project.repositoryUrl}
                  target="_blank"
                  underline="hover"
                  sx={{
                    fontSize: "0.68rem",
                    color: "#c9d1d9",
                    whiteSpace: "nowrap",
                    overflow: "hidden",
                    textOverflow: "ellipsis",
                    flex: 1,
                  }}
                >
                  {project.projectCode} / {project.name}
                </Link>
              </Tooltip>
            </ListItem>
          ))}
        </List>
      )}
    </Box>
  );
};

export default RecentProjectFeed;
