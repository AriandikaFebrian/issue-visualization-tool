namespace BugNest.Application.DTOs.Comments;

public class CreateCommentDto
{
    public string IssueCode { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}

