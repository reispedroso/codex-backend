using codex_backend.Application.Common.Exceptions;
using codex_backend.Application.Dtos;
using codex_backend.Application.Services.Interfaces;
using codex_backend.Application.Services.Token;
using codex_backend.Helpers;

namespace codex_backend.Application.Services.Implementations;

public class AuthService(
    IUserService userService,
    IJwtService jwtService,
    IRoleService roleService
    )
: IAuthService
{
    private readonly IUserService _userService = userService;
    private readonly IJwtService _jwtService = jwtService;
    private readonly IRoleService _roleService = roleService;

    public async Task<LoginResponseDto> RegisterAsync(UserCreateDto userDto)
    {
        var user = await _userService.CreateUserAsync(userDto);

      var tokenResult = await _jwtService.GenerateTokenAsync(user);
        var roleName = await _roleService.GetRoleNameAsync(user.RoleId);

        var responseDto = new LoginResponseDto
        {
            AccessToken = tokenResult.AccessToken,
            Expiration = tokenResult.Expiration,
            User = new UserInfoDto
            {
                Email = user.Email!,
                Role = roleName!
            }
        };

        return responseDto;
      
    }
    public async Task<LoginResponseDto> LoginAsync(UserLoginDto userLoginDto)
    {
        var user = await _userService.GetUserByEmailAsync(userLoginDto.Email)
        ?? throw new InvalidException("Invalid email or password");

        if (!PasswordHasher.Verify(userLoginDto.Password, user.Password_Hash!))
        {
            throw new InvalidException("Invalid email or password");
        }

        var userReadDto = new UserReadDto
        {
            Id = user.Id,
            Email = user.Email!,
            RoleId = user.RoleId
        };

        var tokenResult = await _jwtService.GenerateTokenAsync(userReadDto);
        var roleName = await _roleService.GetRoleNameAsync(user.RoleId);

        var responseDto = new LoginResponseDto
        {
            AccessToken = tokenResult.AccessToken,
            Expiration = tokenResult.Expiration,
            User = new UserInfoDto
            {
                Email = user.Email!,
                Role = roleName!
            }
        };

        return responseDto;
    }

}