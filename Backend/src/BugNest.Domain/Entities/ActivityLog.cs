using BugNest.Domain.Enums;

namespace BugNest.Domain.Entities;

public class ActivityLog
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public ActivityAction Action { get; set; }
    public Guid TargetEntityId { get; set; }

public ActivityEntityType TargetEntityType { get; set; }
    public string? Summary { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? SourcePlatform { get; set; }
    public string? IPAddress { get; set; }
    public bool IsArchived { get; set; } = false;
}
