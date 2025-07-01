using BugNest.Domain.Enums;


namespace BugNest.Application.DTOs.Projects;

public class OwnerProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";

    public string? ProjectCode { get; set; }
    public string? RepositoryUrl { get; set; }
    public string? DocumentationUrl { get; set; }

    public ProjectStatus? Status { get; set; }
    public ProjectVisibility Visibility { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Guid OwnerId { get; set; }
    public string OwnerName { get; set; } = "";
    public string OwnerEmail { get; set; } = "";
    public string? OwnerProfilePictureUrl { get; set; }

    public int MemberCount { get; set; }
    public int IssueCount { get; set; }

    public List<string> Members { get; set; } = new();
    public List<string> Issues { get; set; } = new();
}
