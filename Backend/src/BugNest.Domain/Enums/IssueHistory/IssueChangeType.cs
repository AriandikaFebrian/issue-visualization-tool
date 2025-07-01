namespace BugNest.Domain.Enums;

public enum IssueChangeType
{
    Unknown = 0,
    StatusChanged = 1,
    PriorityChanged = 2,
    AssigneeChanged = 3,
    TitleChanged = 4,
    DescriptionChanged = 5,
    StepsToReproduceChanged = 6,
    DeviceInfoChanged = 7,
    TagsUpdated = 8,
    TypeChanged = 9,
    DeadlineChanged = 10,
    EstimatedFixHoursChanged = 11,
    ActualFixHoursChanged = 12,
    BlockedByChanged = 13,
    DuplicateOfChanged = 14,
    SystemReopened = 20,
    SystemAutoClosed = 21
}
