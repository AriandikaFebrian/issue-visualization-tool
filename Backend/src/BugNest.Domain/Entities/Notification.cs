
namespace BugNest.Domain.Entities;
public class Notification : BaseEntity
{
    // ✅ Target user (relasi foreign key)
    public Guid RecipientId { get; set; }
    public User? Recipient { get; set; }

    // ✅ Konten notifikasi
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    // ✅ Aksi / navigasi (opsional)
    public string? Link { get; set; } // Contoh: "/projects/BN-TRK-001"
    public string? ActionText { get; set; }
    
     // Contoh: "Lihat Proyek" (opsional)

    // ✅ Visual UI support (opsional)
    public string? Icon { get; set; } // frontend bisa pakai icon: "project", "issue", "comment"

    // ✅ Status & Metadata
    public bool IsRead { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    public DateTime? ReadAt { get; set; } // waktu dibaca user
}
