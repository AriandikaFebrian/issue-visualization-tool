import {
  Avatar,
  Box,
  Button,
  CircularProgress,
  Paper,
  Stack,
  TextField,
  Typography,
} from "@mui/material";
import { useForm } from "react-hook-form";
import { useRegister } from "../hooks/useRegister";
import type { RegisterFormInputs } from "../types/auth";
import { useEffect, useState } from "react";
import PersonIcon from "@mui/icons-material/Person";
import { DepartmentType, PositionType, UserRole } from "../types/enums";
import EnumStep from "./Reuseable/EnumStep";
import { motion, AnimatePresence } from "framer-motion";
import { toast } from "sonner";

const steps = ["Foto & Akun", "Informasi Personal", "Role", "Department", "Position"];

export default function RegisterForm({
  onStepChange,
}: {
  onStepChange?: (step: number) => void;
}) {
  const {
    register,
    handleSubmit,
    setValue,
    watch,
    reset,
  } = useForm<RegisterFormInputs>({
    defaultValues: {
      Username: "",
      Email: "",
      Password: "",
      Role: UserRole[0] || "Admin",
      FullName: "",
      PhoneNumber: "",
      Department: DepartmentType[0] || "IT",
      Position: PositionType[0] || "Staff",
      ProfilePicture: null,
    },
  });

  const { mutateAsync, isPending } = useRegister();
  const [activeStep, setActiveStep] = useState(0);
  const [direction, setDirection] = useState<1 | -1>(1);
  const selectedFile = watch("ProfilePicture");

  useEffect(() => {
    onStepChange?.(activeStep);
  }, [activeStep]);

  const setActiveStepWithNotify = (step: number) => {
    onStepChange?.(step);
    setActiveStep(step);
  };

  const nextStep = () => {
    setDirection(1);
    setActiveStepWithNotify(Math.min(activeStep + 1, steps.length - 1));
  };

  const prevStep = () => {
    setDirection(-1);
    setActiveStepWithNotify(Math.max(activeStep - 1, 0));
  };

  useEffect(() => {
    const handleKeyDown = (e: KeyboardEvent) => {
      if (e.key === "Enter") {
        e.preventDefault();
        if (activeStep < steps.length - 1) {
          nextStep();
        }
      }
    };
    window.addEventListener("keydown", handleKeyDown);
    return () => window.removeEventListener("keydown", handleKeyDown);
  }, [activeStep]);

  const onSubmit = async (data: RegisterFormInputs) => {
    const formData = new FormData();
    formData.append("Username", data.Username);
    formData.append("Email", data.Email);
    formData.append("Password", data.Password);
    formData.append("Role", data.Role);
    formData.append("FullName", data.FullName);
    formData.append("PhoneNumber", data.PhoneNumber);
    formData.append("Department", data.Department);
    formData.append("Position", data.Position);
    if (data.ProfilePicture instanceof File) {
      formData.append("ProfilePicture", data.ProfilePicture);
    }

    try {
      const res = await mutateAsync(formData);
      toast.success(res.message || "Pendaftaran berhasil");
      toast.message(`NRP Anda: ${res.nrp}`, {
        description: "Simpan NRP ini untuk login.",
        action: {
          label: "Salin",
          onClick: () => navigator.clipboard.writeText(res.nrp),
        },
      });

      reset();
      setActiveStepWithNotify(0);
    } catch (err: any) {
      toast.error("Pendaftaran gagal", {
        description: "Silakan periksa kembali data Anda atau coba beberapa saat lagi.",
      });
    }
  };

  const stepContent = (step: number) => {
    const commonPaperSx = { p: 3, width: "100%", boxSizing: "border-box" };
    switch (step) {
      case 0:
        return (
          <Paper sx={commonPaperSx}>
            <Stack spacing={3} alignItems="center">
              <Avatar
                src={selectedFile ? URL.createObjectURL(selectedFile) : ""}
                sx={{ width: 100, height: 100, cursor: "pointer" }}
                onClick={() => document.getElementById("profile-upload")?.click()}
              >
                {!selectedFile && <PersonIcon fontSize="large" />}
              </Avatar>
              <input
                id="profile-upload"
                type="file"
                hidden
                accept="image/*"
                onChange={(e) => {
                  const file = e.target.files?.[0] || null;
                  setValue("ProfilePicture", file);
                }}
              />
              {selectedFile && (
                <Typography variant="body2" color="text.secondary">
                  {selectedFile.name}
                </Typography>
              )}
              <TextField label="Username" fullWidth {...register("Username")} />
              <TextField label="Email" fullWidth {...register("Email")} />
              <TextField label="Password" type="password" fullWidth {...register("Password")} />
            </Stack>
          </Paper>
        );

      case 1:
        return (
          <Paper sx={commonPaperSx}>
            <Stack spacing={3}>
              <TextField label="Full Name" fullWidth {...register("FullName")} />
              <TextField label="Phone Number" fullWidth {...register("PhoneNumber")} />
            </Stack>
          </Paper>
        );

      case 2:
        return (
          <Paper sx={commonPaperSx}>
            <EnumStep
              label="Pilih Role Anda"
              options={UserRole.filter(Boolean)}
              value={watch("Role")}
              onChange={(val) => setValue("Role", val)}
            />
          </Paper>
        );

      case 3:
        return (
          <Paper sx={commonPaperSx}>
            <EnumStep
              label="Pilih Departemen Anda"
              options={DepartmentType.filter(Boolean)}
              value={watch("Department")}
              onChange={(val) => setValue("Department", val)}
            />
          </Paper>
        );

      case 4:
        return (
          <Paper sx={commonPaperSx}>
            <EnumStep
              label="Pilih Posisi Anda"
              options={PositionType.filter(Boolean)}
              value={watch("Position")}
              onChange={(val) => setValue("Position", val)}
            />
          </Paper>
        );

      default:
        return null;
    }
  };

  const variants = {
    enter: (dir: number) => ({ x: dir > 0 ? 300 : -300, opacity: 0 }),
    center: { x: 0, opacity: 1 },
    exit: (dir: number) => ({ x: dir > 0 ? -300 : 300, opacity: 0 }),
  };

  return (
    <Box component="form" onSubmit={handleSubmit(onSubmit)} noValidate sx={{ position: "relative" }}>
      <Stack spacing={3}>
<Box
  sx={{
    minHeight: 480,
    height: 480,
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    position: "relative",
  }}
>

          <AnimatePresence mode="wait" initial={false} custom={direction}>
          <motion.div
  key={activeStep}
  custom={direction}
  variants={variants}
  initial="enter"
  animate="center"
  exit="exit"
  transition={{ duration: 0.4 }}
  style={{ width: "100%" }}
>
  {stepContent(activeStep)}
</motion.div>
          </AnimatePresence>
        </Box>

        <Stack direction="row" spacing={2} justifyContent="center">
          {activeStep > 0 && (
            <Button onClick={prevStep} variant="outlined">
              Kembali
            </Button>
          )}
          {activeStep === steps.length - 1 && (
            <Button type="submit" variant="contained" disabled={isPending}>
              {isPending ? <CircularProgress size={24} /> : "Daftar"}
            </Button>
          )}
        </Stack>

        <Typography variant="caption" textAlign="center" mt={4} color="text.secondary">
          Dengan mendaftar, Anda menyetujui {" "}
          <a href="/terms" style={{ color: "#58a6ff" }}>Syarat & Ketentuan</a> serta {" "}
          <a href="/privacy" style={{ color: "#58a6ff" }}>Kebijakan Privasi</a> BugNEst.
        </Typography>
      </Stack>
    </Box>
  );
}
