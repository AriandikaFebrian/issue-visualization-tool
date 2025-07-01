using System;
using BugNest.Domain.Entities;

namespace BugNest.Domain.Entities;

public class RecentProject : BaseEntity
{
    // ✅ Unik per user dan project
    public string NRP { get; set; } = string.Empty;
    public string ProjectCode { get; set; } = string.Empty;

    // ✅ Timestamp kapan terakhir diakses
    public DateTime AccessedAt { get; set; } = DateTime.UtcNow;

    // ✅ Relasi opsional
    public User? User { get; set; }
    public Project? Project { get; set; }
}
