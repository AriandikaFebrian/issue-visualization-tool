namespace BugNest.Application.Common.Dtos;
public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;

    // Identitas dasar
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    // Identitas tambahan & profil
    public string? NRP { get; set; }
    public string? FullName { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? Department { get; set; }
    public string? Position { get; set; }
}
