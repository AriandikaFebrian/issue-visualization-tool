namespace BugNest.Application.DTOs.Comments;

public class CommentDto
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string AuthorNRP { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsEdited { get; set; }
}
