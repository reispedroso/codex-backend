using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Database;
using codex_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace codex_backend.Infra.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;
    public async Task<User> CreateUserAsync(User User)
    {
        await _context.Users.AddAsync(User);
        await _context.SaveChangesAsync();
        return User;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users
           .ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(Guid UserId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
    }

    public async Task<User?> GetUserByEmailAsync(string userEmail)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
    }

    public async Task<User?> GetUserByNameAsync(string Username)
    {
        return await _context.Users
                 .FirstOrDefaultAsync(u => u.Username == Username);
    }

    public async Task<bool> UpdateUserAsync(User User)
    {
        _context.Users.Update(User);
        var updated = await _context.SaveChangesAsync();
        return updated > 0;

    }
}