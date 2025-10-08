using System.Security.Claims;
using codex_backend.Application.Dtos;

namespace codex_backend.Application.Services.Token;

public interface IJwtService
{
    Task<TokenResultDto> GenerateTokenAsync(UserReadDto user);
    ClaimsPrincipal? ValidateToken(string token);
}