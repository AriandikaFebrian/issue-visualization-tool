import { useMutation } from "@tanstack/react-query";
import { registerUser } from "../api/auth";
import type { RegisterResponse } from "../types/auth";

export const useRegister = () => {
  return useMutation<RegisterResponse, Error, FormData>({
    mutationFn: registerUser,
  });
};
