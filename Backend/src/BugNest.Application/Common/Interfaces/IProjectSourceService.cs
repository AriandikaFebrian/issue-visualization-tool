using Microsoft.AspNetCore.Http;

namespace BugNest.Application.Interfaces;

public interface IProjectSourceService
{
    Task<string> UploadAndExtractSourceAsync(Guid projectId, IFormFile zipFile);
    Task<string> GetFileContentAsync(Guid projectId, string relativePath);

}
