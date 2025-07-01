import { Box, Typography } from "@mui/material";
import RegisterForm from "../components/RegisterForm";

export default function RegisterPage() {
  return (
    <Box
      display="flex"
      height="100vh"
      sx={{ backgroundColor: "#0d1117" }}
    >
      {/* Kiri: Formulir */}
      <Box
        flex={1}
        display="flex"
        justifyContent="center"
        alignItems="center"
        p={4}
      >
        <RegisterForm />
      </Box>

      {/* Kanan: Panel Informasi */}
      <Box
        flex={1}
        display="flex"
        justifyContent="center"
        alignItems="center"
        sx={{
          backgroundImage: "url('/path/to/your-image.jpg')",
          backgroundSize: "cover",
          backgroundPosition: "center",
          color: "white",
        }}
      >
        <Box sx={{ textAlign: "center", p: 4, backdropFilter: "blur(4px)" }}>
          <Typography variant="h4" fontWeight="bold" gutterBottom>
            Selamat Datang di BugNEst
          </Typography>
          <Typography variant="body1">
            Sistem pengelolaan bug yang modern dan terorganisir untuk tim development Anda.
          </Typography>
        </Box>
      </Box>
    </Box>
  );
}
