using codex_backend.Application.Dtos;
using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Application.Validators;
using codex_backend.Helpers;
using codex_backend.Models;
using codex_backend.Application.Factories;
using codex_backend.Application.Services.Interfaces;

namespace codex_backend.Application.Services;

public class UserService(
IUserRepository userRepository,
IUserFactory factory
) : IUserService
{
private readonly IUserRepository _userRepository = userRepository;
private readonly IUserFactory _factory = factory;
public async Task<UserReadDto> CreateUserAsync(UserCreateDto user)
{
    InvalidFieldsHelper.ThrowIfInvalid(UserValidator.ValidateCreate(user));

    await EnsureUsernameAndEmailNotInUse(user.Email, user.Username);

    var newUser = await _factory.CreateUserAsync(user);

    await _userRepository.CreateUserAsync(newUser);
    return MapToDto(newUser);
}

public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
{
    var allUsers = await _userRepository.GetAllUsersAsync();
    return allUsers.Select(u => new UserReadDto
    {
        Id = u.Id,
        Username = u.Username!,
        FirstName = u.FirstName!,
        LastName = u.LastName,
        Email = u.Email!,
        RoleId = u.RoleId,
        CreatedAt = u.CreatedAt
    });
}

public async Task<UserReadDto> UpdateUserAsync(Guid id, UserUpdateDto user)
{
    var errors = UserValidator.ValidateUpdate(user);
    if (errors.Any())
        throw new ArgumentException(string.Join("; ", errors));

    var updateUser = await _userRepository.GetUserByIdAsync(id) ?? throw new Exception("User not found");


    updateUser.Username = user.Username;
    updateUser.FirstName = user.FirstName;
    updateUser.LastName = user.LastName;
    updateUser.Email = user.Email;
    updateUser.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

    await EnsureUsernameAndEmailNotInUse(user.Email, user.Username);

    if (!string.IsNullOrWhiteSpace(user.Password_Hash))
    {
        updateUser.Password_Hash = PasswordHasher.Hash(user.Password_Hash);
    }

    await _userRepository.UpdateUserAsync(updateUser);

    return new UserReadDto
    {
        Id = updateUser.Id,
        Username = updateUser.Username!,
        FirstName = updateUser.FirstName!,
        LastName = updateUser.LastName,
        Email = updateUser.Email!,
        RoleId = updateUser.RoleId,
        UpdatedAt = updateUser.UpdatedAt
    };
}

public async Task DeleteUserAsync(Guid id)
{
    var deleteUser = await _userRepository.GetUserByIdAsync(id) ?? throw new Exception("User not found");

    deleteUser.DeletedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

    await _userRepository.UpdateUserAsync(deleteUser);
}

public async Task<User?> GetUserByEmailAsync(string email)
{
    return await _userRepository.GetUserByEmailAsync(email);
}

public async Task<UserReadDto> GetUserByIdAsync(Guid id)
{
    var userById = await _userRepository.GetUserByIdAsync(id)
        ?? throw new Exception("User not found");

    return MapToDto(userById);
}

public async Task<bool> VerifyEmail(string email)
{
    return await _userRepository.GetUserByEmailAsync(email) is not null;
}

private static UserReadDto MapToDto(User u) => new()
{
    Id = u.Id,
    Username = u.Username!,
    FirstName = u.FirstName!,
    LastName = u.LastName,
    Email = u.Email!,
    RoleId = u.RoleId,
    CreatedAt = u.CreatedAt
};

private async Task EnsureUsernameAndEmailNotInUse(string email, string username)
{
    var emailInUse = await _userRepository.GetUserByEmailAsync(email) is not null;
    var usernameInUse = await _userRepository.GetUserByNameAsync(username) is not null;

    switch (emailInUse)
    {
        case true when usernameInUse:
            throw new Exception("Email and username already in use");
        case true:
            throw new Exception("Email in use");
    }

    if (usernameInUse)
        throw new Exception("Username in use");
}


}