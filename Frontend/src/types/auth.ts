export interface RegisterFormInputs {
  Username: string;
  Email: string;
  Password: string;
  Role: string;
  FullName: string;
  PhoneNumber: string;
  Department: string;
  Position: string;
  ProfilePicture: File | null;
}



export interface RegisterResponse {
  message: string;
  nrp: string;
}

export interface LoginFormInputs {
  identifier: string;
  password: string;
}

export interface LoginResponse {
  id: string;
  token: string;
  username: string;
  email: string;
  role: string;
  nrp: string;
  fullName: string;
  profilePictureUrl: string | null;
  department: string | null;
  position: string | null;
}

