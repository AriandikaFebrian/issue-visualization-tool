import { useMutation } from "@tanstack/react-query";
import { loginUser } from "../api/auth";
import type { LoginFormInputs, LoginResponse } from "../types/auth";

export const useLogin = () => {
  return useMutation<LoginResponse, Error, LoginFormInputs>({
    mutationFn: loginUser,
  });
};
