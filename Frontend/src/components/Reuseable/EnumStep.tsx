import { Typography, Paper, Grid, useTheme } from "@mui/material";
import { motion } from "framer-motion";

interface EnumStepProps {
  label: string;
  options: string[];
  value: string;
  onChange: (val: string) => void;
  autoNext?: () => void;
}

export default function EnumStep({
  label,
  options,
  value,
  onChange,
  autoNext,
}: EnumStepProps) {
  const theme = useTheme();

  return (
    <>
      <Typography variant="subtitle1" fontWeight="bold" gutterBottom>
        {label}
      </Typography>

      <Grid container spacing={2} justifyContent="center">
        {options.filter(Boolean).map((opt) => (
          <Grid item key={opt} xs={12} sm={6} md={4} lg={4}>
            <motion.div whileTap={{ scale: 0.97 }} whileHover={{ scale: 1.02 }}>
              <Paper
  onClick={() => {
    onChange(opt);
    if (autoNext) autoNext();
  }}
  elevation={value === opt ? 4 : 1}
  sx={{
    width: "200px",
    height: 50,
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    borderRadius: 2,
    bgcolor: value === opt ? "#238636" : "#161b22",
    color: value === opt ? "#ffffff" : "#c9d1d9",
    fontWeight: 600,
    cursor: "pointer",
    border: `1px solid ${value === opt ? "#238636" : "#30363d"}`,
    transition: "all 0.2s ease-in-out",
    "&:hover": {
      bgcolor: value === opt ? "#2ea043" : "#21262d",
      borderColor: "#58a6ff",
    },
  }}
>
  {opt}
</Paper>

            </motion.div>
          </Grid>
        ))}
      </Grid>
    </>
  );
}
