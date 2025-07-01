using BugNest.Domain.Entities;

namespace BugNest.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByNRPAsync(string nrp);
    Task<bool> EmailExistsAsync(string email);
    Task<bool> NRPExistsAsync(string nrp);
    Task<bool> UsernameExistsAsync(string username);
    Task<bool> UsernameExistsAsync(string username, Guid excludeUserId);
    Task<List<User>> GetUsersByIdsAsync(List<Guid> ids);
    Task<List<User>> GetByNRPsAsync(List<string> nrps);
    Task<List<User>> SearchUsersAsync(string keyword);
    
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task SaveChangesAsync();
}
