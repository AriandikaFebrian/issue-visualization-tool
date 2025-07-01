using BugNest.Application.DTOs.ActivityLogs;
using BugNest.Application.Interfaces;
using BugNest.Domain.Enums;
using MediatR;

public class GetActivityLogDetailByIdQueryHandler : IRequestHandler<GetActivityLogDetailByIdQuery, ActivityLogDetailDto>
{
    private readonly IActivityLogRepository _logRepository;
    private readonly IIssueHistoryRepository _historyRepository;
    private readonly IIssueRepository _issueRepository;

    public GetActivityLogDetailByIdQueryHandler(
        IActivityLogRepository logRepository,
        IIssueHistoryRepository historyRepository,
        IIssueRepository issueRepository)
    {
        _logRepository = logRepository;
        _historyRepository = historyRepository;
        _issueRepository = issueRepository;
    }

    public async Task<ActivityLogDetailDto> Handle(GetActivityLogDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var log = await _logRepository.GetByIdAsync(request.Id);
        if (log == null) throw new Exception("Activity log not found");

        string? previousValue = null, newValue = null, note = null;
        string? targetCode = null, targetTitle = null;

        if (log.TargetEntityType == ActivityEntityType.Issue &&
            log.Action == ActivityAction.ChangedIssueStatus)
        {
            var history = await _historyRepository
                .GetLatestStatusChangeAsync(log.TargetEntityId, log.UserId);

            if (history != null)
            {
                previousValue = history.PreviousValue;
                newValue = history.NewValue;
                note = history.Note;
            }

            var issue = await _issueRepository.GetByIdAsync(log.TargetEntityId);
            if (issue != null)
            {
                targetCode = issue.IssueCode; // ✅ benar
                targetTitle = issue.Title;
            }
        }

        return new ActivityLogDetailDto
        {
            Id = log.Id,
            UserId = log.UserId,
            Username = log.User?.Username ?? "Unknown",
            UserProfileUrl = log.User?.ProfilePictureUrl,
            Action = log.Action,
            TargetEntityId = log.TargetEntityId,
            TargetEntityType = log.TargetEntityType,
            Summary = log.Summary,
            SourcePlatform = log.SourcePlatform,
            IPAddress = log.IPAddress,
            CreatedAt = log.CreatedAt,

            PreviousValue = previousValue,
            NewValue = newValue,
            Note = note,
            TargetEntityCode = targetCode,
            TargetEntityName = targetTitle
        };
    }
}
