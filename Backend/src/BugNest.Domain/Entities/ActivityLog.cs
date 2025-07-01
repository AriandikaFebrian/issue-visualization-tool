using BugNest.Domain.Enums;

namespace BugNest.Domain.Entities;

public class ActivityLog
{
    public Guid Id { get; set; }

    // 📁 Project terkait aktivitas
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }

    // 👤 Pengguna yang melakukan aktivitas
    public Guid UserId { get; set; }
    public User? User { get; set; }

    // 🔧 Jenis aksi (create issue, update status, dll)
    public ActivityAction Action { get; set; }

    // 📌 Entitas yang jadi target dari aksi ini (Issue, Comment, dsb)
    public Guid TargetEntityId { get; set; }

public ActivityEntityType TargetEntityType { get; set; }
 // Contoh: "Issue", "Comment"

    // 📝 Ringkasan aktivitas
    public string? Summary { get; set; }

    // 🕒 Timestamp
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // 🌐 Info tambahan (opsional)
    public string? SourcePlatform { get; set; } // Web, Mobile, API, SystemJob
    public string? IPAddress { get; set; }

    // 🗑️ Soft delete (kalau histori tidak boleh langsung dihapus)
    public bool IsArchived { get; set; } = false;
}
