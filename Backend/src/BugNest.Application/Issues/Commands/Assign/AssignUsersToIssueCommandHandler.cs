using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;
using MediatR;

namespace BugNest.Application.UseCases.Issues.Commands;

public class AssignUsersToIssueCommandHandler : IRequestHandler<AssignUsersToIssueCommand, Unit>
{
    private readonly IIssueRepository _issueRepository;
    private readonly IUserRepository _userRepository;
    private readonly INotificationRepository _notificationRepository;
    private readonly INotificationService _notificationService;

    public AssignUsersToIssueCommandHandler(
        IIssueRepository issueRepository,
        IUserRepository userRepository,
        INotificationService notificationService,
        INotificationRepository notificationRepository)
    {
        _issueRepository = issueRepository;
        _userRepository = userRepository;
        _notificationService = notificationService;
        _notificationRepository = notificationRepository;
    }

    public async Task<Unit> Handle(AssignUsersToIssueCommand request, CancellationToken cancellationToken)
    {
        var issue = await _issueRepository.GetByCodeAsync(request.IssueCode);
        if (issue == null)
            throw new Exception("Issue not found.");

        var userIds = await _issueRepository.GetUserIdsByNRPsAsync(request.NRPs);

        var assignedUsers = userIds.Select(userId => new AssignedUser
        {
            IssueId = issue.Id,
            UserId = userId
        }).ToList();

        await _issueRepository.AddAssignedUsersAsync(assignedUsers);
        await _issueRepository.SaveChangesAsync();

        var users = await _userRepository.GetByNRPsAsync(request.NRPs);

        foreach (var user in users)
        {
            await _notificationRepository.AddAsync(new Notification
            {
                RecipientId = user.Id,
                Title = "Ditugaskan ke issue",
                Message = $"Anda telah ditugaskan ke issue \"{issue.Title}\".",
                Link = $"/issues/{issue.IssueCode}",
                ActionText = "Lihat Issue",
                Icon = "issue",
                CreatedAt = DateTime.UtcNow
            });
        }

        await _notificationRepository.SaveChangesAsync();

        return Unit.Value;
    }
}
