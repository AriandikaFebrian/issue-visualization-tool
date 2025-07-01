using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;
using BugNest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BugNest.Infrastructure.Repositories;

public class ActivityLogRepository : IActivityLogRepository
{
    private readonly BugNestDbContext _context;

    public ActivityLogRepository(BugNestDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ActivityLog log)
    {
        await _context.ActivityLogs.AddAsync(log);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<ActivityLog?> GetByIdAsync(Guid id)
    {
        return await _context.ActivityLogs
            .Include(log => log.User)
            .Include(log => log.Project)
            .FirstOrDefaultAsync(log => log.Id == id);
    }

    public async Task<List<ActivityLog>> GetAllAsync()
    {
        return await _context.ActivityLogs
            .Include(log => log.User)
            .OrderByDescending(log => log.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<ActivityLog>> GetByProjectIdAsync(Guid projectId)
    {
        return await _context.ActivityLogs
            .Include(log => log.User)
            .Where(log => log.ProjectId == projectId)
            .OrderByDescending(log => log.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<ActivityLog>> GetByUserIdAsync(Guid userId)
    {
        return await _context.ActivityLogs
            .Include(log => log.Project)
            .Where(log => log.UserId == userId)
            .OrderByDescending(log => log.CreatedAt)
            .ToListAsync();
    }

    // ✅ NEW: Paged result for general listing
    public async Task<(List<ActivityLog> Logs, int Total)> GetPagedAsync(int page, int pageSize)
    {
        var query = _context.ActivityLogs
            .Include(log => log.User)
            .OrderByDescending(log => log.CreatedAt);

        var total = await query.CountAsync();

        var logs = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (logs, total);
    }
}
