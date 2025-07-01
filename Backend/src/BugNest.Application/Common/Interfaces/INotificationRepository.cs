using BugNest.Domain.Entities;

namespace BugNest.Application.Interfaces
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification notification);
        Task<List<Notification>> GetByUserIdAsync(Guid userId);
        Task<List<Notification>> GetUnreadAsync(Guid userId);
        Task MarkAsReadAsync(Guid notificationId);
        Task MarkAllAsReadAsync(Guid userId);
        Task SoftDeleteAsync(Guid notificationId);
    Task MarkAsReadAsync(Guid notificationId, Guid userId);
        Task SaveChangesAsync();
    }
}
