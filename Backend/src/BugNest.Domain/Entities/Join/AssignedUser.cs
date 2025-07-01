namespace BugNest.Domain.Entities;
public class AssignedUser
{
    public Guid IssueId { get; set; }
    public Issue? Issue { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }
}