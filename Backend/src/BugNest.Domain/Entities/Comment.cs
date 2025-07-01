namespace BugNest.Domain.Entities;

public class Comment : BaseEntity
{
    public Guid IssueId { get; set; }
    public Issue? Issue { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; }
    public bool IsEdited { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    public string? AttachmentUrl { get; set; }
    public bool IsPinned { get; set; } = false;
}
