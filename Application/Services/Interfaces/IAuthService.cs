using codex_backend.Application.Dtos;

namespace codex_backend.Application.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> RegisterAsync(UserCreateDto userDto);
    Task<LoginResponseDto> LoginAsync(UserLoginDto userLoginDto);
}
