// 📁 BugNest.Domain/Entities/Join/IssueTag.cs

using BugNest.Domain.Entities;
using BugNest.Domain.Enums;

namespace BugNest.Domain.Entities;

public class IssueTag
{
    public Guid IssueId { get; set; }
    public Issue Issue { get; set; } = default!;

    public Guid TagId { get; set; }
    public Tag Tag { get; set; } = default!;

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    public string? AssignedByNRP { get; set; } // audit, jika perlu
}
