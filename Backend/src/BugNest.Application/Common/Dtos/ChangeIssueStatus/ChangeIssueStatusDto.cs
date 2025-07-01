namespace BugNest.Application.DTOs.Issues;

public class ChangeIssueStatusDto
{
    public IssueStatus NewStatus { get; set; }
    public string? Note { get; set; }
    public string ChangedByNRP { get; set; } = string.Empty; // jika pakai NRP
}


