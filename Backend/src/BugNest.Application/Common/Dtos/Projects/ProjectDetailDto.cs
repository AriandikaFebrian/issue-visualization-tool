namespace BugNest.Application.DTOs.Projects;

public class ProjectDetailDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ProjectCode { get; set; }
    public string? RepositoryUrl { get; set; }
    public string? DocumentationUrl { get; set; }
    public string Status { get; set; } = "Planning";
    public string Visibility { get; set; } = "Private";
    public DateTime? UpdatedAt { get; set; }
    public string OwnerNRP { get; set; } = string.Empty;
    public string OwnerUsername { get; set; } = string.Empty;
    public string? OwnerFullName { get; set; }
}
