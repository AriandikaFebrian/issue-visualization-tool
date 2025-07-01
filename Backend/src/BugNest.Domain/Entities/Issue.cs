using BugNest.Domain.Enums;

namespace BugNest.Domain.Entities;

public class Issue : BaseEntity
{
    // 🔗 Relasi ke Project
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }

    // 🟦 Informasi Utama
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StepsToReproduce { get; set; } = string.Empty;
    public string DeviceInfo { get; set; } = string.Empty;
    public string IssueCode { get; set; } = string.Empty;


    // 🟧 Metadata
    public IssueType Type { get; set; } = IssueType.Bug;
    public PriorityLevel Priority { get; set; } = PriorityLevel.Medium;
    public IssueStatus Status { get; set; } = IssueStatus.Open;
    public int? SeverityScore { get; set; }
    public int ReopenCount { get; set; } = 0;

    // ⏳ Estimasi dan Waktu
    public DateTime? Deadline { get; set; }
    public int? EstimatedFixHours { get; set; }
    public int? ActualFixHours { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }
    
    public bool IsDuplicate { get; set; } = false;
    public Guid? DuplicateOfId { get; set; }
    public Issue? DuplicateOf { get; set; }

    // 👤 Informasi Pencipta
    public Guid CreatedBy { get; set; }
    public User? Creator { get; set; }

    // 🏷️ Koleksi Relasi
    public ICollection<IssueTag> Tags { get; set; } = new List<IssueTag>();
    public ICollection<AssignedUser> AssignedUsers { get; set; } = new List<AssignedUser>();
    public ICollection<IssueHistory> History { get; set; } = new List<IssueHistory>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
