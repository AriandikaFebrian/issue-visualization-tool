namespace BugNest.Domain.Enums;

public enum IssueChangeType
{
    Unknown = 0,

    // 📌 Perubahan Status
    StatusChanged = 1,

    // ⏫ Perubahan Prioritas
    PriorityChanged = 2,

    // 👥 Perubahan Penanggung Jawab
    AssigneeChanged = 3,

    // 📝 Perubahan Isi
    TitleChanged = 4,
    DescriptionChanged = 5,
    StepsToReproduceChanged = 6,
    DeviceInfoChanged = 7,

    // 🏷️ Perubahan Tag
    TagsUpdated = 8,

    // 🧭 Metadata
    TypeChanged = 9,
    DeadlineChanged = 10,
    EstimatedFixHoursChanged = 11,
    ActualFixHoursChanged = 12,

    // 🔁 Relasi Issue
    BlockedByChanged = 13,
    DuplicateOfChanged = 14,

    // 🛠️ Sistem (automated)
    SystemReopened = 20,
    SystemAutoClosed = 21
}
