namespace BugNest.Application.DTOs.Projects;

public class ProjectSummaryDto
{
    public string ProjectCode { get; set; } = string.Empty;
    public int TotalIssues { get; set; }
    public Dictionary<string, int> IssueStatusCounts { get; set; } = new();

    public int MemberCount { get; set; }
    public DateTime? LastActivityAt { get; set; }

    private static readonly string[] CompletedStatuses = new[]
    {
        nameof(IssueStatus.Closed),
        nameof(IssueStatus.Resolved)
    };

    public double ProgressPercentage => TotalIssues == 0
        ? 0
        : Math.Round(
            (double)IssueStatusCounts
                .Where(kv => CompletedStatuses.Contains(kv.Key))
                .Sum(kv => kv.Value) / TotalIssues * 100,
            2
        );
}
