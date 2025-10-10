using codex_backend.Application.Dtos;
using codex_backend.Helpers;
using codex_backend.Models;

namespace codex_backend.Application.Factories;

public class UserFactory : IUserFactory
{
  public Task<User?> CreateUser(UserCreateDto dto, Guid roleId)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = dto.Username,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Password_Hash = PasswordHasher.Hash(dto.Password!),
            RoleId = roleId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            DeletedAt = null
        };

        return Task.FromResult<User?>(user);
    }

}