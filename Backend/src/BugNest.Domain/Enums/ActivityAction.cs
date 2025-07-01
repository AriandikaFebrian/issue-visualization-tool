namespace BugNest.Domain.Enums;

public enum ActivityAction
{
    Unknown = 0,

    // ===== 📦 Project =====
    CreatedProject = 10,
    UpdatedProject = 11,
    ArchivedProject = 12,
    RestoredProject = 13,

    // ===== 🐛 Issue =====
    CreatedIssue = 20,
    UpdatedIssue = 21,
    ChangedIssueStatus = 22,
    AssignedUserToIssue = 23,
    RemovedUserFromIssue = 24,
    DeletedIssue = 25,

    // ===== 💬 Comment =====
    AddedComment = 30,
    EditedComment = 31,
    DeletedComment = 32,

    // ===== 🏷️ Tag / Label =====
    CreatedTag = 40,
    UpdatedTag = 41,
    DeletedTag = 42,

    // ===== 🧑‍🤝‍🧑 User / Member Management =====
    InvitedUser = 50,
    RemovedUserFromProject = 51,
    ChangedUserRole = 52,

    // ===== 📁 Attachment / File =====
    UploadedFile = 60,
    DeletedFile = 61,

    // ===== 🔁 Automation / System-Generated =====
    AutoAssigned = 90,
    AutoClosedIssue = 91,
    AutoReopenedIssue = 92
}
