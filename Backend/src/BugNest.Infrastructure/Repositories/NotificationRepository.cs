using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;
using BugNest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class NotificationRepository : INotificationRepository
{
    private readonly BugNestDbContext _context;

    public NotificationRepository(BugNestDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Notification notification)
    {
        await _context.Notifications.AddAsync(notification);
    }

    public async Task<List<Notification>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Notifications
            .Where(n => n.RecipientId == userId && !n.IsDeleted)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<Notification>> GetUnreadAsync(Guid userId)
    {
        return await _context.Notifications
            .Where(n => n.RecipientId == userId && !n.IsRead && !n.IsDeleted)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
    }

    public async Task MarkAsReadAsync(Guid notificationId)
    {
        var notif = await _context.Notifications.FindAsync(notificationId);
        if (notif is not null && !notif.IsRead)
        {
            notif.IsRead = true;
        }
    }

    public async Task MarkAllAsReadAsync(Guid userId)
    {
        var notifs = await _context.Notifications
            .Where(n => n.RecipientId == userId && !n.IsRead)
            .ToListAsync();

        foreach (var notif in notifs)
        {
            notif.IsRead = true;
            notif.ReadAt = DateTime.UtcNow;
        }
    }

    public async Task SoftDeleteAsync(Guid notificationId)
    {
        var notif = await _context.Notifications.FindAsync(notificationId);
        if (notif is not null && !notif.IsDeleted)
        {
            notif.IsDeleted = true;
        }
    }

    public async Task MarkAsReadAsync(Guid notificationId, Guid userId)
{
    var notif = await _context.Notifications
        .FirstOrDefaultAsync(n => n.Id == notificationId && n.RecipientId == userId);
    if (notif == null) throw new Exception("Notification not found");

    notif.IsRead = true;
    notif.ReadAt = DateTime.UtcNow;

    await _context.SaveChangesAsync();
}



    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
