// 📁 BugNest.Application/Interfaces/IProjectRepository.cs
using BugNest.Domain.Entities;

namespace BugNest.Application.Interfaces;

public interface IProjectRepository
{
    // CRUD Dasar
    Task AddAsync(Project project);
    Task<Project?> GetByIdAsync(Guid id);
    Task<List<Project>> GetProjectsByOwnerAsync(Guid ownerId);
    Task<Guid?> GetProjectIdByCodeAsync(string projectCode);
    Task<Project?> GetByCodeAsync(string projectCode);
    Task<Project?> GetByCodeWithOwnerAsync(string projectCode);
    Task<List<Project>> GetPublicProjectsWithOwnerAsync();


    // ✅ Tambahan
    Task<Project?> GetByCodeWithDetailsAsync(string projectCode); // ← Tambahan baru
    Task<Project?> GetWithMembersAndOwnerByCodeAsync(string projectCode);

    Task<List<Project>> GetAllAsync(); // Untuk admin
    Task<List<Project>> GetPublicProjectsAsync(); // Kalau ada proyek publik
    Task UpdateAsync(Project project); // Untuk update umum (status, visibilitas, metadata)
Task<List<Issue>> GetIssuesByProjectCodeAsync(string projectCode, CancellationToken cancellationToken = default);

    

    Task SaveChangesAsync();
}
