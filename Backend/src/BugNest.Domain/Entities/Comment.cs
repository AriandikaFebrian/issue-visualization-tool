namespace BugNest.Domain.Entities;

public class Comment : BaseEntity
{
    // 🔗 Hubungan ke issue (pakai ID relasional tetap, untuk efisiensi)
    public Guid IssueId { get; set; }
    public Issue? Issue { get; set; }

    // 🧑 Penulis komentar
    public Guid UserId { get; set; }
    public User? User { get; set; }

    // ✍️ Konten utama
    public string Content { get; set; } = string.Empty;

    // 🕒 Audit waktu (diwarisi dari BaseEntity.CreatedAt)
    public DateTime? UpdatedAt { get; set; }

    // 🧾 Apakah komentar ini di-edit?
    public bool IsEdited { get; set; } = false;

    // 🗑️ Soft delete flag (optional)
    public bool IsDeleted { get; set; } = false;

    // 📎 (opsional) Lampiran atau file
    public string? AttachmentUrl { get; set; }

    // 📌 (opsional) Flag komentar penting
    public bool IsPinned { get; set; } = false;
}
