namespace BugNest.Application.DTOs.Projects;

public class ProjectDetailDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ProjectCode { get; set; }
    public string? RepositoryUrl { get; set; }
    public string? DocumentationUrl { get; set; }
    public string Status { get; set; } = "Planning";
    public string Visibility { get; set; } = "Private";
    public DateTime? UpdatedAt { get; set; }
    public string OwnerNRP { get; set; } = string.Empty;
    public string OwnerUsername { get; set; } = string.Empty;
    public string? OwnerFullName { get; set; }

    // Ringkasan
    public int TotalIssues { get; set; }
    public Dictionary<string, int> IssueStatusCounts { get; set; } = new();
    public int MemberCount { get; set; }
    public DateTime? LastActivityAt { get; set; }
    public List<string> RecentIssueSummaries { get; set; } = new();

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
