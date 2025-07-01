import { Box } from "@mui/material";
import LoginForm from "../components/LoginForm";
import FooterGitHubStyle from "../components/Reuseable/FooterGitHubStyle";

export default function LoginPage() {
  return (
    <Box
      sx={{
        height: "100vh",
        backgroundColor: "#0d1117",
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
      }}
    >
      <Box sx={{ flexGrow: 1, display: "flex", alignItems: "center" }}>
        <LoginForm />
      </Box>

      <Box pb={2}>
        <FooterGitHubStyle />
      </Box>
    </Box>
  );
}
