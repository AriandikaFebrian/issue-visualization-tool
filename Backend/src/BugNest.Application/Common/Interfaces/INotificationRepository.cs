using BugNest.Domain.Entities;

namespace BugNest.Application.Interfaces
{
    public interface INotificationRepository
    {
        /// <summary>
        /// Menambahkan notifikasi baru untuk user.
        /// </summary>
        Task AddAsync(Notification notification);

        /// <summary>
        /// Mengambil semua notifikasi milik user (berdasarkan RecipientId), urut terbaru.
        /// </summary>
        Task<List<Notification>> GetByUserIdAsync(Guid userId);

        /// <summary>
        /// Mengambil semua notifikasi yang belum dibaca oleh user.
        /// </summary>
        Task<List<Notification>> GetUnreadAsync(Guid userId);

        /// <summary>
        /// Tandai notifikasi tertentu sebagai sudah dibaca.
        /// </summary>
        Task MarkAsReadAsync(Guid notificationId);

        /// <summary>
        /// Tandai semua notifikasi user sebagai sudah dibaca.
        /// </summary>
        Task MarkAllAsReadAsync(Guid userId);

        /// <summary>
        /// Menghapus (soft delete) notifikasi tertentu.
        /// </summary>
        Task SoftDeleteAsync(Guid notificationId);
    Task MarkAsReadAsync(Guid notificationId, Guid userId);


        /// <summary>
        /// Simpan semua perubahan ke database.
        /// </summary>
        Task SaveChangesAsync();
    }
}
