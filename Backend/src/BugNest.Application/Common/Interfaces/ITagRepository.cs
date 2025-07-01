using BugNest.Domain.Entities;

namespace BugNest.Application.Interfaces;

public interface ITagRepository
{
    // 🔘 Tambah tag baru
    Task AddAsync(Tag tag);

    // 🔍 Ambil semua tag untuk 1 proyek (dengan opsi ambil juga global tag)
    Task<List<Tag>> GetByProjectCodeAsync(string projectCode, bool includeGlobal = true);

    // 🔍 Ambil semua tag (semua project + global)
    Task<List<Tag>> GetAllAsync();

    // 🔍 Ambil semua tag hanya untuk 1 proyek saja (tanpa global)
    Task<List<Tag>> GetByProjectOnlyAsync(string projectCode);

    // 🔍 Ambil tag berdasarkan ID
    Task<Tag?> GetByIdAsync(Guid tagId);

    // 🗑️ Archive tag (soft delete)
    Task ArchiveAsync(Guid tagId);

    // 💾 Commit perubahan
    Task SaveChangesAsync();
}
