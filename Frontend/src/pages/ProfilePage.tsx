import React from "react";
import EditProfilePage from "../components/EditProfilePage";
import {
  Box,
  Paper,
  Typography,
  List,
  ListItemButton,
  ListItemText,
} from "@mui/material";

const ProfilePage: React.FC = () => {
  return (
    <Box
      sx={{
        minHeight: "100vh",
        bgcolor: "#0d1117",
        color: "white",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        py: 5,
      }}
    >
      <Box sx={{ display: "flex", width: "100%", maxWidth: 900 }}>
        <Paper
          elevation={3}
          sx={{
            bgcolor: "#161b22",
            p: 2,
            width: 250,
            borderRadius: 2,
            mr: 2,
            flexShrink: 0,
            height: "100%",
          }}
        >
          <Typography variant="h6" gutterBottom>
            Menu Profil
          </Typography>
          <List>
            {["Edit Profil", "Ganti Password", "Preferensi"].map((item) => (
              <ListItemButton
                key={item}
                sx={{
                  borderRadius: 1,
                  mb: 1,
                  "&:hover": {
                    bgcolor: "#238636",
                    color: "white",
                  },
                }}
              >
                <ListItemText primary={item} />
              </ListItemButton>
            ))}
          </List>
        </Paper>

        {/* Panel Kanan */}
        <Paper
          elevation={3}
          sx={{
            bgcolor: "#161b22",
            p: 3,
            borderRadius: 2,
            flexGrow: 1,
            display: "flex",
            flexDirection: "column",
            justifyContent: "center",
          }}
        >
          <Typography variant="h5" gutterBottom align="center">
            Edit Profil
          </Typography>
          <EditProfilePage />
        </Paper>
      </Box>
    </Box>
  );
};

export default ProfilePage;
