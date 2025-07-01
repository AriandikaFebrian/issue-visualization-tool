using System.Security.Claims;
using Microsoft.AspNetCore.Http;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetNRP()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst("nrp")?.Value;
    }

    public Guid? GetUserId()
    {
        var sub = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.TryParse(sub, out var userId))
            return userId;
        return null;
    }
}
