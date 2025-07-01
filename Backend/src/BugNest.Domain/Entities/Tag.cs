namespace BugNest.Domain.Entities;

public class Tag : BaseEntity
{
    // 🏷️ Nama Tag, misalnya "UI", "Urgent"
    public string Name { get; set; } = string.Empty;

    // 🎨 Warna untuk visualisasi
    public string Color { get; set; } = "#000000";

    public required string Category { get; set; }

    // 📁 Scope Tag (opsional, tergantung multi-tenancy)
  public string? ProjectCode { get; set; } // null = global tag
public Project? Project { get; set; }


    // 🧑‍💼 Siapa yang buat tag ini (audit)
    public Guid CreatedBy { get; set; }
    public User? Creator { get; set; }

    // 🛑 Apakah tag ini sudah tidak aktif/dihapus soft?
    public bool IsArchived { get; set; } = false;

    // 📅 Waktu diupdate
    public DateTime? UpdatedAt { get; set; }

    // 🔗 Relasi ke Issue
    public ICollection<IssueTag> IssueTags { get; set; } = new List<IssueTag>();
}
