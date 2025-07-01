using Microsoft.AspNetCore.Http;

namespace BugNest.API.Requests;

public class RegisterUserRequest
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = "Developer";
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Department { get; set; }
    public string? Position { get; set; }
    public IFormFile? ProfilePicture { get; set; }
}
