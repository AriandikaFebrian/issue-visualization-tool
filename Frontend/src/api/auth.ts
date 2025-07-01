import axiosInstance from "../lib/axiosInstance";
import type { RegisterResponse } from "../types/auth";
export const registerUser = async (data: FormData): Promise<RegisterResponse> => {
  const response = await axiosInstance.post("/api/Auth/register", data, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
  return response.data;
};

import type { LoginFormInputs, LoginResponse } from "../types/auth";

export const loginUser = async (data: LoginFormInputs): Promise<LoginResponse> => {
  const response = await axiosInstance.post("/api/Auth/login", data, {
    headers: {
      "Content-Type": "application/json",
    },
  });
  return response.data;
};
