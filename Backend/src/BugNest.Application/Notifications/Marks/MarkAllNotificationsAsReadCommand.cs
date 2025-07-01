// Application/UseCases/Notifications/Commands/MarkAllNotificationsAsReadCommand.cs
using MediatR;
using System;

namespace BugNest.Application.UseCases.Notifications.Commands;


public class MarkAllNotificationsAsReadCommand : IRequest<Unit>
{
    public Guid UserId { get; set; }

    public MarkAllNotificationsAsReadCommand(Guid userId)
    {
        UserId = userId;
    }
}
