using BugNest.Domain.Entities;

namespace BugNest.Application.Interfaces;

public interface IIssueRepository
{
    // 🟩 Core Operations
    Task AddAsync(Issue issue);
    Task<Issue?> GetByIdAsync(Guid id);
    Task<List<Issue>> GetByProjectIdAsync(Guid projectId);
    Task<List<Issue>> GetIssuesWithDetailsByProjectIdAsync(Guid projectId);
    Task<bool> IssueExistsAsync(Guid id);
    Task<bool> IsValidIssueReference(Guid? issueId);
    Task<List<Issue>> GetRecentIssuesAsync(int count);
    Task<List<Issue>> GetIssuesAssignedToUserAsync(Guid userId);



    // 🟨 Relasi (many-to-many)
    Task AddTagsAsync(List<IssueTag> tags);
    Task AddAssignedUsersAsync(List<AssignedUser> assignedUsers);

    // 🟥 Optional lookups (kalau diperlukan eksplisit)
    Task<Issue?> GetByCodeAsync(string issueCode);


    // 🧠 Utility
    Task<List<Guid>> GetUserIdsByNRPsAsync(List<string> nrps);

    // 💾 Commit perubahan
    Task SaveChangesAsync();
}
