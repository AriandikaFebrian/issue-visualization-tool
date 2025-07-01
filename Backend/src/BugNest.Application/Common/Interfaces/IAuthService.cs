using BugNest.Domain.Entities;

namespace BugNest.Application.Interfaces;

public interface IAuthService
{
    string HashPassword(string password);
    bool VerifyPassword(string hash, string password);
    string GenerateJwtToken(User user);
}