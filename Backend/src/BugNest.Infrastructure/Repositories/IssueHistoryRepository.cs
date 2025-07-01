using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;
using BugNest.Domain.Enums;
using BugNest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BugNest.Infrastructure.Repositories;

public class IssueHistoryRepository : IIssueHistoryRepository
{
    private readonly BugNestDbContext _context;

    public IssueHistoryRepository(BugNestDbContext context)
    {
        _context = context;
    }

    public async Task<List<IssueHistory>> GetByIssueIdAsync(Guid issueId)
{
    return await _context.IssueHistories
        .Where(h => h.IssueId == issueId)
        .OrderByDescending(h => h.CreatedAt)
        .ToListAsync();
}

public async Task<IssueHistory?> GetLatestStatusChangeAsync(Guid issueId, Guid userId)
{
    return await _context.IssueHistories
        .Where(h => h.IssueId == issueId &&
                    h.ChangedBy == userId &&
                    h.ChangeType == IssueChangeType.StatusChanged)
        .OrderByDescending(h => h.CreatedAt)
        .FirstOrDefaultAsync();
}



    public async Task AddAsync(IssueHistory history)
    {
        await _context.IssueHistories.AddAsync(history);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
