using BugNest.Domain.Enums;

public interface IAuditableCommand
{
    ActivityAction Action { get; }
    ActivityEntityType TargetEntityType { get; }
    Guid TargetEntityId { get; }
    string Summary { get; }
    string? PerformedByNRP { get; }
    Guid ProjectId { get; }
}
