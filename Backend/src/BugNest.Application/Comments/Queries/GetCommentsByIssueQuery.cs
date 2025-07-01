using MediatR;
using BugNest.Application.DTOs.Comments;

namespace BugNest.Application.UseCases.Comments.Queries;

public class GetCommentsByIssueQuery : IRequest<List<CommentDto>>
{
    public string IssueCode { get; set; }

    public GetCommentsByIssueQuery(string issueCode)
    {
        IssueCode = issueCode;
    }
}
