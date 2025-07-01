using BugNest.Application.Interfaces;
using MediatR;

namespace BugNest.Application.UseCases.Notifications.Commands;

public class MarkAllNotificationsAsReadCommandHandler
    : IRequestHandler<MarkAllNotificationsAsReadCommand, Unit>
{
    private readonly INotificationService _notificationService;

    public MarkAllNotificationsAsReadCommandHandler(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task<Unit> Handle(MarkAllNotificationsAsReadCommand request, CancellationToken cancellationToken)
    {
        await _notificationService.MarkAllAsReadAsync(request.UserId);
        return Unit.Value;
    }
}
