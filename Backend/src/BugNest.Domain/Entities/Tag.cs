namespace BugNest.Domain.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = "#000000";

    public required string Category { get; set; }
  public string? ProjectCode { get; set; }
public Project? Project { get; set; }
    public Guid CreatedBy { get; set; }
    public User? Creator { get; set; }
    public bool IsArchived { get; set; } = false;
    public DateTime? UpdatedAt { get; set; }
    public ICollection<IssueTag> IssueTags { get; set; } = new List<IssueTag>();
}
