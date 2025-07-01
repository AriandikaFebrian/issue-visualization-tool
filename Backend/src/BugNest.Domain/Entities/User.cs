namespace BugNest.Domain.Entities;

public class User : BaseEntity
{
    // Identitas dasar
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }

    // ✅ Identitas tambahan (misalnya dari institusi atau internal ID)
    public string NRP { get; set; } = string.Empty; // bisa NRP / NIP / EmployeeID

    // ✅ Info Profil
    public string? FullName { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public DepartmentType? Department { get; set; }
    public PositionType? Position { get; set; }

    // ✅ Status & audit
    public bool IsActive { get; set; } = true;
    public DateTime LastLogin { get; set; } = DateTime.UtcNow;
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

    // ✅ Relasi ke sistem
    public ICollection<Project> OwnedProjects { get; set; } = new List<Project>();
    public ICollection<ProjectMember> ProjectMemberships { get; set; } = new List<ProjectMember>();
    public ICollection<AssignedUser> AssignedIssues { get; set; } = new List<AssignedUser>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    
}
