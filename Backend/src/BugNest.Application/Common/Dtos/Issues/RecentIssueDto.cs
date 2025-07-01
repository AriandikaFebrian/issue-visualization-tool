namespace BugNest.Application.DTOs.Issues;

public class RecentIssueDto
{
    public string IssueCode { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string ProjectCode { get; set; } = default!;
    public string Status { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}
