namespace BugNest.Domain.Enums;

public enum ActivityEntityType
{
    Unknown = 0,

    // 🐛 Issue related
    Issue = 1,
    Comment = 2,
    Tag = 3,
    Attachment = 4,

    // 📦 Project
    Project = 10,
    ProjectMember = 11,

    // 👤 User related
    User = 20,
    Role = 21,

    // 🛠️ System
    SystemEvent = 99
}
