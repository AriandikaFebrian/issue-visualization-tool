import {
  Box,
  Button,
  CircularProgress,
  Paper,
  Stack,
  TextField,
  Typography,
  Link,
} from "@mui/material";
import { useForm } from "react-hook-form";
import { useLogin } from "../hooks/useLogin";
import type { LoginFormInputs } from "../types/auth";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

export default function LoginForm() {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormInputs>();
  const { mutateAsync, isPending, error } = useLogin();
  const navigate = useNavigate();
  const { toggleAuthMode } = useAuth();

  const onSubmit = async (data: LoginFormInputs) => {
    const res = await mutateAsync(data);
    localStorage.setItem("token", res.token);
    navigate("/");
  };

  return (
<Box
  display="flex"
  flexDirection="column"
  alignItems="center"
  justifyContent="center"
>

      {/* Optional GitHub-style icon */}
      <Box mb={2}>
        <img
          src="../assets/background1.jpg"
          alt="logo"
          width={48}
          height={48}
        />
      </Box>

      <Paper
        elevation={1}
        sx={{
          width: 360,
          px: 3,
          py: 4,
          bgcolor: "#161b22",
          border: "1px solid #30363d",
          borderRadius: 2,
          color: "#c9d1d9",
        }}
      >
        <form onSubmit={handleSubmit(onSubmit)} noValidate>
          <Stack spacing={2}>
            <Typography variant="h6" textAlign="center" mb={1}>
              Sign in to BugNEst
            </Typography>

            <TextField
              label="Username or email address"
              fullWidth
              variant="filled"
              InputProps={{ disableUnderline: true }}
              sx={{
                bgcolor: "#0d1117",
                input: { color: "#c9d1d9" },
                label: { color: "#8b949e" },
              }}
              error={!!errors.identifier}
              helperText={errors.identifier?.message}
              {...register("identifier", { required: "Identifier wajib diisi" })}
            />

            <Box>
              <TextField
                label="Password"
                type="password"
                fullWidth
                variant="filled"
                InputProps={{ disableUnderline: true }}
                sx={{
                  bgcolor: "#0d1117",
                  input: { color: "#c9d1d9" },
                  label: { color: "#8b949e" },
                }}
                error={!!errors.password}
                helperText={errors.password?.message}
                {...register("password", { required: "Password wajib diisi" })}
              />
              <Box display="flex" justifyContent="flex-end" mt={0.5}>
                <Link href="#" underline="hover" sx={{ color: "#58a6ff", fontSize: 13 }}>
                  Forgot password?
                </Link>
              </Box>
            </Box>

            <Button
              type="submit"
              variant="contained"
              disabled={isPending}
              fullWidth
              sx={{
                mt: 1,
                bgcolor: "#238636",
                "&:hover": { bgcolor: "#2ea043" },
                fontWeight: "bold",
              }}
            >
              {isPending ? <CircularProgress size={24} color="inherit" /> : "Sign in"}
            </Button>
          </Stack>
        </form>
      </Paper>

      {/* Footer: Sign up */}
      <Paper
        elevation={0}
        sx={{
          mt: 2,
          px: 3,
          py: 2,
          width: 360,
          bgcolor: "#0d1117",
          border: "1px solid #30363d",
          borderRadius: 2,
          textAlign: "center",
        }}
      >
        <Typography variant="body2" color="#8b949e">
          New to YourApp?{" "}
          <Link
  component="button"
  onClick={() => navigate("/register")}
  sx={{ color: "#58a6ff" }}
>
  Create an account
</Link>

        </Typography>
      </Paper>
    </Box>
  );
}
