import { useState } from "react";
import axios from "axios";
import {
  Box,
  TextField,
  Button,
  MenuItem,
  Alert,
  Avatar,
  Typography,
  Stack,
  IconButton,
} from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import { useProfileContext } from "../context/ProfileContext";

const departments = [
  { value: 0, label: "Engineering" },
  { value: 1, label: "Product" },
  { value: 2, label: "Marketing" },
];

const positions = [
  { value: 0, label: "Intern" },
  { value: 1, label: "Junior" },
  { value: 2, label: "Senior" },
];

export default function EditProfilePage() {
  const { profile, setProfile } = useProfileContext();
  const [successMsg, setSuccessMsg] = useState("");
  const [errorMsg, setErrorMsg] = useState("");
  const [loadingImage, setLoadingImage] = useState(false);

  const token = localStorage.getItem("token");

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setProfile((prev: any) => ({
      ...prev,
      [name]: name === "department" || name === "position" ? Number(value) : value,
    }));
  };

  const handleSubmit = async () => {
    try {
      await axios.put("https://localhost:5001/api/auth/me", profile, {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      });
      setSuccessMsg("Profil berhasil diperbarui!");
      setErrorMsg("");
    } catch (err) {
      console.error(err);
      setErrorMsg("Gagal memperbarui profil.");
      setSuccessMsg("");
    }
  };

  const handleImageUpload = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (!file) return;

    const formData = new FormData();
    formData.append("file", file);
    setLoadingImage(true);

    try {
      const res = await axios.post("https://localhost:5001/api/auth/upload", formData, {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "multipart/form-data",
        },
      });

      const { url } = res.data;

      setProfile((prev: any) => ({
        ...prev,
        profilePictureUrl: url,
      }));
    } catch (err) {
      console.error("Gagal upload gambar:", err);
      setErrorMsg("Gagal upload gambar.");
    } finally {
      setLoadingImage(false);
    }
  };

  const handleRemovePhoto = () => {
    setProfile((prev: any) => ({
      ...prev,
      profilePictureUrl: "",
    }));
  };

  if (!profile) return null;

  return (
    <Box
      sx={{
        width: "100%",
        maxWidth: 500,
        mx: "auto",
        textAlign: "center",
      }}
    >
      {/* Avatar upload */}
      <Box sx={{ display: "flex", justifyContent: "center", mb: 2 }}>
        <Box sx={{ position: "relative", textAlign: "center" }}>
          <input
            type="file"
            accept="image/*"
            id="avatar-upload"
            style={{ display: "none" }}
            onChange={handleImageUpload}
          />
          <label htmlFor="avatar-upload" style={{ cursor: "pointer" }}>
            <Avatar
              src={profile.profilePictureUrl || undefined}
              alt={profile.fullName || "Profil"}
              sx={{
                width: 100,
                height: 100,
                border: "2px solid #2ea043",
                bgcolor: "#30363d",
                fontSize: 40,
                mx: "auto",
              }}
            >
              {!profile.profilePictureUrl && profile.fullName
                ? profile.fullName.charAt(0).toUpperCase()
                : ""}
            </Avatar>
            <Typography variant="caption" display="block" color="gray" mt={1}>
              {loadingImage ? "Mengunggah..." : "Klik untuk ganti foto"}
            </Typography>
          </label>

          {/* Tombol Hapus Foto */}
          {profile.profilePictureUrl && (
            <Stack direction="row" justifyContent="center" mt={1}>
              <IconButton
                onClick={handleRemovePhoto}
                size="small"
                sx={{ color: "red" }}
                title="Hapus foto"
              >
                <DeleteIcon fontSize="small" />
              </IconButton>
            </Stack>
          )}
        </Box>
      </Box>

      {successMsg && (
        <Alert severity="success" sx={{ mb: 2 }}>
          {successMsg}
        </Alert>
      )}
      {errorMsg && (
        <Alert severity="error" sx={{ mb: 2 }}>
          {errorMsg}
        </Alert>
      )}

      <TextField
        label="Username"
        name="username"
        value={profile.username || ""}
        onChange={handleChange}
        fullWidth
        margin="dense"
      />
      <TextField
        label="Full Name"
        name="fullName"
        value={profile.fullName || ""}
        onChange={handleChange}
        fullWidth
        margin="dense"
      />
      <TextField
        label="Phone Number"
        name="phoneNumber"
        value={profile.phoneNumber || ""}
        onChange={handleChange}
        fullWidth
        margin="dense"
      />
      <TextField
        select
        label="Department"
        name="department"
        value={profile.department ?? ""}
        onChange={handleChange}
        fullWidth
        margin="dense"
      >
        {departments.map((opt) => (
          <MenuItem key={opt.value} value={opt.value}>
            {opt.label}
          </MenuItem>
        ))}
      </TextField>
      <TextField
        select
        label="Position"
        name="position"
        value={profile.position ?? ""}
        onChange={handleChange}
        fullWidth
        margin="dense"
      >
        {positions.map((opt) => (
          <MenuItem key={opt.value} value={opt.value}>
            {opt.label}
          </MenuItem>
        ))}
      </TextField>

      <Button
        variant="contained"
        color="primary"
        onClick={handleSubmit}
        fullWidth
        sx={{ mt: 2 }}
      >
        Simpan Perubahan
      </Button>
    </Box>
  );
}
