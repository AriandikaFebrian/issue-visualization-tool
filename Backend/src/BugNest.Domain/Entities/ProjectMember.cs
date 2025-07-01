// 📁 BugNest.Domain/Entities/ProjectMember.cs
namespace BugNest.Domain.Entities;

public class ProjectMember : BaseEntity
{
    // 🔗 Foreign Key ke Project
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }

    // 🔗 Foreign Key ke User
    public Guid UserId { get; set; }
    public User? User { get; set; }

    // ⏱️ Waktu gabung ke proyek (opsional tapi informatif)
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
}