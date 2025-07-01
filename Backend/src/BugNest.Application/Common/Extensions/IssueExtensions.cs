using BugNest.Domain.Entities;

namespace BugNest.Domain.Extensions;

public static class IssueExtensions
{
    public static bool IsResolved(this Issue issue)
    {
        return issue.Status == IssueStatus.Resolved || issue.Status == IssueStatus.Closed;
    }
}
