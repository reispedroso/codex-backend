using codex_backend.Application.Dtos;
using codex_backend.Models;

namespace codex_backend.Application.Factories;

public interface IUserFactory
{
    Task<User?> CreateUserAsync(UserCreateDto dto);
    
}