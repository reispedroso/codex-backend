using codex_backend.Application.Dtos;

namespace codex_backend.Application.Services.Interfaces;

public interface IAuthService
{
    Task<UserReadDto> RegisterAsync(UserCreateDto userDto);
    Task<UserReadDto> LoginAsync(UserLoginDto userLoginDto);
}
