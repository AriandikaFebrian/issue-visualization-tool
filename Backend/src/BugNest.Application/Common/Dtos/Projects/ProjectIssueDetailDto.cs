using BugNest.Domain.Enums;

using BugNest.Application.DTOs.Issues;
public class ProjectIssueDetailDto
{
    public string ProjectCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string RepositoryUrl { get; set; }
    public string DocumentationUrl { get; set; }
    public string Status { get; set; }
    public string Visibility { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public UserDto Owner { get; set; }
    public List<UserDto> Members { get; set; }

    public int TotalIssues { get; set; }
    public Dictionary<string, int> IssueStatusCounts { get; set; }
    public List<IssueDto> RecentIssues { get; set; }
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
