import {
  Box,
  Typography,
  CircularProgress,
  TextField,
  Avatar,
  Chip,
  Link,
  Stack,
  Divider,
  IconButton,
} from '@mui/material';
import { useState } from 'react';
import { usePublicProjects } from '../hooks/usePublicProjects';
import ProjectSummaryList from './ProjectSummaryList';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import ExpandLessIcon from '@mui/icons-material/ExpandLess';
import Tooltip from '@mui/material/Tooltip';


const PublicProjectFeedList: React.FC = () => {
  const { projects, loading, error } = usePublicProjects();
  const [searchTerm, setSearchTerm] = useState('');
  const [showAll, setShowAll] = useState(false);
  const [isExpanding, setIsExpanding] = useState(false);

  if (loading) return <CircularProgress sx={{ m: 4 }} />;
  if (error) return <Typography color="error">Gagal memuat proyek publik.</Typography>;

  const filtered = projects
    .filter(
      (p) =>
        p.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
        p.description.toLowerCase().includes(searchTerm.toLowerCase())
    )
    .sort((a, b) => new Date(b.updatedAt).getTime() - new Date(a.updatedAt).getTime());

  const visibleProjects = showAll ? filtered : filtered.slice(0, 2);

  const handleToggleShowAll = () => {
    if (!showAll) {
      setIsExpanding(true);
      setTimeout(() => {
        setShowAll(true);
        setIsExpanding(false);
      }, 600); // delay animasi loading 600ms
    } else {
      setShowAll(false);
    }
  };

  return (
    <Box
      sx={{
        p: 2,
        borderRadius: 2,
        bgcolor: '#161b22',
        color: '#c9d1d9',
        border: '1px solid #30363d',
        height: '100%',
        maxHeight: '100%',
        overflowY: 'auto',
      }}
    >
      <Typography variant="subtitle2" fontWeight="bold" mb={1}>
        🧭 Public Project Feed
      </Typography>

      <TextField
        fullWidth
        size="small"
        label="Cari Proyek"
        value={searchTerm}
        onChange={(e) => setSearchTerm(e.target.value)}
        sx={{
          mb: 2,
          input: { color: '#c9d1d9', fontSize: '0.75rem' },
          label: { color: '#8b949e', fontSize: '0.75rem' },
          '& .MuiOutlinedInput-root': {
            '& fieldset': { borderColor: '#30363d' },
            '&:hover fieldset': { borderColor: '#58a6ff' },
            '&.Mui-focused fieldset': { borderColor: '#58a6ff' },
          },
        }}
      />

      {visibleProjects.map((project, idx) => (
        <Box key={project.id} sx={{ mb: 2 }}>
          <Typography fontWeight="bold" fontSize="0.85rem">
            {project.name}
          </Typography>
          <Typography fontSize="0.75rem" color="#8b949e" noWrap>
            {project.description}
          </Typography>

          <Stack direction="row" spacing={1} alignItems="center" mt={0.5}>
            <Avatar
              src={
                project.profilePictureUrl
                  ? project.profilePictureUrl.startsWith('http')
                    ? project.profilePictureUrl
                    : `https://localhost:5001${project.profilePictureUrl}`
                  : undefined
              }
              alt={project.ownerName}
              sx={{
                width: 20,
                height: 20,
                fontSize: 10,
                bgcolor: '#58a6ff',
              }}
            >
              {!project.profilePictureUrl && project.ownerName[0]?.toUpperCase()}
            </Avatar>

            <Typography variant="caption" color="#8b949e" fontSize="0.7rem">
              {project.ownerName}
            </Typography>
            <Chip
              label="React"
              variant="outlined"
              size="small"
              sx={{
                borderColor: '#30363d',
                color: '#c9d1d9',
                fontSize: '0.65rem',
                height: 18,
              }}
            />
          </Stack>

          <Link
            href={project.repositoryUrl}
            target="_blank"
            rel="noopener"
            underline="hover"
            sx={{ fontSize: '0.7rem', color: '#58a6ff', mt: 0.5, display: 'inline-block' }}
          >
            🔗 Repository
          </Link>

          {idx < visibleProjects.length - 1 && <Divider sx={{ my: 1, borderColor: '#30363d' }} />}
        </Box>
      ))}

      {/* Show More / Less with loading animation */}
      {filtered.length > 2 && (
  <Box sx={{ display: 'flex', justifyContent: 'center', mt: 1 }}>
    {isExpanding ? (
      <CircularProgress size={16} sx={{ color: '#58a6ff' }} />
    ) : (
      <Tooltip title={showAll ? 'Show Less' : 'Show More'}>
        <IconButton
          onClick={handleToggleShowAll}
          size="small"
          sx={{ color: '#58a6ff' }}
        >
          {showAll ? <ExpandLessIcon fontSize="small" /> : <ExpandMoreIcon fontSize="small" />}
        </IconButton>
      </Tooltip>
    )}
  </Box>
)}

      {/* Ringkasan Proyek */}
      <Box mt={2}>
        <ProjectSummaryList />
      </Box>
    </Box>
  );
};

export default PublicProjectFeedList;
