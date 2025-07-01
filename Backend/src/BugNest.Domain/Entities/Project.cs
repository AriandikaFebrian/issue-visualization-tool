using BugNest.Domain.Enums;

namespace BugNest.Domain.Entities;

public class Project : BaseEntity
{
    // ✅ Informasi Dasar Proyek
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // ✅ Metadata Proyek
    public string? ProjectCode { get; set; } // Contoh: "BN-TRK-001"
    public string? RepositoryUrl { get; set; } // GitHub/GitLab repo link
    public string? DocumentationUrl { get; set; } // Link ke dokumentasi teknis

    // ✅ Status & Audit
    public DateTime? UpdatedAt { get; set; }
    public ProjectStatus? Status { get; set; } = ProjectStatus.Planning;
    public ProjectVisibility Visibility { get; set; } = ProjectVisibility.Private;


    // ✅ Relasi ke Owner
    public Guid OwnerId { get; set; }
    public User? Owner { get; set; }

    // ✅ Relasi ke entitas lain
    public ICollection<ProjectMember> Members { get; set; } = new List<ProjectMember>();
    public ICollection<Issue> Issues { get; set; } = new List<Issue>();
    public ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}