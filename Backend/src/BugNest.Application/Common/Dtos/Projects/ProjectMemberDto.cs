// 📁 BugNest.Application/DTOs/Projects/ProjectMemberDto.cs
namespace BugNest.Application.DTOs.Projects;

public class ProjectMemberDto
{
    public string UserNRP { get; set; } = "";
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string Role { get; set; } = ""; // Ambil dari User.Role.ToString()
}

