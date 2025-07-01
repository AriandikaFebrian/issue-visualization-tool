namespace BugNest.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public string NRP { get; set; } = string.Empty;
    public string? FullName { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public DepartmentType? Department { get; set; }
    public PositionType? Position { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime LastLogin { get; set; } = DateTime.UtcNow;
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    public ICollection<Project> OwnedProjects { get; set; } = new List<Project>();
    public ICollection<ProjectMember> ProjectMemberships { get; set; } = new List<ProjectMember>();
    public ICollection<AssignedUser> AssignedIssues { get; set; } = new List<AssignedUser>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    
}
