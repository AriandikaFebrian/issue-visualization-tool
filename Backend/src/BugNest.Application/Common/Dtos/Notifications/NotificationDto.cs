namespace BugNest.Application.DTOs.Notifications;

public class NotificationDto
{
    public Guid Id { get; set; }

    // Konten
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    // Navigasi/Aksi
    public string? Link { get; set; }
    public string? ActionText { get; set; } // opsional CTA seperti: "Lihat Proyek"

    // Visual
    public string? Icon { get; set; } // frontend: "project", "issue", "comment", dsb

    // Status
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ReadAt { get; set; }
}
