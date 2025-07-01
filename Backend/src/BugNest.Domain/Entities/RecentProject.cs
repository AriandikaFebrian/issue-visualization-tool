using System;
using BugNest.Domain.Entities;

namespace BugNest.Domain.Entities;

public class RecentProject : BaseEntity
{
    public string NRP { get; set; } = string.Empty;
    public string ProjectCode { get; set; } = string.Empty;
    public DateTime AccessedAt { get; set; } = DateTime.UtcNow;
    public User? User { get; set; }
    public Project? Project { get; set; }
}
