using BugNest.Domain.Entities;

public interface INotificationService
{
    Task NotifyAsync(Notification notification);
    Task MarkAsReadAsync(Guid notificationId, Guid userId);
    Task MarkAllAsReadAsync(Guid userId);

    // ✅ Notifikasi saat user ditugaskan ke issue
    Task NotifyAssignedToIssue(Issue issue, IEnumerable<User> users);

    // ✅ Notifikasi saat user ditambahkan ke project
    Task NotifyAddedToProject(Project project, IEnumerable<User> users);
}
