using codex_backend.Application.Dtos;
using codex_backend.Application.Services.Interfaces;
using codex_backend.Application.Services.Token;
using codex_backend.Helpers;

namespace codex_backend.Application.Services;

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

    public async Task<UserReadDto> RegisterAsync(UserCreateDto userDto)
    {
        var emailExists = await _userService.VerifyEmail(userDto.Email);
        if (emailExists) throw new Exception("Email already registered");

        var clientRoleId = await _roleService.GetRoleIdAsync("Client");

        userDto.Id = Guid.NewGuid();
        userDto.RoleId = clientRoleId;

        var user = await _userService.CreateUserAsync(userDto);

        var token = await _jwtService.GenerateTokenAsync(user);

        return new UserReadDto
        {
            Id = user.Id,
            Email = user.Email,
            RoleId = user.RoleId,
            Token = token
        };

    }

    public async Task<UserReadDto> LoginAsync(UserLoginDto userLoginDto)
    {
        var user = await _userService.GetUserByEmailAsync(userLoginDto.Email)
        ?? throw new Exception("Invalid email or password");

        if (!PasswordHasher.Verify(userLoginDto.Password, user.Password_Hash))
        {
            throw new Exception("Invalid email or password");
        }

        var userReadDto = new UserReadDto
        {
            Id = user.Id,
            Email = user.Email,
            RoleId = user.RoleId
        };

        var token = await _jwtService.GenerateTokenAsync(userReadDto);

        userReadDto.Token = token;

        return userReadDto;
    }
}