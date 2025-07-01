// components/CreateTagModal.tsx
import {
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  MenuItem,
  Snackbar,
  Stack,
  TextField,
} from '@mui/material';
import axios from 'axios';
import { useEffect, useState } from 'react';

interface Props {
  open: boolean;
  onClose: () => void;
}

interface Project {
  projectCode: string;
  name: string;
}

const CreateTagModal = ({ open, onClose }: Props) => {
  const [projects, setProjects] = useState<Project[]>([]);
  const [name, setName] = useState('');
  const [color, setColor] = useState('');
  const [projectCode, setProjectCode] = useState('');
  const [category, setCategory] = useState('');
  const [snackbarOpen, setSnackbarOpen] = useState(false);
  const [createdTagId, setCreatedTagId] = useState<string | null>(null);

  useEffect(() => {
    if (open) {
      axios
        .get('https://localhost:5001/api/Project/mine', {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        })
        .then((res) => setProjects(res.data))
        .catch((err) => console.error('Failed to fetch projects', err));
    }
  }, [open]);

  const handleSubmit = async () => {
    try {
      const res = await axios.post(
        'https://localhost:5001/api/Tag',
        {
          name,
          color,
          projectCode,
          category,
        },
        {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        }
      );
      setCreatedTagId(res.data.tagId);
      setSnackbarOpen(true);
      onClose();
      setName('');
      setColor('');
      setProjectCode('');
      setCategory('');
    } catch (err) {
      console.error('Failed to create tag:', err);
    }
  };

  return (
    <>
      <Dialog open={open} onClose={onClose} maxWidth="xs" fullWidth>
        <DialogTitle>Create New Tag</DialogTitle>
        <DialogContent>
          <Stack spacing={2} mt={1}>
            <TextField
              label="Tag Name"
              fullWidth
              value={name}
              onChange={(e) => setName(e.target.value)}
            />
            <TextField
              label="Color"
              fullWidth
              value={color}
              onChange={(e) => setColor(e.target.value)}
              placeholder="e.g., #FF5733"
            />
            <TextField
              label="Project Code"
              select
              fullWidth
              value={projectCode}
              onChange={(e) => setProjectCode(e.target.value)}
            >
              {projects.map((project) => (
                <MenuItem key={project.projectCode} value={project.projectCode}>
                  {project.projectCode}
                </MenuItem>
              ))}
            </TextField>
            <TextField
              label="Category"
              fullWidth
              value={category}
              onChange={(e) => setCategory(e.target.value)}
            />
          </Stack>
        </DialogContent>
        <DialogActions>
          <Button onClick={onClose}>Cancel</Button>
          <Button variant="contained" onClick={handleSubmit}>
            Create Tag
          </Button>
        </DialogActions>
      </Dialog>

      <Snackbar
        open={snackbarOpen}
        autoHideDuration={6000}
        onClose={() => setSnackbarOpen(false)}
        message={`Tag created with ID: ${createdTagId}`}
      />
    </>
  );
};

export default CreateTagModal;
