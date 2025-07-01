using BugNest.Domain.Entities;

public interface INotificationService
{
    Task NotifyAsync(Notification notification);
    Task MarkAsReadAsync(Guid notificationId, Guid userId);
    Task MarkAllAsReadAsync(Guid userId);
    Task NotifyAssignedToIssue(Issue issue, IEnumerable<User> users);
    Task NotifyAddedToProject(Project project, IEnumerable<User> users);
}
