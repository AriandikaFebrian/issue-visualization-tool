using BugNest.Domain.Entities;

namespace BugNest.Application.Interfaces;

public interface ITagRepository
{
    Task AddAsync(Tag tag);
    Task<List<Tag>> GetByProjectCodeAsync(string projectCode, bool includeGlobal = true);
    Task<List<Tag>> GetAllAsync();
    Task<List<Tag>> GetByProjectOnlyAsync(string projectCode);
    Task<Tag?> GetByIdAsync(Guid tagId);
    Task ArchiveAsync(Guid tagId);
    Task SaveChangesAsync();
}
