using codex_backend.Models;

namespace codex_backend.Application.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task<User?> GetUserByNameAsync(string userName);
    Task<User?> GetUserByEmailAsync(string userEmail);
    Task<User?> GetUserByIdAsync(Guid userId); 
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<bool> UpdateUserAsync(User user);

}
