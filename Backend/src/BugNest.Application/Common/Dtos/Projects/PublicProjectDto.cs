namespace BugNest.Application.DTOs.Projects;

public class PublicProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ProjectCode { get; set; }
    public string? RepositoryUrl { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
