using BugNest.Domain.Enums;

namespace BugNest.Domain.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ProjectCode { get; set; }
    public string? RepositoryUrl { get; set; }
    public string? DocumentationUrl { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ProjectStatus? Status { get; set; } = ProjectStatus.Planning;
    public ProjectVisibility Visibility { get; set; } = ProjectVisibility.Private;
    public Guid OwnerId { get; set; }
    public User? Owner { get; set; }
    public ICollection<ProjectMember> Members { get; set; } = new List<ProjectMember>();
    public ICollection<Issue> Issues { get; set; } = new List<Issue>();
    public ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}