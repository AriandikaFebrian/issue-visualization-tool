using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;
using BugNest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BugNest.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly BugNestDbContext _context;

    public ProjectRepository(BugNestDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Project project)
    {
        await _context.Projects.AddAsync(project);
    }

    public async Task<Project?> GetByIdAsync(Guid id)
    {
        return await _context.Projects
            .Include(p => p.Owner)
            .Include(p => p.Members).ThenInclude(pm => pm.User)
            .Include(p => p.Issues)
            .Include(p => p.ActivityLogs)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Project>> GetProjectsByOwnerAsync(Guid ownerId)
    {
        return await _context.Projects
            .Where(p => p.OwnerId == ownerId)
            .Include(p => p.Owner)
            .Include(p => p.Members).ThenInclude(pm => pm.User)
            .Include(p => p.Issues)
            .ToListAsync();
    }

    public async Task<Project?> GetByProjectCodeAsync(string projectCode)
    {
        return await _context.Projects
            .Include(p => p.Owner)
            .Include(p => p.Members).ThenInclude(pm => pm.User)
            .Include(p => p.Issues)
            .FirstOrDefaultAsync(p => p.ProjectCode == projectCode);
    }

    public async Task<List<Project>> GetAllAsync()
    {
        return await _context.Projects
            .Include(p => p.Owner)
            .Include(p => p.Members).ThenInclude(pm => pm.User)
            .Include(p => p.Issues)
            .ToListAsync();
    }

    public async Task<List<Project>> GetPublicProjectsAsync()
    {
        return await _context.Projects
            .Where(p => p.Visibility == Domain.Enums.ProjectVisibility.Public)
            .Include(p => p.Owner)
            .Include(p => p.Members)
            .ToListAsync();
    }

    public async Task<Project?> GetByCodeAsync(string projectCode)
    {
        return await _context.Projects
            .Include(p => p.Owner)
            .Include(p => p.Members).ThenInclude(pm => pm.User)
            .Include(p => p.Issues)
            .FirstOrDefaultAsync(p => p.ProjectCode == projectCode);
    }

    public async Task<Guid?> GetProjectIdByCodeAsync(string projectCode)
    {
        return await _context.Projects
            .Where(p => p.ProjectCode == projectCode)
            .Select(p => (Guid?)p.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<Project?> GetByCodeWithOwnerAsync(string projectCode)
    {
        return await _context.Projects
            .Include(p => p.Owner)
            .FirstOrDefaultAsync(p => p.ProjectCode == projectCode);
    }

    public async Task<Project?> GetByCodeWithDetailsAsync(string projectCode)
{
    return await _context.Projects
        .Include(p => p.Owner)
        .Include(p => p.Members).ThenInclude(pm => pm.User)
        .Include(p => p.Issues).ThenInclude(i => i.AssignedUsers).ThenInclude(au => au.User)
        .Include(p => p.Issues).ThenInclude(i => i.Tags)
        .Include(p => p.Issues).ThenInclude(i => i.Creator)
        .Include(p => p.ActivityLogs)
        .FirstOrDefaultAsync(p => p.ProjectCode == projectCode);
}


    public async Task<List<Project>> GetPublicProjectsWithOwnerAsync()
    {
        return await _context.Projects
            .Where(p => p.Visibility == Domain.Enums.ProjectVisibility.Public)
            .Include(p => p.Owner)
            .ToListAsync();
    }

public async Task<Project?> GetWithMembersAndOwnerByCodeAsync(string projectCode)
{
    return await _context.Projects
        .Include(p => p.Owner)
        .Include(p => p.Members)
            .ThenInclude(pm => pm.User)
        .FirstOrDefaultAsync(p => p.ProjectCode == projectCode);
}

public async Task<List<Issue>> GetIssuesByProjectCodeAsync(string projectCode, CancellationToken cancellationToken = default)
{
    return await _context.Issues
        .Include(i => i.Project)
        .Where(i => i.Project != null && i.Project.ProjectCode == projectCode)
        .OrderByDescending(i => i.UpdatedAt)
        .ToListAsync(cancellationToken);
}






    public async Task UpdateAsync(Project project)
    {
        _context.Projects.Update(project);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
