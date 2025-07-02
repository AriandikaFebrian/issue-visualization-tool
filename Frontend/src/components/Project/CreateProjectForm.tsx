// CreateProjectForm.tsx
import React, { useState } from "react";
import {
  TextField,
  Button,
  MenuItem,
  Box,
  Typography,
  Input,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
} from "@mui/material";
import axios from "axios";
import { ProjectStatus, ProjectVisibility } from "../../types/enums";

const statuses = ProjectStatus.map((status) => ({
  value: status,
  label: status.replace(/([A-Z])/g, " $1").trim(),
}));

const visibilities = ProjectVisibility.map((vis) => ({
  value: vis,
  label: vis,
}));

interface Props {
  onSuccess?: () => void;
}

const CreateProjectForm: React.FC<Props> = ({ onSuccess }) => {
  const token = localStorage.getItem("token");

  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [repositoryUrl, setRepositoryUrl] = useState("");
  const [documentationUrl, setDocumentationUrl] = useState("");
  const [status, setStatus] = useState<ProjectStatus>(ProjectStatus[0]);
  const [visibility, setVisibility] = useState<ProjectVisibility>(ProjectVisibility[0]);
  const [error, setError] = useState<string | null>(null);
  const [successMsg, setSuccessMsg] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);
  const [uploadFile, setUploadFile] = useState<File | null>(null);
  const [askUpload, setAskUpload] = useState(false);
  const [newProjectId, setNewProjectId] = useState<string | null>(null);
  const [showUploadPrompt, setShowUploadPrompt] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    setSuccessMsg(null);
    setLoading(true);

    if (!token) {
      setError("User belum login");
      setLoading(false);
      return;
    }

    try {
      const res = await axios.post(
        "https://localhost:5001/api/Project",
        {
          name,
          description,
          repositoryUrl: repositoryUrl || null,
          documentationUrl: documentationUrl || null,
          status,
          visibility,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );

      const projectId = res.data.projectId;
      setNewProjectId(projectId);
      setShowUploadPrompt(true); // Show prompt after successful create

      // Reset form fields
      setName("");
      setDescription("");
      setRepositoryUrl("");
      setDocumentationUrl("");
      setStatus(ProjectStatus[0]);
      setVisibility(ProjectVisibility[0]);

      if (onSuccess) onSuccess();
    } catch (err: any) {
      setError(err.response?.data?.message || "Terjadi kesalahan saat membuat project");
    } finally {
      setLoading(false);
    }
  };

  const handleUploadSource = async () => {
    if (!uploadFile || !newProjectId || !token) return;
    setLoading(true);
    try {
      const formData = new FormData();
      formData.append("file", uploadFile);

      await axios.post(
        `https://localhost:5001/api/Project/${newProjectId}/upload-source`,
        formData,
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "multipart/form-data",
          },
        }
      );

      setSuccessMsg("Project berhasil dibuat dan source code di-upload.");
    } catch (err: any) {
      setError("Gagal upload source code: " + (err.response?.data?.message || err.message));
    } finally {
      // Reset upload state
      setAskUpload(false);
      setUploadFile(null);
      setNewProjectId(null);
      setLoading(false);
    }
  };

  return (
    <Box component="form" onSubmit={handleSubmit}>
      <TextField
        label="Nama Project"
        value={name}
        onChange={(e) => setName(e.target.value)}
        fullWidth
        margin="normal"
        required
      />

      <TextField
        label="Deskripsi"
        value={description}
        onChange={(e) => setDescription(e.target.value)}
        fullWidth
        multiline
        rows={3}
        margin="normal"
      />

      <TextField
        label="Repository URL (Opsional)"
        value={repositoryUrl}
        onChange={(e) => setRepositoryUrl(e.target.value)}
        fullWidth
        margin="normal"
      />

      <TextField
        label="Documentation URL (Opsional)"
        value={documentationUrl}
        onChange={(e) => setDocumentationUrl(e.target.value)}
        fullWidth
        margin="normal"
      />

      <TextField
        select
        label="Status"
        value={status}
        onChange={(e) => setStatus(e.target.value as ProjectStatus)}
        fullWidth
        margin="normal"
      >
        {statuses.map((option) => (
          <MenuItem key={option.value} value={option.value}>
            {option.label}
          </MenuItem>
        ))}
      </TextField>

      <TextField
        select
        label="Visibilitas"
        value={visibility}
        onChange={(e) => setVisibility(e.target.value as ProjectVisibility)}
        fullWidth
        margin="normal"
      >
        {visibilities.map((option) => (
          <MenuItem key={option.value} value={option.value}>
            {option.label}
          </MenuItem>
        ))}
      </TextField>

      {error && (
        <Typography color="error" mt={2}>
          {error}
        </Typography>
      )}

      {successMsg && (
        <Typography color="success.main" mt={2}>
          {successMsg}
        </Typography>
      )}

      <Button
        type="submit"
        variant="contained"
        color="primary"
        fullWidth
        disabled={loading}
        sx={{ mt: 3 }}
      >
        {loading ? "Membuat..." : "Buat Project"}
      </Button>

      {/* Dialog Prompt Upload */}
      <Dialog open={showUploadPrompt} onClose={() => setShowUploadPrompt(false)}>
        <DialogTitle>Upload Source Code?</DialogTitle>
        <DialogContent>
          <Typography>Apakah kamu ingin meng-upload source code sekarang?</Typography>
        </DialogContent>
        <DialogActions>
          <Button
            onClick={() => {
              setShowUploadPrompt(false);
              setNewProjectId(null); // reset
              setUploadFile(null);
            }}
          >
            Nanti saja
          </Button>
          <Button
            variant="contained"
            onClick={() => {
              setShowUploadPrompt(false);
              setAskUpload(true); // show file upload dialog
            }}
          >
            Ya
          </Button>
        </DialogActions>
      </Dialog>

      {/* Dialog Upload File ZIP */}
      <Dialog open={askUpload} onClose={() => setAskUpload(false)}>
        <DialogTitle>Upload Source Code</DialogTitle>
        <DialogContent>
          <Typography>Silakan upload file ZIP berisi source code.</Typography>
          <Box mt={2}>
            <Input
              type="file"
              inputProps={{ accept: ".zip" }}
              onChange={(e) => {
                const target = e.target as HTMLInputElement;
                if (target.files && target.files.length > 0) {
                  setUploadFile(target.files[0]);
                }
              }}
            />
          </Box>
        </DialogContent>
        <DialogActions>
          <Button
            onClick={() => {
              setAskUpload(false);
              setUploadFile(null);
              setNewProjectId(null);
            }}
          >
            Batal
          </Button>
          <Button
            variant="contained"
            onClick={handleUploadSource}
            disabled={!uploadFile || loading}
          >
            Upload Sekarang
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
};

export default CreateProjectForm;
