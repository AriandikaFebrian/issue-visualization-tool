
namespace BugNest.Domain.Entities;
public class Notification : BaseEntity
{
    public Guid RecipientId { get; set; }
    public User? Recipient { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? Link { get; set; }
    public string? ActionText { get; set; }
    public string? Icon { get; set; }
    public bool IsRead { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    public DateTime? ReadAt { get; set; }
}
