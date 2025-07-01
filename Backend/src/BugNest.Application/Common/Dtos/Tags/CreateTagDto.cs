namespace BugNest.Application.DTOs.Tags;

public class CreateTagDto
{
    // 🏷️ Informasi dasar
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = "#cccccc";

    // 🔗 Scope proyek (null jika global tag)
    public string? ProjectCode { get; set; }

    // 👤 Siapa yang membuat tag
    public string CreatedByNRP { get; set; } = string.Empty;

    // 📎 Opsional: Kategori/klasifikasi tag
    public string? Category { get; set; } // contoh: "Component", "Risk"
}
