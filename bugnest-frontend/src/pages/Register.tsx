import { Box, Typography, IconButton, Collapse } from "@mui/material";
import RegisterForm from "../components/RegisterForm";
import { useState } from "react";
import { AnimatePresence, motion } from "framer-motion";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import ExpandLessIcon from "@mui/icons-material/ExpandLess";
import panelBg from "../assets/background1.jpg";

const stepDescriptions = [
  {
    title: "Unggah Foto & Buat Akun",
    description:
      "Gunakan foto profesional dan buat akun untuk mulai menggunakan BugNEst. Ini akan jadi identitas Anda di sistem.",
  },
  {
    title: "Lengkapi Data Pribadi",
    description:
      "Informasi ini membantu BugNEst mempersonalisasi pengalaman Anda di dalam tim dan memastikan keakuratan data.",
  },
  {
    title: "Pilih Role",
    description:
      "Tentukan peran Anda di dalam tim. Role mempengaruhi akses fitur dan tanggung jawab Anda di BugNEst.",
  },
  {
    title: "Pilih Departemen",
    description:
      "Departemen membantu mengorganisir tim dan pelaporan bug lebih terstruktur.",
  },
  {
    title: "Pilih Posisi",
    description:
      "Posisi menunjukkan jabatan Anda di organisasi, penting untuk pengelompokan tugas dan laporan.",
  },
];

// Ini array penjelasan "Kenapa BugNEst?" per step
const bugNestReasons = [
  [
    "BugNEst memudahkan kolaborasi laporan bug dengan tim.",
    "Membuat akun adalah langkah pertama untuk mulai berkontribusi.",
    "Profil profesional membuat identitas Anda kuat di komunitas.",
  ],
  [
    "Data pribadi lengkap mempercepat proses kolaborasi.",
    "Personalisasi pengalaman sesuai peran dan kebutuhan Anda.",
  ],
  [
    "Role menentukan akses fitur yang relevan untuk pekerjaan Anda.",
    "Mempermudah pembagian tugas dan tanggung jawab di tim.",
  ],
  [
    "Pengelompokan berdasarkan departemen membuat pelaporan terstruktur.",
    "Mempermudah tracking dan penyelesaian bug berdasarkan departemen.",
  ],
  [
    "Posisi menandakan jabatan dan tanggung jawab spesifik Anda.",
    "Mendukung sistem pelaporan dan manajemen tugas yang rapi.",
  ],
];

export default function RegisterPage() {
  const [activeStep, setActiveStep] = useState(0);
  const [showReasons, setShowReasons] = useState(false);

  const currentStep = stepDescriptions[activeStep];
  const currentReasons = bugNestReasons[activeStep] || [];

  return (
    <Box display="flex" height="100vh" sx={{ backgroundColor: "#0d1117" }}>
      {/* Kiri: Form */}
      <Box
        flex={1}
        display="flex"
        alignItems="center"
        justifyContent="center"
        p={4}
      >
        <Box maxWidth={480} width="100%">
          <RegisterForm onStepChange={(step) => setActiveStep(step)} />
        </Box>
      </Box>

      {/* Kanan: Panel Informasi */}
      <Box
        flex={1}
        display="flex"
        justifyContent="center"
        alignItems="center"
        sx={{
          position: "relative",
          overflow: "hidden",
        }}
      >
        {/* Background Image */}
        <Box
          sx={{
            position: "absolute",
            inset: 0,
            backgroundImage: `url(${panelBg})`,
            backgroundSize: "cover",
            backgroundPosition: "center",
            opacity: 0.3,
            zIndex: 0,
          }}
        />

        {/* Overlay Content */}
        <Box
          sx={{
            zIndex: 1,
            color: "white",
            textAlign: "center",
            px: 6,
            maxWidth: 500,
          }}
        >
          {/* Step Descriptions */}
          <AnimatePresence mode="wait">
            <motion.div
              key={activeStep}
              initial={{ opacity: 0, y: 20 }}
              animate={{ opacity: 1, y: 0 }}
              exit={{ opacity: 0, y: -20 }}
              transition={{ duration: 0.4 }}
            >
              <Typography variant="h4" fontWeight="bold" gutterBottom>
                {currentStep.title}
              </Typography>
              <Typography variant="body1" mb={3}>
                {currentStep.description}
              </Typography>
            </motion.div>
          </AnimatePresence>

          {/* Kenapa BugNEst - expandable */}
          <Box textAlign="left">
            <Box
              display="flex"
              alignItems="center"
              justifyContent="space-between"
              onClick={() => setShowReasons(!showReasons)}
              sx={{ cursor: "pointer" }}
            >
              <Typography variant="h6" fontWeight="bold">
                Kenapa BugNEst?
              </Typography>
              <IconButton sx={{ color: "white" }}>
                {showReasons ? <ExpandLessIcon /> : <ExpandMoreIcon />}
              </IconButton>
            </Box>

            <Collapse in={showReasons}>
              <Box mt={2}>
                {currentReasons.map((reason, i) => (
                  <Box key={i} display="flex" alignItems="center" mb={1}>
                    <Box
                      sx={{
                        width: 10,
                        height: 10,
                        border: "2px solid white",
                        borderRadius: "50%",
                        marginRight: 1.5,
                        flexShrink: 0,
                      }}
                    />
                    <Typography variant="body2">{reason}</Typography>
                  </Box>
                ))}
              </Box>
            </Collapse>
          </Box>
                {/* Link Masuk di kanan atas */}
       <Box sx={{ position: "absolute", top: 24, right: 24 }}>
  <Typography variant="body2" color="text.secondary">
    Sudah punya akun? <a href="/login" style={{ color: "#58a6ff" }}>Masuk</a>
  </Typography>
</Box>

        </Box>
      </Box>
    </Box>
  );
}
