using BugNest.Domain.Enums;

namespace BugNest.Application.DTOs.ActivityLogs;

public class LogActivityDto
{
    public Guid ProjectId { get; set; }

    public Guid UserId { get; set; }

    public ActivityAction Action { get; set; }

    public Guid TargetEntityId { get; set; }

    // 🧾 Jenis entitas yang dilog (opsional, misal: "Issue", "Comment")
    public string? TargetEntityType { get; set; }

    // 📝 Ringkasan aktivitas (opsional, human readable)
    public string? Summary { get; set; }

    // 🌐 Metadata audit (opsional)
    public string? SourcePlatform { get; set; }  // Web, Mobile, API, dll
    public string? IPAddress { get; set; }       // IP pengguna
}
