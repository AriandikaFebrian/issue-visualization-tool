using BugNest.Domain.Enums;

namespace BugNest.Application.DTOs.ActivityLogs;

public class ActivityLogDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? UserProfileUrl { get; set; }
    public ActivityAction Action { get; set; }
    public Guid TargetEntityId { get; set; }
    public ActivityEntityType TargetEntityType { get; set; }
    public string? Summary { get; set; }
    public string? SourcePlatform { get; set; }
    public string? IPAddress { get; set; }
    public DateTime CreatedAt { get; set; }
}
