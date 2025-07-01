using MediatR;
using BugNest.Application.DTOs.Comments;
using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;
using BugNest.Application.UseCases.Comments.Commands;

namespace BugNest.Application.UseCases.Comments.Handlers;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentDto>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IIssueRepository _issueRepository;

    public CreateCommentCommandHandler(
        ICommentRepository commentRepository,
        IUserRepository userRepository,
        IIssueRepository issueRepository)
    {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
        _issueRepository = issueRepository;
    }

   public async Task<CommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
{
    var user = await _userRepository.GetByNRPAsync(request.NRP);
    if (user == null)
        throw new Exception("User not found");

    var issue = await _issueRepository.GetByCodeAsync(request.IssueCode);
    if (issue == null)
        throw new Exception("Issue not found");

    var comment = new Comment
    {
        IssueId = issue.Id,
        UserId = user.Id,
        Content = request.Content,
        CreatedAt = DateTime.UtcNow,
        IsEdited = false
    };

    await _commentRepository.AddAsync(comment);
    await _commentRepository.SaveChangesAsync();

    // ✅ Penting: isi agar pipeline bisa mencatat aktivitas
    request.CreatedCommentId = comment.Id;
    request.ProjectId = issue.ProjectId;

    return new CommentDto
    {
        Id = comment.Id,
        Content = comment.Content,
        CreatedAt = comment.CreatedAt,
        IsEdited = comment.IsEdited,
        Username = user.Username ?? "Unknown",
        AuthorNRP = user.NRP,
        AuthorName = user.FullName ?? user.Username ?? "Unknown",
        ProfilePictureUrl = user.ProfilePictureUrl
    };
}

}
