import { Routes, Route, Navigate, Outlet, useNavigate } from "react-router-dom";
import RegisterPage from "./pages/Register";
import LoginPage from "./pages/LoginPage";
import HomePage from "./pages/HomePage";
import AuthPage from "./pages/AuthPage";
import Navbar from "./components/Reuseable/Navbar";
import Sidebar from "./components/Reuseable/Sidebar";
import { useEffect, useState } from "react";
import { Toolbar, Box } from "@mui/material";
import ProfilePage from "./pages/ProfilePage";
import { ProfileProvider } from "./context/ProfileContext";
import BugsPage from "./pages/BugsPage";
import MyProjectPage from "./pages/MyProjectPage";
function useAuth() {
  const token = localStorage.getItem("token");
  return Boolean(token);
}

function PrivateRoute() {
  const isAuth = useAuth();
  const navigate = useNavigate();
  const [openSidebar, setOpenSidebar] = useState(false);

  useEffect(() => {
    if (!isAuth) navigate("/login");
  }, [isAuth, navigate]);

  if (!isAuth) return null;

  return (
    <>
      {/* Sidebar (bisa collapse) */}
      <Sidebar
        open={openSidebar}
        onClose={() => setOpenSidebar(false)}
      />

      <Navbar
        token={localStorage.getItem("token") || ""}
        onOpenSidebar={() => setOpenSidebar(true)}
      />

      <Toolbar />

      <Box component="main" sx={{ px: 2 }}>
        <Outlet />
      </Box>
    </>
  );
}
function App() {
  return (
    <ProfileProvider>
      <Routes>
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/auth" element={<AuthPage />} />
        <Route path="/profile" element={<ProfilePage />} />

        {/* Halaman yang butuh login */}
        <Route element={<PrivateRoute />}>
          <Route path="/" element={<HomePage />} />
          <Route path="/bugs" element={<BugsPage />} />
          <Route path="/my-project" element={<MyProjectPage />} />
        </Route>

        {/* Fallback */}
        <Route path="*" element={<Navigate to="/register" replace />} />
      </Routes>
    </ProfileProvider>
  );
}

export default App;
