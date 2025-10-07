using codex_backend.Application.Dtos;
using codex_backend.Application.Services.Interfaces;
using codex_backend.Application.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace codex_backend.Application.Services.Token;

public class JwtService(IOptions<TokenSettings> opt, IRoleService roleService) : IJwtService
{
    private readonly TokenSettings _settings = opt.Value;
    private readonly IRoleService _roleService = roleService;
    public async Task<string> GenerateTokenAsync(UserReadDto user)
    {
        var roleDto = await _roleService.GetRoleNameAsync(user.RoleId);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Role, roleDto!)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
        issuer: _settings.Issuer,
        audience: _settings.Audience,
        claims: claims,
        expires: DateTime.SpecifyKind(DateTime.UtcNow.AddDays(_settings.ExpiresInDays), DateTimeKind.Utc),
        signingCredentials: creds
        );

          return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_settings.Key);

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _settings.Issuer,
                ValidateAudience = true,
                ValidAudience = _settings.Audience,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true
            }, out SecurityToken validatedToken);

            return principal;
        }
        catch
        {
            return null;
        }
    }
}