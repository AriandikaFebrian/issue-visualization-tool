namespace BugNest.Application.DTOs.Issues;

public class IssueHistoryDto
{
    public string ChangeType { get; set; } = string.Empty;

    public string? PreviousValue { get; set; }
    public string? NewValue { get; set; }
    public string? Note { get; set; }

    public Guid ChangedBy { get; set; }
    public string ChangedByUsername { get; set; } = string.Empty;
    public string? ChangedByProfileUrl { get; set; }

    public string? ChangedFromIP { get; set; }
    public string? SourcePlatform { get; set; }

    public DateTime CreatedAt { get; set; }
}
