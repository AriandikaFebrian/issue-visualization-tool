using BugNest.Domain.Entities;

namespace BugNest.Application.Interfaces;

public interface IActivityLogRepository
{
    Task AddAsync(ActivityLog log);
    Task<List<ActivityLog>> GetByProjectIdAsync(Guid projectId);
    Task<List<ActivityLog>> GetByUserIdAsync(Guid userId);

    Task<List<ActivityLog>> GetAllAsync();
    Task<ActivityLog?> GetByIdAsync(Guid id);
    Task<(List<ActivityLog> Logs, int Total)> GetPagedAsync(int page, int pageSize);
    Task SaveChangesAsync();
}
