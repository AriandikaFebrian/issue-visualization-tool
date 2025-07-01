using BugNest.Application.DTOs.Notifications;
using MediatR;

namespace BugNest.Application.UseCases.Notifications.Queries;

public class GetNotificationsQuery : IRequest<List<NotificationDto>>
{
    public string NRP { get; }
    public bool OnlyUnread { get; }

    public GetNotificationsQuery(string nrp, bool onlyUnread = false)
    {
        NRP = nrp;
        OnlyUnread = onlyUnread;
    }
}
