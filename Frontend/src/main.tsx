import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import { CssBaseline, ThemeProvider, GlobalStyles } from "@mui/material";
import { QueryClientProvider } from "@tanstack/react-query";
import queryClient from "./lib/queryClient";
import theme from "./theme";
import { BrowserRouter } from "react-router-dom";
import { AuthProvider } from "./context/AuthContext";
import { Toaster } from "sonner";

const globalScrollbarStyles = (
  <GlobalStyles
    styles={{
      '*': {
        scrollbarWidth: 'thin',
        scrollbarColor: '#30363d #0d1117',
      },
      '*::-webkit-scrollbar': {
        width: '6px',
        height: '6px',
      },
      '*::-webkit-scrollbar-track': {
        background: '#0d1117',
      },
      '*::-webkit-scrollbar-thumb': {
        backgroundColor: '#30363d',
        borderRadius: '6px',
        border: '1px solid transparent',
      },
      '*::-webkit-scrollbar-thumb:hover': {
        backgroundColor: '#484f58',
      },
    }}
  />
);

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <QueryClientProvider client={queryClient}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        {globalScrollbarStyles}
        <BrowserRouter>
          <AuthProvider>
            <App />
            <Toaster richColors position="top-center" />
          </AuthProvider>
        </BrowserRouter>
      </ThemeProvider>
    </QueryClientProvider>
  </React.StrictMode>
);
