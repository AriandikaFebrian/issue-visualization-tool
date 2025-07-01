using BugNest.Application.DTOs.ActivityLogs;
using BugNest.Application.Interfaces;
using MediatR;

public class GetActivityLogsQueryHandler : IRequestHandler<GetActivityLogsQuery, GetActivityLogsResult>
{
    private readonly IActivityLogRepository _logRepository;

    public GetActivityLogsQueryHandler(IActivityLogRepository logRepository)
    {
        _logRepository = logRepository;
    }

    public async Task<GetActivityLogsResult> Handle(GetActivityLogsQuery request, CancellationToken cancellationToken)
    {
        var logs = await _logRepository.GetAllAsync();

        var result = logs
            .OrderByDescending(log => log.CreatedAt)
            .Select(log => new ActivityLogDto
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
                CreatedAt = log.CreatedAt
            })
            .ToList();

        var paged = result
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        return new GetActivityLogsResult
        {
            Total = result.Count,
            Page = request.Page,
            PageSize = request.PageSize,
            Data = paged
        };
    }
}
