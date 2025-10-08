using codex_backend.Application.Dtos;
using codex_backend.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace codex_backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserCreateDto userCreateDto)
    {
        var userReadDto = await _authService.RegisterAsync(userCreateDto);

        return StatusCode(201, userReadDto);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {

        var userReadDto = await _authService.LoginAsync(userLoginDto);
        return Ok(userReadDto);
    }
}