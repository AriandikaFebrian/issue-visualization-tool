using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;

namespace BugNest.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationService(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task NotifyAsync(Notification notification)
    {
        await _notificationRepository.AddAsync(notification);
        await _notificationRepository.SaveChangesAsync();
    }

    public async Task MarkAsReadAsync(Guid notificationId, Guid userId)
    {
        await _notificationRepository.MarkAsReadAsync(notificationId, userId);
    }

    public async Task MarkAllAsReadAsync(Guid userId)
    {
        await _notificationRepository.MarkAllAsReadAsync(userId);
    }

    public async Task NotifyAssignedToIssue(Issue issue, IEnumerable<User> users)
    {
        var notifications = users.Select(user => new Notification
        {
            RecipientId = user.Id,
            Title = "Ditugaskan ke Issue",
            Message = $"Anda telah ditugaskan ke issue \"{issue.Title}\".",
            Link = $"/issues/{issue.IssueCode}",
            ActionText = "Lihat Issue",
            Icon = "issue",
            CreatedAt = DateTime.UtcNow
        });

        foreach (var notification in notifications)
        {
            await _notificationRepository.AddAsync(notification);
        }

        await _notificationRepository.SaveChangesAsync();
    }

    public async Task NotifyAddedToProject(Project project, IEnumerable<User> users)
{
    var notifications = users.Select(user => new Notification
    {
        RecipientId = user.Id,
        Title = "Ditambahkan ke Project",
        Message = $"Anda telah ditambahkan ke project \"{project.Name}\".",
        Link = $"/projects/{project.ProjectCode}",
        ActionText = "Lihat Project",
        Icon = "project",
        CreatedAt = DateTime.UtcNow
    });

    foreach (var notification in notifications)
    {
        await _notificationRepository.AddAsync(notification);
    }

    await _notificationRepository.SaveChangesAsync();
}

}
