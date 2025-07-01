using BugNest.Domain.Entities;

namespace BugNest.Application.Interfaces;

public interface ICommentRepository
{
    // ➕ Tambah komentar baru
    Task AddAsync(Comment comment);

    // 🔍 Ambil semua komentar berdasarkan IssueCode (bukan IssueId lagi)
    Task<List<Comment>> GetByIssueCodeAsync(string issueCode);

    // 💾 Simpan perubahan
    Task SaveChangesAsync();
}
