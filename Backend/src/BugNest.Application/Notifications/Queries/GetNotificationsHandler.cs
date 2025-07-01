using BugNest.Application.DTOs.Notifications;
using BugNest.Application.Interfaces;
using BugNest.Application.UseCases.Notifications.Queries;
using MediatR;

public class GetNotificationsHandler : IRequestHandler<GetNotificationsQuery, List<NotificationDto>>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserRepository _userRepository;

    public GetNotificationsHandler(
        INotificationRepository notificationRepository,
        IUserRepository userRepository)
    {
        _notificationRepository = notificationRepository;
        _userRepository = userRepository;
    }

    public async Task<List<NotificationDto>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByNRPAsync(request.NRP);
        if (user is null)
            throw new Exception("User tidak ditemukan");

        var notifications = request.OnlyUnread
            ? await _notificationRepository.GetUnreadAsync(user.Id)
            : await _notificationRepository.GetByUserIdAsync(user.Id);

        return notifications
            .OrderByDescending(n => n.CreatedAt)
            .Select(n => new NotificationDto
            {
                Id = n.Id,
                Title = n.Title,
                Message = n.Message,
                Link = n.Link,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt
            }).ToList();
    }
}
