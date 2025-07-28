import { useEffect, useState } from 'react';
import axios from 'axios';
import {
  Avatar,
  Box,
  Button,
  Chip,
  CircularProgress,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Divider,
  InputBase,
  Paper,
  Stack,
  Typography,
  useTheme,
} from '@mui/material';
import WorkIcon from '@mui/icons-material/Work';
import BugReportIcon from '@mui/icons-material/BugReport';
import UpdateIcon from '@mui/icons-material/Update';
import SearchIcon from '@mui/icons-material/Search';
import AddIcon from '@mui/icons-material/Add';
import FolderOpenIcon from '@mui/icons-material/FolderOpen';
import TagCreateModal from './CreateTagModal';
import LabelIcon from '@mui/icons-material/Label';
import { useNavigate } from "react-router-dom";



interface Activity {
  id: string;
  userId: string;
  username: string;
  userProfileUrl: string;
  action: string;
  targetEntityId: string;
  targetEntityType: string;
  summary: string;
  createdAt: string;
  previousValue?: string;
  newValue?: string;
  note?: string;
  targetEntityCode?: string;
  targetEntityName?: string;
  ipAddress?: string;
  sourcePlatform?: string;
}

const ActivityFeed = () => {
  const [activities, setActivities] = useState<Activity[]>([]);
  const [loading, setLoading] = useState(true);
  const [search, setSearch] = useState('');
  const [selectedActivity, setSelectedActivity] = useState<Activity | null>(null);
  const [modalOpen, setModalOpen] = useState(false);
  const [detailLoading, setDetailLoading] = useState(false);
  const theme = useTheme();
  const [tagModalOpen, setTagModalOpen] = useState(false);


  useEffect(() => {
    const loadActivities = async () => {
      try {
        const res = await axios.get('https://localhost:5001/api/activities?page=1&pageSize=20', {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        });
        setActivities(res.data.data);
      } catch (err) {
        console.error('Failed to fetch activities', err);
      } finally {
        setLoading(false);
      }
    };

    loadActivities();
  }, []);

  const navigate = useNavigate();
  

  const getIcon = (action: string) => {
    switch (action) {
      case 'CreatedProject': return <WorkIcon fontSize="small" />;
      case 'CreatedIssue': return <BugReportIcon fontSize="small" />;
      case 'ChangedIssueStatus': return <UpdateIcon fontSize="small" />;
      default: return <WorkIcon fontSize="small" />;
    }
  };

  const getActionColor = (action: string) => {
    switch (action) {
      case 'CreatedProject': return 'success';
      case 'CreatedIssue': return 'info';
      case 'ChangedIssueStatus': return 'warning';
      default: return 'default';
    }
  };

  const filteredActivities = activities.filter((a) =>
    a.summary.toLowerCase().includes(search.toLowerCase())
  );

  const handleOpenModal = async (activityId: string) => {
    setModalOpen(true);
    setDetailLoading(true);
    try {
      const res = await axios.get(`https://localhost:5001/api/activities/${activityId}`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
      });
      setSelectedActivity(res.data);
    } catch (err) {
      console.error('Gagal mengambil detail aktivitas:', err);
    } finally {
      setDetailLoading(false);
    }
  };

  const handleCloseModal = () => {
    setModalOpen(false);
    setSelectedActivity(null);
  };

  return (
    <Box width="100%">
      {/* Search + Action Buttons */}
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={2} gap={2}>
        <Paper
          sx={{
            p: '2px 8px',
            display: 'flex',
            alignItems: 'center',
            width: 300,
            borderRadius: 2,
            backgroundColor: theme.palette.mode === 'dark' ? '#1e1e1e' : '#f5f5f5',
            border: `1px solid ${theme.palette.divider}`,
            '&:hover': {
              backgroundColor: '#1f2937',
            },
          }}
        >
          <SearchIcon sx={{ mr: 1 }} />
          <InputBase
            placeholder="Search activity..."
            value={search}
            onChange={(e) => setSearch(e.target.value)}
            sx={{ flex: 1 }}
          />
        </Paper>

        <Stack direction="row" spacing={1}>
  <Button variant="contained" startIcon={<AddIcon />}>Add Issue</Button>
<Button
  variant="outlined"
  startIcon={<FolderOpenIcon />}
  onClick={() => navigate("/my-project?create=true")}
>
  Create Project
</Button>

 <Button
  variant="outlined"
  color="secondary"
  startIcon={<LabelIcon />}
  onClick={() => setTagModalOpen(true)}
>
  Create Tag
</Button>

</Stack>
        <TagCreateModal open={tagModalOpen} onClose={() => setTagModalOpen(false)} />
      </Box>

      <Typography variant="h6" fontWeight={600} gutterBottom>🕒 Activity Log</Typography>

      {loading ? (
        <Box display="flex" justifyContent="center" mt={4}>
          <CircularProgress />
        </Box>
      ) : (
        <Stack spacing={2}>
          {filteredActivities.map((activity) => (
           <Paper
  key={activity.id}
  onClick={() => handleOpenModal(activity.id)}
  sx={{
    p: 2,
    borderRadius: 2,
    backgroundColor: theme.palette.mode === 'dark' ? '#1e1e1e' : '#fafafa',
    border: `1px solid ${theme.palette.divider}`,
    cursor: 'pointer',
    '&:hover': {
      boxShadow: '0px 4px 12px rgba(0,0,0,0.15)',
      backgroundColor: theme.palette.mode === 'dark' ? '#2a2a2a' : '#f0f0f0',
      transform: 'translateY(-2px)',
      transition: 'all 0.2s ease-in-out',
    },
  }}
>

              <Box display="flex" alignItems="center" gap={1}>
                <Avatar src={activity.userProfileUrl} alt={activity.username}>
                  {activity.username[0]}
                </Avatar>

                <Box>
                  <Typography variant="subtitle2" fontWeight={600}>
                    {activity.username}
                  </Typography>
                  <Typography variant="caption" color="text.secondary">
                    {new Date(activity.createdAt).toLocaleString()}
                  </Typography>
                </Box>

                <Chip
                  label={activity.action}
                  size="small"
                  icon={getIcon(activity.action)}
                  color={getActionColor(activity.action)}
                  sx={{ ml: 'auto' }}
                />
              </Box>

              <Box mt={2}>
                <Typography variant="body1" fontWeight={600}>
                  {activity.summary}
                </Typography>
              </Box>
            </Paper>
          ))}
        </Stack>
      )}

      {/* Detail Modal */}
      <Dialog open={modalOpen} onClose={handleCloseModal} maxWidth="sm" fullWidth>
        <DialogTitle>📋 Detail Aktivitas</DialogTitle>
        <DialogContent dividers>
          {detailLoading ? (
            <Box display="flex" justifyContent="center" py={3}>
              <CircularProgress />
            </Box>
          ) : selectedActivity ? (
            <Stack spacing={2}>
              <Box display="flex" alignItems="center" gap={2}>
                <Avatar src={selectedActivity.userProfileUrl}>
                  {selectedActivity.username[0]}
                </Avatar>
                <Box>
                  <Typography fontWeight={600}>{selectedActivity.username}</Typography>
                  <Typography variant="caption" color="text.secondary">
                    {new Date(selectedActivity.createdAt).toLocaleString()}
                  </Typography>
                </Box>
                <Chip
                  label={selectedActivity.action}
                  icon={getIcon(selectedActivity.action)}
                  color={getActionColor(selectedActivity.action)}
                  size="small"
                  sx={{ ml: 'auto' }}
                />
              </Box>

              <Divider />

              <Box display="flex" flexWrap="wrap" gap={2}>
                {[['ID', selectedActivity.id],
                  ['User ID', selectedActivity.userId],
                  ['Entity', `${selectedActivity.targetEntityType} - ${selectedActivity.targetEntityId}`],
                  ['Code', selectedActivity.targetEntityCode],
                  ['Name', selectedActivity.targetEntityName],
                  ['Summary', selectedActivity.summary],
                  ['IP Address', selectedActivity.ipAddress],
                  ['Platform', selectedActivity.sourcePlatform || 'N/A'],
                ].map(([label, value], index) => (
                  <Box key={index} width="calc(50% - 8px)">
                    <Typography variant="caption" color="text.secondary">
                      {label}
                    </Typography>
                    <Typography fontWeight={500}>{value}</Typography>
                  </Box>
                ))}
              </Box>

              {selectedActivity.previousValue && selectedActivity.newValue && (
                <Box>
                  <Typography variant="caption" color="text.secondary">
                    Status
                  </Typography>
                  <Box display="flex" alignItems="center" gap={1} mt={0.5}>
                    <Chip
                      label={selectedActivity.previousValue}
                      color="error"
                      variant="outlined"
                      size="small"
                    />
                    <Typography variant="body2">→</Typography>
                    <Chip
                      label={selectedActivity.newValue}
                      color="success"
                      variant="filled"
                      size="small"
                    />
                  </Box>
                </Box>
              )}

              {selectedActivity.note && (
                <Box>
                  <Typography variant="caption" color="text.secondary">
                    Note
                  </Typography>
                  <Typography fontStyle="italic">{selectedActivity.note}</Typography>
                </Box>
              )}
            </Stack>
          ) : (
            <Typography variant="body2" color="text.secondary">
              Data tidak tersedia.
            </Typography>
          )}
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloseModal}>Tutup</Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
};

export default ActivityFeed;
