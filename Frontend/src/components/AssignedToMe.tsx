import React, { useEffect, useState } from 'react';
import {
  IconButton, Badge, Popover, Typography, List, ListItem, ListItemText,
  CircularProgress, Divider, Box, Tooltip, InputBase
} from '@mui/material';
import AssignmentIcon from '@mui/icons-material/Assignment';
import SearchIcon from '@mui/icons-material/Search';
import OpenInNewIcon from '@mui/icons-material/OpenInNew';
import { useTheme } from '@mui/material/styles';

interface IssueDto {
  id: string;
  issueCode: string;
  title: string;
  status: string;
  priority: string;
  createdAt: string;
}

const AssignedToMePopover = () => {
  const [issues, setIssues] = useState<IssueDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [search, setSearch] = useState('');
  const theme = useTheme();

  const open = Boolean(anchorEl);

  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
    setSearch('');
  };

const filteredIssues = issues.filter(issue =>

    issue.title.toLowerCase().includes(search.toLowerCase())
  );

  useEffect(() => {
    const fetchIssues = async () => {
      try {
        const res = await fetch('https://localhost:5001/api/Issue/assigned', {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        });
        const data = await res.json();
        const sorted = data.sort((a: IssueDto, b: IssueDto) =>
          new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
        );
        setIssues(sorted);
      } catch (err) {
        console.error(err);
      } finally {
        setLoading(false);
      }
    };
    fetchIssues();
  }, []);

  if (loading) {
    return (
      <IconButton disabled>
        <CircularProgress size={20} />
      </IconButton>
    );
  }

  return (
    <>

     {issues.length > 0 && (
  <Tooltip title="Assigned to you">
    <IconButton onClick={handleClick}>
      <Badge badgeContent={issues.length} color="secondary">
        <AssignmentIcon color="action" />
      </Badge>
    </IconButton>
  </Tooltip>
)}


      <Popover
        open={open}
        anchorEl={anchorEl}
        onClose={handleClose}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
        transformOrigin={{ vertical: 'top', horizontal: 'right' }}
        PaperProps={{ sx: { width: 350, p: 1 } }}
      >
        <Typography variant="h6" sx={{ px: 2, pt: 1, color: theme.palette.primary.main }}>
          Assigned Issues
        </Typography>
        <Divider sx={{ my: 1 }} />

        <Box sx={{ display: 'flex', alignItems: 'center', px: 2, pb: 1 }}>
          <SearchIcon sx={{ color: 'text.secondary', mr: 1 }} />
          <InputBase
            placeholder="Search issues…"
            value={search}
            onChange={(e) => setSearch(e.target.value)}
            fullWidth
            sx={{ fontSize: 14 }}
          />
        </Box>

        <List dense>
          {filteredIssues.length === 0 ? (
            <Typography sx={{ px: 2, py: 2, fontSize: 14, color: theme.palette.text.secondary }}>
              No matching issues
            </Typography>
          ) : (
            filteredIssues.map((issue) => (
              <ListItem key={issue.id} button>
                <ListItemText
                  primary={issue.title}
                  secondary={`#${issue.issueCode} - ${issue.priority}`}
                />
              </ListItem>
            ))
          )}
        </List>

        <Box sx={{ display: 'flex', justifyContent: 'flex-end', px: 2, pt: 1 }}>
          <Tooltip title="View all assigned issues">
            <IconButton
              size="small"
              href="/assigned-to-me"
              sx={{ color: theme.palette.primary.main }}
            >
              <OpenInNewIcon fontSize="small" />
            </IconButton>
          </Tooltip>
        </Box>
      </Popover>
    </>
  );
};

export default AssignedToMePopover;
