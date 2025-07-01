using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;
using BugNest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BugNest.Infrastructure.Repositories;

public class ProjectMemberRepository : IProjectMemberRepository
{
    private readonly BugNestDbContext _context;

    public ProjectMemberRepository(BugNestDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ProjectMember member)
    {
        await _context.ProjectMembers.AddAsync(member);
    }

    public async Task<List<ProjectMember>> GetMembersByProjectIdAsync(Guid projectId)
    {
        return await _context.ProjectMembers
            .Include(pm => pm.User) // ✅ penting agar data Username, Email dll. tersedia
            .Where(pm => pm.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task<bool> IsUserInProject(Guid userId, Guid projectId)
    {
        return await _context.ProjectMembers
            .AnyAsync(pm => pm.ProjectId == projectId && pm.UserId == userId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
