import React, { createContext, useContext, useState } from "react";

interface ProfileData {
  username: string;
  fullName: string;
  profilePictureUrl: string;
  phoneNumber: string;
  department: number;
  position: number;
}

interface ProfileContextType {
  activePanel: string;
  setActivePanel: (panel: string) => void;
  profile: ProfileData;
  setProfile: React.Dispatch<React.SetStateAction<ProfileData>>;
}

const defaultProfile: ProfileData = {
  username: "",
  fullName: "",
  profilePictureUrl: "",
  phoneNumber: "",
  department: 0,
  position: 0,
};

const ProfileContext = createContext<ProfileContextType | undefined>(undefined);

export const ProfileProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [activePanel, setActivePanel] = useState("edit");
  const [profile, setProfile] = useState<ProfileData>(defaultProfile);

  return (
    <ProfileContext.Provider value={{ activePanel, setActivePanel, profile, setProfile }}>
      {children}
    </ProfileContext.Provider>
  );
};

export const useProfileContext = (): ProfileContextType => {
  const context = useContext(ProfileContext);
  if (!context) throw new Error("useProfileContext must be used within ProfileProvider");
  return context;
};
