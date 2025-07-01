namespace BugNest.Application.DTOs.Issues;

public class IssueCreatedDto
{
    public Guid IssueId { get; set; }
    public string IssueCode { get; set; } = string.Empty;
}
