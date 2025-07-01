using BugNest.Application.Common;
using Microsoft.AspNetCore.Hosting;

public class WebEnvironment : IWebEnvironment
{
    private readonly IWebHostEnvironment _env;

    public WebEnvironment(IWebHostEnvironment env)
    {
        _env = env;
    }

    public string WebRootPath => _env.WebRootPath ?? "wwwroot";
}
