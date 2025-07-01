namespace BugNest.API.Requests.Comments;

public class CreateCommentRequest
{
    public string IssueCode { get; set; } = null!;
    public string Content { get; set; } = null!;
}
