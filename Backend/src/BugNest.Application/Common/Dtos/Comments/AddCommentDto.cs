public class AddCommentDto
{
    public Guid IssueId { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; } = string.Empty;
}