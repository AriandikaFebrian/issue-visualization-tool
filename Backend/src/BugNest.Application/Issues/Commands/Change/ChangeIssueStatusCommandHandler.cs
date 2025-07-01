using BugNest.Application.Interfaces;
using BugNest.Application.UseCases.Issues.Commands;
using BugNest.Domain.Enums;
using MediatR;

namespace BugNest.Application.UseCases.Issues.Commands;

public class ChangeIssueStatusCommandHandler : IRequestHandler<ChangeIssueStatusCommand, Unit>
{
    private readonly IIssueRepository _issueRepository;
    private readonly IIssueHistoryRepository _historyRepository;
    private readonly IUserRepository _userRepository;

    public ChangeIssueStatusCommandHandler(
        IIssueRepository issueRepository,
        IIssueHistoryRepository historyRepository,
        IUserRepository userRepository)
    {
        _issueRepository = issueRepository;
        _historyRepository = historyRepository;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(ChangeIssueStatusCommand request, CancellationToken cancellationToken)
    {
        var issue = await _issueRepository.GetByCodeAsync(request.IssueCode);
        if (issue == null)
            throw new Exception("Issue not found");

        var user = await _userRepository.GetByNRPAsync(request.ChangedByNRP);
        if (user == null)
            throw new Exception("User not found");

        var oldStatus = issue.Status;
        issue.Status = request.NewStatus;

        await _historyRepository.AddAsync(new IssueHistory
        {
            IssueId = issue.Id,
            ChangeType = IssueChangeType.StatusChanged,
            PreviousValue = oldStatus.ToString(),
            NewValue = request.NewStatus.ToString(),
            Note = request.Note,
            ChangedBy = user.Id,
            ChangedByUser = user,
            ChangedByUsername = user.Username,
            ChangedByProfileUrl = user.ProfilePictureUrl,
            CreatedAt = DateTime.UtcNow
        });

        await _issueRepository.SaveChangesAsync();
        await _historyRepository.SaveChangesAsync();

       request.ChangedIssueId = issue.Id;
request.ProjectId = issue.ProjectId;
request.PreviousValue = oldStatus.ToString();
request.NewValue = request.NewStatus.ToString();


        return Unit.Value;
    }
}
