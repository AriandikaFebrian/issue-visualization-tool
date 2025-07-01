namespace BugNest.Application.Common.Dtos;

public class LoginRequestDto
{
    public string Identifier { get; set; } = string.Empty; // bisa email atau NRP
    public string Password { get; set; } = string.Empty;
}
