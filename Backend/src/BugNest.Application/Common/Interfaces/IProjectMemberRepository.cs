// 📁 BugNest.Application/Interfaces/IProjectMemberRepository.cs
using BugNest.Domain.Entities;

namespace BugNest.Application.Interfaces;

public interface IProjectMemberRepository
{
    Task AddAsync(ProjectMember member);
    Task<List<ProjectMember>> GetMembersByProjectIdAsync(Guid projectId);
    Task<bool> IsUserInProject(Guid userId, Guid projectId);
    Task SaveChangesAsync();
}
