using BugNest.Application.Common;
using BugNest.Application.Interfaces;
using BugNest.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

namespace BugNest.Infrastructure.Services;

public class ProjectSourceService : IProjectSourceService
{
    private readonly BugNestDbContext _context;
    private readonly IProjectRepository _projectRepository;
    private readonly IWebEnvironment _env;

    public ProjectSourceService(
        BugNestDbContext context,
        IProjectRepository projectRepository,
        IWebEnvironment env)
    {
        _context = context;
        _projectRepository = projectRepository;
        _env = env;
    }

    public async Task<string> UploadAndExtractSourceAsync(Guid projectId, IFormFile zipFile)
    {
        if (zipFile == null || zipFile.Length == 0)
            throw new Exception("ZIP file tidak valid.");

        var project = await _projectRepository.GetByIdAsync(projectId);
        if (project == null)
            throw new Exception("Project tidak ditemukan.");

        var uploadsRoot = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploaded_sources");
        Directory.CreateDirectory(uploadsRoot);

        var safeFolderName = $"{project.ProjectCode}_{Guid.NewGuid()}";
        var extractPath = Path.Combine(uploadsRoot, safeFolderName);

        // Save temporary zip
        var tempZipPath = Path.Combine(uploadsRoot, $"{safeFolderName}.zip");
        using (var stream = new FileStream(tempZipPath, FileMode.Create))
        {
            await zipFile.CopyToAsync(stream);
        }

        // Extract ZIP
        ZipFile.ExtractToDirectory(tempZipPath, extractPath);
        File.Delete(tempZipPath);

        // Save info to project
        project.SourceUploadPath = Path.Combine("uploaded_sources", safeFolderName).Replace("\\", "/");
        project.HasUploadedSource = true;
        await _projectRepository.UpdateAsync(project);
        await _projectRepository.SaveChangesAsync();

        return project.SourceUploadPath!;
    }
    
    public async Task<string> GetFileContentAsync(Guid projectId, string relativePath)
{
    var project = await _projectRepository.GetByIdAsync(projectId);
    if (project == null)
        throw new Exception("Project tidak ditemukan.");

    var basePath = Path.Combine(_env.WebRootPath ?? "wwwroot", project.SourceUploadPath!);
    var fullPath = Path.Combine(basePath, relativePath);

    if (!File.Exists(fullPath))
        throw new FileNotFoundException("File tidak ditemukan.", relativePath);

    return await File.ReadAllTextAsync(fullPath);
}

}
