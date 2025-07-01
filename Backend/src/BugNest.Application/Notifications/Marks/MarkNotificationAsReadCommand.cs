using MediatR;

namespace BugNest.Application.UseCases.Notifications.Commands;

public class MarkNotificationAsReadCommand : IRequest<Unit>
{
    public Guid NotificationId { get; set; }
    public Guid UserId { get; set; }

    public MarkNotificationAsReadCommand(Guid notificationId, Guid userId)
    {
        NotificationId = notificationId;
        UserId = userId;
    }
}
