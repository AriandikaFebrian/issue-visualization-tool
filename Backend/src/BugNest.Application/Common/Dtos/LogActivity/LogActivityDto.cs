using BugNest.Domain.Enums;

namespace BugNest.Application.DTOs.ActivityLogs;

public class ActivityLogDto
{
    public Guid Id { get; set; }

    // 👤 Informasi pengguna
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? UserProfileUrl { get; set; }

    // 🗂️ Aksi & Target
    public ActivityAction Action { get; set; }
    public Guid TargetEntityId { get; set; }
    public ActivityEntityType TargetEntityType { get; set; }

    // 📝 Ringkasan (jika ada)
    public string? Summary { get; set; }

    // 🌐 Metadata Audit
    public string? SourcePlatform { get; set; }
    public string? IPAddress { get; set; }

    // 🕒 Waktu
    public DateTime CreatedAt { get; set; }
}
