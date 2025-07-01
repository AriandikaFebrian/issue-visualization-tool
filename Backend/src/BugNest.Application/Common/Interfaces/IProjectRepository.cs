using BugNest.Domain.Entities;

namespace BugNest.Application.Interfaces;

public interface IProjectRepository
{
    Task AddAsync(Project project);
    Task<Project?> GetByIdAsync(Guid id);
    Task<List<Project>> GetProjectsByOwnerAsync(Guid ownerId);
    Task<Guid?> GetProjectIdByCodeAsync(string projectCode);
    Task<Project?> GetByCodeAsync(string projectCode);
    Task<Project?> GetByCodeWithOwnerAsync(string projectCode);
    Task<Project?> GetByCodeWithDetailsAsync(string projectCode);
    Task<Project?> GetWithMembersAndOwnerByCodeAsync(string projectCode);
    Task<List<Project>> GetAllAsync();
    Task<List<Project>> GetPublicProjectsAsync();
    Task<List<Project>> GetPublicProjectsWithOwnerAsync();
    Task<List<Issue>> GetIssuesByProjectCodeAsync(string projectCode, CancellationToken cancellationToken = default);

    Task UpdateAsync(Project project);
    Task SaveChangesAsync();

    // 🆕 Optional (kalau mau validasi path unik)
    Task<bool> IsSourcePathUsedAsync(string path); 
}

