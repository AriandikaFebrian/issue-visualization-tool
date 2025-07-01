import React, { useEffect, useState } from "react";
import axios from "axios";
import {
  Box,
  Typography,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  CircularProgress,
} from "@mui/material";

type Project = {
  id: string;
  name: string;
  description: string;
  projectCode: string;
  repositoryUrl: string;
  documentationUrl: string;
  status: string;
  visibility: string;
  createdAt: string;
  updatedAt: string | null;
  ownerId: string;
  ownerName: string;
  ownerEmail: string;
  ownerProfilePictureUrl: string;
  memberCount: number;
  issueCount: number;
  members: string[];
  issues: any[];
};

const MyProjects: React.FC = () => {
  const [projects, setProjects] = useState<Project[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchProjects = async () => {
      try {
        const response = await axios.get("https://localhost:5001/api/Project/mine", {
          withCredentials: true,
          headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        });
        setProjects(response.data);
      } catch (error) {
        console.error("Gagal mengambil project:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchProjects();
  }, []);

  if (loading) {
    return (
      <Box display="flex" justifyContent="center" alignItems="center" height="200px">
        <CircularProgress />
        <Typography ml={2}>Memuat data proyek...</Typography>
      </Box>
    );
  }

  return (
    <Box p={4}>
      <Typography variant="h5" fontWeight="bold" gutterBottom>
        Daftar Proyek
      </Typography>

      {projects.length === 0 ? (
        <Typography>Tidak ada proyek.</Typography>
      ) : (
        <TableContainer component={Paper}>
          <Table size="small">
            <TableHead>
              <TableRow>
                <TableCell><strong>Project Code</strong></TableCell>
                <TableCell><strong>Judul</strong></TableCell>
                <TableCell><strong>Sub Judul</strong></TableCell>
                <TableCell><strong>Created At</strong></TableCell>
                <TableCell><strong>Created By</strong></TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {projects.map((project) => (
                <TableRow key={project.id}>
                  <TableCell>
                    <Typography fontFamily="monospace">{project.projectCode}</Typography>
                  </TableCell>
                  <TableCell>{project.name}</TableCell>
                  <TableCell>{project.description}</TableCell>
                  <TableCell>
                    {new Date(project.createdAt).toLocaleDateString()}
                  </TableCell>
                  <TableCell>{project.ownerName}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}
    </Box>
  );
};

export default MyProjects;
