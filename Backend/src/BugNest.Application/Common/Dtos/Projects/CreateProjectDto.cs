using BugNest.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace BugNest.Application.Common.Dtos;

public class CreateProjectDto
{
    // ✅ Informasi Proyek
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ProjectCode { get; set; }

    // ✅ Metadata Teknis
    public string? RepositoryUrl { get; set; }
    public string? DocumentationUrl { get; set; }

    // ✅ Status & Visibility
    public ProjectStatus? Status { get; set; } = ProjectStatus.Planning;
    public ProjectVisibility Visibility { get; set; } = ProjectVisibility.Private;
    
}
