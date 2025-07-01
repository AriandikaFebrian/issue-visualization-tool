import { createContext, useContext, useState, type ReactNode } from "react";

type AuthMode = "login" | "register";

interface AuthContextType {
  authMode: AuthMode;
  setAuthMode: (mode: AuthMode) => void;
  toggleAuthMode: () => void;
}

// Default value saat tidak ada provider (optional/fallback)
const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [authMode, setAuthMode] = useState<AuthMode>("login");

  const toggleAuthMode = () => {
    setAuthMode(prev => (prev === "login" ? "register" : "login"));
  };

  return (
    <AuthContext.Provider value={{ authMode, setAuthMode, toggleAuthMode }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};
