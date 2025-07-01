using BugNest.Domain.Entities;
using BugNest.Domain.Enums;

public class IssueHistory : BaseEntity
{
    public Guid IssueId { get; set; }
    public Issue? Issue { get; set; }

    public IssueChangeType ChangeType { get; set; }

    public string? PreviousValue { get; set; }
    public string? NewValue { get; set; }

    public string? Note { get; set; }

    public Guid ChangedBy { get; set; }
    public User? ChangedByUser { get; set; }

    // Optional snapshot
    public string ChangedByUsername { get; set; } = string.Empty;
    public string? ChangedByProfileUrl { get; set; }

    public string? ChangedFromIP { get; set; }
    public string? SourcePlatform { get; set; }

}
