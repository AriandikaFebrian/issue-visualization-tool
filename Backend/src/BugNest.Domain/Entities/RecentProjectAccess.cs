using BugNest.Domain.Entities;

public class RecentProjectAccess
{
    public int Id { get; set; }
    public string NRP { get; set; } = string.Empty;
    public string ProjectCode { get; set; } = string.Empty;
    public DateTime AccessedAt { get; set; } = DateTime.UtcNow;

    public Project? Project { get; set; }
}
