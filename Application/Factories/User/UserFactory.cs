using codex_backend.Application.Dtos;
using codex_backend.Helpers;
using codex_backend.Models;

namespace codex_backend.Application.Factories;

public class UserFactory : IUserFactory
{
    public async Task<User?> CreateUserAsync(UserCreateDto dto)
    {
        return new User
        {
            Id = dto.Id,
            Username = dto.Username,
            FirstName = dto.FirstName + " ",
            LastName = dto.LastName,
            Email = dto.Email,
            Password_Hash = PasswordHasher.Hash(dto.Password_Hash!),
            RoleId = dto.RoleId,
            CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-3), DateTimeKind.Utc),
            UpdatedAt = null,
            DeletedAt = null
        };

    }

}