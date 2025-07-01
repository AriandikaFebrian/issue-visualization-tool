using BugNest.Domain.Entities;

namespace BugNest.Application.Interfaces;

public interface IActivityLogRepository
{
    // ➕ Tambah aktivitas baru
    Task AddAsync(ActivityLog log);

    // 🔍 Ambil semua aktivitas berdasarkan proyek (include user)
    Task<List<ActivityLog>> GetByProjectIdAsync(Guid projectId);

    // 🔍 Ambil log berdasarkan pengguna (opsional: untuk profil user)
    Task<List<ActivityLog>> GetByUserIdAsync(Guid userId);

    Task<List<ActivityLog>> GetAllAsync();


    // 🔍 Ambil 1 log berdasarkan ID (jika butuh detail)
    Task<ActivityLog?> GetByIdAsync(Guid id);
    Task<(List<ActivityLog> Logs, int Total)> GetPagedAsync(int page, int pageSize);


    // 💾 Simpan perubahan
    Task SaveChangesAsync();
}
