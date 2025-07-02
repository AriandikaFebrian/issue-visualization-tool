using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;
using BugNest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BugNest.Infrastructure.Repositories;

public class IssueRepository : IIssueRepository
{
    private readonly BugNestDbContext _context;

    public IssueRepository(BugNestDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Issue issue)
    {
        await _context.Issues.AddAsync(issue);
    }

    public async Task AddTagsAsync(List<IssueTag> tags)
    {
        await _context.IssueTags.AddRangeAsync(tags);
    }

    public async Task AddAssignedUsersAsync(List<AssignedUser> assignedUsers)
    {
        await _context.AssignedUsers.AddRangeAsync(assignedUsers);
    }

    public async Task<Issue?> GetByIdAsync(Guid id)
    {
        return await _context.Issues
            .Include(i => i.Tags).ThenInclude(t => t.Tag)
            .Include(i => i.AssignedUsers).ThenInclude(au => au.User)
            .Include(i => i.Comments)
            .Include(i => i.Creator)
            .Include(i => i.DuplicateOf)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<List<Issue>> GetByProjectIdAsync(Guid projectId)
    {
        return await _context.Issues
            .Where(i => i.ProjectId == projectId)
            .Include(i => i.Creator)
            .Include(i => i.Tags).ThenInclude(t => t.Tag)
            .ToListAsync();
    }

    public async Task<List<Issue>> GetIssuesWithDetailsByProjectIdAsync(Guid projectId)
    {
        return await _context.Issues
            .Where(i => i.ProjectId == projectId)
            .Include(i => i.Tags).ThenInclude(it => it.Tag)
            .Include(i => i.AssignedUsers).ThenInclude(au => au.User)
            .Include(i => i.Creator)
            .ToListAsync();
    }

    public async Task<List<Issue>> GetRecentIssuesAsync(int count)
{
    return await _context.Issues
        .Where(i => i.Status != IssueStatus.Closed)
        .OrderByDescending(i => i.CreatedAt)
        .Include(i => i.Project)
        .Take(count)
        .ToListAsync();
}

public async Task<List<Issue>> GetIssuesAssignedToUserAsync(Guid userId)
{
    return await _context.Issues
        .Where(i => i.AssignedUsers.Any(au => au.UserId == userId))
        .Include(i => i.Project)
        .Include(i => i.AssignedUsers).ThenInclude(au => au.User)
        .Include(i => i.Creator)
        .Include(i => i.Tags)
        .ToListAsync();
}



    public async Task<bool> IssueExistsAsync(Guid id)
    {
        return await _context.Issues.AnyAsync(i => i.Id == id);
    }

    public async Task<bool> IsValidIssueReference(Guid? issueId)
    {
        if (issueId is null) return true;
        return await _context.Issues.AnyAsync(i => i.Id == issueId.Value);
    }

    public async Task<List<Guid>> GetUserIdsByNRPsAsync(List<string> nrps)
    {
        return await _context.Users
            .Where(u => nrps.Contains(u.NRP))
            .Select(u => u.Id)
            .ToListAsync();
    }
    public async Task<Issue?> GetByCodeAsync(string issueCode)
{
    return await _context.Issues
        .Include(i => i.Tags).ThenInclude(t => t.Tag)
        .Include(i => i.AssignedUsers).ThenInclude(au => au.User)
        .Include(i => i.Comments)
        .Include(i => i.Creator)
        .Include(i => i.DuplicateOf)
        .FirstOrDefaultAsync(i => i.IssueCode == issueCode);
}


public async Task<int> GetTotalIssuesByProjectIdAsync(Guid projectId)
{
    return await _context.Issues.CountAsync(i => i.ProjectId == projectId);
}

public async Task<Dictionary<string, int>> GetIssueStatusCountsByProjectIdAsync(Guid projectId)
{
    return await _context.Issues
        .Where(i => i.ProjectId == projectId)
        .GroupBy(i => i.Status.ToString())
        .ToDictionaryAsync(g => g.Key, g => g.Count());
}

public async Task<DateTime?> GetLastActivityAtByProjectIdAsync(Guid projectId)
{
    return await _context.Issues
        .Where(i => i.ProjectId == projectId)
        .MaxAsync(i => (DateTime?)i.UpdatedAt ?? i.CreatedAt);
}

public async Task<List<string>> GetRecentIssueTitlesByProjectIdAsync(Guid projectId, int count = 3)
{
    return await _context.Issues
        .Where(i => i.ProjectId == projectId)
        .OrderByDescending(i => i.CreatedAt)
        .Select(i => i.Title)
        .Take(count)
        .ToListAsync();
}

public async Task<List<Issue>> GetByProjectCodeAsync(string projectCode)
{
    return await _context.Issues
        .Where(i => i.Project!.ProjectCode == projectCode)
        .Include(i => i.Project)
        .ToListAsync();
}

    

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
