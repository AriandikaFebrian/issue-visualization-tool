import { Box, Link, Stack, Typography } from "@mui/material";

export default function FooterGitHubStyle() {
  const links = [
    { label: "Terms", href: "#" },
    { label: "Privacy", href: "#" },
    { label: "Docs", href: "#" },
    { label: "Contact Support", href: "#" },
    { label: "Manage cookies", href: "#" },
    { label: "Do not share my personal information", href: "#" },
  ];

  return (
    <Box
      component="footer"
      width="100%"
      py={2}
      sx={{
        backgroundColor: "#0d1117",
        borderTop: "1px solid #30363d",
        textAlign: "center",
      }}
    >
      <Stack
        direction="row"
        spacing={2}
        justifyContent="center"
        flexWrap="wrap"
        sx={{ px: 2, mb: 1 }}
      >
        {links.map((link, index) => (
          <Link
            key={index}
            href={link.href}
            underline="hover"
            sx={{
              color: "#8b949e",
              fontSize: 13,
              "&:hover": { color: "#58a6ff" },
            }}
          >
            {link.label}
          </Link>
        ))}
      </Stack>

      <Typography variant="body2" sx={{ color: "#6e7681", fontSize: 12 }}>
        © {new Date().getFullYear()} BugNEst, Inc. 
      </Typography>
    </Box>
  );
}
