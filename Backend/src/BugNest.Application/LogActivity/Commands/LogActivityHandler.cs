using BugNest.Application.DTOs.ActivityLogs;
using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;

namespace BugNest.Application.UseCases.ActivityLogs;

public class LogActivityHandler
{
    private readonly IActivityLogRepository _logRepository;

    public LogActivityHandler(IActivityLogRepository logRepository)
    {
        _logRepository = logRepository;
    }

    public async Task Handle(LogActivityDto dto)
    {
        var log = new ActivityLog
        {
            ProjectId = dto.ProjectId,
            UserId = dto.UserId,
            Action = dto.Action,
            TargetEntityId = dto.TargetEntityId,
            CreatedAt = DateTime.UtcNow
        };

        await _logRepository.AddAsync(log);
        await _logRepository.SaveChangesAsync();
    }
}
