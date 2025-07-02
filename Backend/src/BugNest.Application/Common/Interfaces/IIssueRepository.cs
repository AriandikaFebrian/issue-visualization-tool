using BugNest.Domain.Entities;

namespace BugNest.Application.Interfaces;

public interface IIssueRepository
{
    Task AddAsync(Issue issue);
    Task<Issue?> GetByIdAsync(Guid id);
    Task<List<Issue>> GetByProjectIdAsync(Guid projectId);
    Task<List<Issue>> GetIssuesWithDetailsByProjectIdAsync(Guid projectId);
    Task<bool> IssueExistsAsync(Guid id);
    Task<bool> IsValidIssueReference(Guid? issueId);
    Task<List<Issue>> GetRecentIssuesAsync(int count);
    Task<List<Issue>> GetIssuesAssignedToUserAsync(Guid userId);
    Task AddTagsAsync(List<IssueTag> tags);
    Task AddAssignedUsersAsync(List<AssignedUser> assignedUsers);
    Task<Issue?> GetByCodeAsync(string issueCode);


Task<List<Issue>> GetByProjectCodeAsync(string projectCode);
    Task<List<Guid>> GetUserIdsByNRPsAsync(List<string> nrps);

    // Tambahan ini harus ADA
    Task<int> GetTotalIssuesByProjectIdAsync(Guid projectId);
    Task<Dictionary<string, int>> GetIssueStatusCountsByProjectIdAsync(Guid projectId);
    Task<DateTime?> GetLastActivityAtByProjectIdAsync(Guid projectId);
    Task<List<string>> GetRecentIssueTitlesByProjectIdAsync(Guid projectId, int count = 3);

    Task SaveChangesAsync();
}

