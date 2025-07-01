using BugNest.Domain.Enums;

namespace BugNest.Application.DTOs.ActivityLogs;

public class LogActivityDto
{
    public Guid ProjectId { get; set; }

    public Guid UserId { get; set; }

    public ActivityAction Action { get; set; }

    public Guid TargetEntityId { get; set; }
    public string? TargetEntityType { get; set; }
    public string? Summary { get; set; }
    public string? SourcePlatform { get; set; }
    public string? IPAddress { get; set; }
}
