public class MarkNotificationAsReadHandler
{
    private readonly INotificationService _notificationService;

    public MarkNotificationAsReadHandler(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task Handle(Guid notificationId, Guid userId)
    {
        await _notificationService.MarkAsReadAsync(notificationId, userId);
    }
}

public class MarkAllNotificationsAsReadHandler
{
    private readonly INotificationService _notificationService;

    public MarkAllNotificationsAsReadHandler(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task Handle(Guid userId)
    {
        await _notificationService.MarkAllAsReadAsync(userId);
    }
}
