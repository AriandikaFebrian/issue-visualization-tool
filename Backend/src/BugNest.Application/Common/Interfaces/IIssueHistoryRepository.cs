using BugNest.Domain.Entities;

namespace BugNest.Application.Interfaces;

public interface IIssueHistoryRepository
{
    Task<List<IssueHistory>> GetByIssueIdAsync(Guid issueId);
    Task AddAsync(IssueHistory history);

    Task<IssueHistory?> GetLatestStatusChangeAsync(Guid issueId, Guid userId);
    Task SaveChangesAsync();

    
}
