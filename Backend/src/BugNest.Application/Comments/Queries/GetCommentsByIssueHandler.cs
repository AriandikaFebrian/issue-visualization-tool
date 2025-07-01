using MediatR;
using BugNest.Application.DTOs.Comments;
using BugNest.Application.Interfaces;
using BugNest.Application.UseCases.Comments.Queries;

namespace BugNest.Application.UseCases.Comments.Handlers;

public class GetCommentsByIssueHandler : IRequestHandler<GetCommentsByIssueQuery, List<CommentDto>>
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentsByIssueHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<List<CommentDto>> Handle(GetCommentsByIssueQuery request, CancellationToken cancellationToken)
    {
        var comments = await _commentRepository.GetByIssueCodeAsync(request.IssueCode);

        return comments.Select(c => new CommentDto
        {
            Id = c.Id,
            Content = c.Content,
            Username = c.User?.Username ?? "Unknown",
            CreatedAt = c.CreatedAt,
            AuthorNRP = c.User?.NRP ?? "",
            AuthorName = c.User?.FullName ?? c.User?.Username ?? "Unknown",
            ProfilePictureUrl = c.User?.ProfilePictureUrl,
            IsEdited = c.IsEdited
        }).ToList();
    }
}
