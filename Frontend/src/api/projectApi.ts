import axiosInstance from "../lib/axiosInstance";
import type { Project } from "../types/project";

export const fetchPublicProjects = async (): Promise<Project[]> => {
  const res = await axiosInstance.get<Project[]>('/api/Project/projects/public-feed');
  return res.data;
};
