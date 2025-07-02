import React, { useState } from "react";
import {
  TextField,
  Button,
  Snackbar,
  Alert,
  Stack,
} from "@mui/material";
import axios from "axios";

interface Props {
  projectCode: string;
  onSuccess?: () => void; // optional callback to refresh member list
}

const AddMemberForm: React.FC<Props> = ({ projectCode, onSuccess }) => {
  const [nrp, setNrp] = useState("");
  const [loading, setLoading] = useState(false);
  const [snackbarOpen, setSnackbarOpen] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);

    try {
      await axios.post(
        `https://localhost:5001/api/Project/${projectCode}/members`,
        { projectCode, userNRP: nrp },
        {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        }
      );

      setSnackbarOpen(true);
      setNrp("");
      if (onSuccess) onSuccess();
    } catch (err: any) {
      setError(err.response?.data?.message || "Gagal menambahkan anggota");
    } finally {
      setLoading(false);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <Stack spacing={2}>
        <TextField
          label="Masukkan NRP"
          value={nrp}
          onChange={(e) => setNrp(e.target.value)}
          fullWidth
          required
        />
        <Button
          type="submit"
          variant="contained"
          disabled={loading}
        >
          {loading ? "Menambahkan..." : "Tambah Anggota"}
        </Button>
        {error && (
          <Alert severity="error">{error}</Alert>
        )}
      </Stack>

      <Snackbar
        open={snackbarOpen}
        autoHideDuration={3000}
        onClose={() => setSnackbarOpen(false)}
        anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
      >
        <Alert severity="success" onClose={() => setSnackbarOpen(false)}>
          Anggota berhasil ditambahkan!
        </Alert>
      </Snackbar>
    </form>
  );
};

export default AddMemberForm;
