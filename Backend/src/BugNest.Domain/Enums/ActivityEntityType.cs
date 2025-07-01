namespace BugNest.Domain.Enums;

public enum ActivityEntityType
{
    Unknown = 0,
    Issue = 1,
    Comment = 2,
    Tag = 3,
    Attachment = 4,
    Project = 10,
    ProjectMember = 11,
    User = 20,
    Role = 21,
    SystemEvent = 99
}
