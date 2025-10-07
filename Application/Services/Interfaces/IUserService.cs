using codex_backend.Application.Dtos;
using codex_backend.Models;

namespace codex_backend.Application.Services.Interfaces;

public interface IUserService
{
    Task<UserReadDto> CreateUserAsync(UserCreateDto user);
    Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
    Task<UserReadDto> UpdateUserAsync(Guid id, UserUpdateDto user);
    Task DeleteUserAsync(Guid id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<UserReadDto> GetUserByIdAsync(Guid id);
    Task<bool> VerifyEmail(string email);
}