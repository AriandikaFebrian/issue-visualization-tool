using BugNest.Application.Interfaces;
using MediatR;

namespace BugNest.Application.UseCases.Notifications.Commands;

public class MarkNotificationAsReadCommandHandler : IRequestHandler<MarkNotificationAsReadCommand, Unit>
{
    private readonly INotificationService _notificationService;

    public MarkNotificationAsReadCommandHandler(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task<Unit> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
    {
        await _notificationService.MarkAsReadAsync(request.NotificationId, request.UserId);
        return Unit.Value;
    }
}
