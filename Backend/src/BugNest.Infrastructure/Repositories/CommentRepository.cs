using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;
using BugNest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BugNest.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly BugNestDbContext _context;

    public CommentRepository(BugNestDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
    }

    // 🔄 Ganti dari GetByIssueIdAsync ➡ GetByIssueCodeAsync
    public async Task<List<Comment>> GetByIssueCodeAsync(string issueCode)
    {
        return await _context.Comments
            .Include(c => c.User) // 🔍 ambil data user (nama, nrp, avatar, dll)
            .Include(c => c.Issue)
            .Where(c => c.Issue != null && c.Issue.IssueCode == issueCode)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
