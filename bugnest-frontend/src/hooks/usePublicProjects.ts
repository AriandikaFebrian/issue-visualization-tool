// 📁 src/hooks/usePublicProjects.ts
import { useEffect, useState } from 'react';
import type { Project } from '../types/project';
import { fetchPublicProjects } from '../api/projectApi';

export const usePublicProjects = () => {
  const [projects, setProjects] = useState<Project[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<Error | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const data = await fetchPublicProjects();
        setProjects(data);
      } catch (err) {
        console.error('Error fetching public projects:', err);
        setError(err as Error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  return { projects, loading, error };
};
