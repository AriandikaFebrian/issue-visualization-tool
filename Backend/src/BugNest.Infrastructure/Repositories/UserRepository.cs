using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;
using BugNest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly BugNestDbContext _context;

    public UserRepository(BugNestDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByNRPAsync(string nrp)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.NRP == nrp);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> NRPExistsAsync(string nrp)
    {
        return await _context.Users.AnyAsync(u => u.NRP == nrp);
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }

    public async Task<bool> UsernameExistsAsync(string username, Guid excludeUserId)
    {
        return await _context.Users.AnyAsync(u => u.Username == username && u.Id != excludeUserId);
    }

    public async Task<List<User>> GetUsersByIdsAsync(List<Guid> ids)
    {
        return await _context.Users
            .Where(u => ids.Contains(u.Id))
            .ToListAsync();
    }

    public async Task<List<User>> GetByNRPsAsync(List<string> nrps)
    {
        return await _context.Users
            .Where(u => nrps.Contains(u.NRP))
            .ToListAsync();
    }

    public async Task<List<User>> SearchUsersAsync(string keyword)
    {
        return await _context.Users
            .Where(u =>
                u.Username.Contains(keyword) ||
                u.Email.Contains(keyword) ||
                u.NRP.Contains(keyword) ||
                (u.FullName != null && u.FullName.Contains(keyword)))
            .ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
