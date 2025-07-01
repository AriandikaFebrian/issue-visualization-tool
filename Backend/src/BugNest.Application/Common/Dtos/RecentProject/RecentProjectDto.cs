

public class RecentProjectDto
{
    public string ProjectCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? RepositoryUrl { get; set; }
    public string? DocumentationUrl { get; set; }
    public DateTime AccessedAt { get; set; }
}
