// theme.ts
import { createTheme } from '@mui/material/styles';

const theme = createTheme({
  palette: {
    mode: 'dark',
    background: {
      default: '#0d0d0d',     // sangat gelap untuk background utama
      paper: '#1a1a1a',       // kartu atau container
    },
    primary: {
      main: '#ffffff',        // tombol/teks utama jadi putih bersih
      contrastText: '#000000',
    },
    secondary: {
      main: '#aaaaaa',        // teks sekunder abu netral
    },
    text: {
      primary: '#f5f5f5',     // putih terang untuk teks utama
      secondary: '#aaaaaa',   // abu netral untuk teks sekunder
    },
    divider: '#2a2a2a',       // garis batas halus
    error: {
      main: '#e57373',        // merah soft (bisa tetap gelap)
    },
    warning: {
      main: '#ffb74d',        // kuning lembut
    },
    success: {
      main: '#81c784',        // hijau lembut
    },
    info: {
      main: '#90caf9',        // biru lembut
    },
  },
  typography: {
    fontFamily: 'Inter, Roboto, Arial, sans-serif',
    h5: {
      fontWeight: 600,
    },
    body1: {
      fontSize: '0.95rem',
    },
    button: {
      textTransform: 'none',
    },
  },
  components: {
    MuiButton: {
      styleOverrides: {
        root: {
          textTransform: 'none',
          borderRadius: 8,
          padding: '6px 16px',
          color: '#ffffff',
          backgroundColor: '#333',
          '&:hover': {
            backgroundColor: '#444',
          },
        },
      },
    },
    MuiPaper: {
      styleOverrides: {
        root: {
          backgroundImage: 'none',
          backgroundColor: '#1a1a1a',
          borderRadius: 12,
        },
      },
    },
    MuiAppBar: {
      styleOverrides: {
        root: {
          backgroundColor: '#0d0d0d',
          borderBottom: '1px solid #2a2a2a',
        },
      },
    },
    MuiDrawer: {
      styleOverrides: {
        paper: {
          backgroundColor: '#121212',
          color: '#f5f5f5',
        },
      },
    },
    MuiTooltip: {
      styleOverrides: {
        tooltip: {
          backgroundColor: '#2a2a2a',
          color: '#ffffff',
        },
      },
    },
  },
});

export default theme;
