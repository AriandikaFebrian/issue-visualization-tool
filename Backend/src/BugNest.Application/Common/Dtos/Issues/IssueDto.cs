namespace BugNest.Application.DTOs.Issues;

public class IssueDto
{
    public Guid Id { get; set; }
      public string IssueCode { get; set; } = ""; // ✅ Tambahkan ini
    public string Title { get; set; } = string.Empty;
    public IssueStatus Status { get; set; }
    public PriorityLevel Priority { get; set; }

        public string ProjectCode { get; set; } = default!;

    // 🆕 Tambahan opsional untuk pelengkap
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? Deadline { get; set; }
    public int? EstimatedFixHours { get; set; }

    // 🧑 Creator (optional, tapi berguna untuk audit log / UI)
    public CreatorUserDto? Creator { get; set; }

    public List<TagDto> Tags { get; set; } = new();
    public List<AssignedUserDto> AssignedUsers { get; set; } = new();
}


public class CreatorUserDto
{
    public Guid UserId { get; set; }
    public string NRP { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string Role { get; set; } = "";
    public string? ProfilePictureUrl { get; set; }
}



public class TagDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Color { get; set; } = "#cccccc";
    public string? Category { get; set; } // tambah properti category
}



public class AssignedUserDto
{
    public Guid UserId { get; set; }
    public string NRP { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string Role { get; set; } = "";
    public string? Position { get; set; }
    public string? Department { get; set; }
    public string? ProfilePictureUrl { get; set; }
}
