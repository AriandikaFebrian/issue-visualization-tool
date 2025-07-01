namespace BugNest.Application.Common.Dtos;

public class RegisterUserDto
{
    // Wajib
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    // Default Role = Developer (boleh override)
    public string Role { get; set; } = "Developer";

    // Tambahan info profil
    public string? NRP { get; set; }
    public string? FullName { get; set; }
    public string? Department { get; set; }  // enum / string
    public string? Position { get; set; }    // enum / string
    public string? PhoneNumber { get; set; }
    public string? ProfilePictureUrl { get; set; } // bisa null saat register
}
