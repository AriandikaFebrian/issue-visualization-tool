using BugNest.Domain.Entities;

namespace BugNest.Application.Interfaces;

public interface ICommentRepository
{
    Task AddAsync(Comment comment);
    Task<List<Comment>> GetByIssueCodeAsync(string issueCode);
    Task SaveChangesAsync();
}
