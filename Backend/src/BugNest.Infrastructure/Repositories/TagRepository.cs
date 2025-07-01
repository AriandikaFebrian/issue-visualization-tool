using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;
using BugNest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class TagRepository : ITagRepository
{
    private readonly BugNestDbContext _context;

    public TagRepository(BugNestDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Tag tag)
    {
        await _context.Tags.AddAsync(tag);
    }

    public async Task<List<Tag>> GetAllAsync()
    {
        return await _context.Tags
            .Where(t => !t.IsArchived)
            .ToListAsync();
    }

    public async Task<List<Tag>> GetByProjectCodeAsync(string projectCode, bool includeGlobal = true)
    {
        return await _context.Tags
            .Where(t =>
                !t.IsArchived &&
                (t.ProjectCode == projectCode || (includeGlobal && t.ProjectCode == null)))
            .ToListAsync();
    }

    public async Task<List<Tag>> GetByProjectOnlyAsync(string projectCode)
    {
        return await _context.Tags
            .Where(t => !t.IsArchived && t.ProjectCode == projectCode)
            .ToListAsync();
    }

    public async Task<Tag?> GetByIdAsync(Guid tagId)
    {
        return await _context.Tags
            .FirstOrDefaultAsync(t => t.Id == tagId && !t.IsArchived);
    }

    public async Task ArchiveAsync(Guid tagId)
    {
        var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == tagId);
        if (tag != null)
        {
            tag.IsArchived = true;
            tag.UpdatedAt = DateTime.UtcNow;
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
