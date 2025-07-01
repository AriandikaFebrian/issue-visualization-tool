using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;
using BugNest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class RecentProjectRepository : IRecentProjectRepository
{
    private readonly BugNestDbContext _context;

    public RecentProjectRepository(BugNestDbContext context)
    {
        _context = context;
    }

    public async Task<List<RecentProjectDto>> GetRecentProjectsByNRPAsync(string nrp)
    {
        return await _context.RecentProjectAccesses
            .Where(r => r.NRP == nrp)
            .OrderByDescending(r => r.AccessedAt)
            .Take(5)
            .Include(r => r.Project)
            .Select(r => new RecentProjectDto
            {
                ProjectCode = r.ProjectCode,
                Name = r.Project!.Name,
                Description = r.Project.Description,
                RepositoryUrl = r.Project.RepositoryUrl,
                DocumentationUrl = r.Project.DocumentationUrl,
                AccessedAt = r.AccessedAt
            })
            .ToListAsync();
    }

    public async Task AddOrUpdateRecentProjectAsync(string nrp, string projectCode)
    {
        var existing = await _context.RecentProjectAccesses
            .FirstOrDefaultAsync(r => r.NRP == nrp && r.ProjectCode == projectCode);

        if (existing != null)
        {
            existing.AccessedAt = DateTime.UtcNow;
        }
        else
        {
            _context.RecentProjectAccesses.Add(new RecentProjectAccess
            {
                NRP = nrp,
                ProjectCode = projectCode,
                AccessedAt = DateTime.UtcNow
            });
        }
        var recentList = await _context.RecentProjectAccesses
            .Where(r => r.NRP == nrp)
            .OrderByDescending(r => r.AccessedAt)
            .ToListAsync();

        if (recentList.Count > 3)
        {
            var toRemove = recentList.Skip(5).ToList();
            _context.RecentProjectAccesses.RemoveRange(toRemove);
        }

        await _context.SaveChangesAsync();
    }
}
