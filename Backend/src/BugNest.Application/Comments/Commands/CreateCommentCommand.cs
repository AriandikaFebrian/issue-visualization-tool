using BugNest.Application.DTOs.Comments;
using BugNest.Domain.Enums;
using MediatR;

namespace BugNest.Application.UseCases.Comments.Commands;

public class CreateCommentCommand : IRequest<CommentDto>, IAuditableCommand
{
    public string IssueCode { get; set; }
    public string Content { get; set; }
    public string NRP { get; set; }
    public Guid CreatedCommentId { get; set; }
    public Guid ProjectId { get; set; }

    public CreateCommentCommand(string issueCode, string content, string nrp)
    {
        IssueCode = issueCode;
        Content = content;
        NRP = nrp;
    }
    public ActivityAction Action => ActivityAction.AddedComment;
    public ActivityEntityType TargetEntityType => ActivityEntityType.Comment;
    public Guid TargetEntityId => CreatedCommentId;
    public string Summary => $"User {NRP} commented on issue {IssueCode}";
    public string? PerformedByNRP => NRP;
}

