using codex_backend.Application.Dtos;
using codex_backend.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace codex_backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController(IUserService service) : ControllerBase
{
    private readonly IUserService _service = service;

    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] UserCreateDto user)
    {
        var createdUser = await _service.CreateUserAsync(user);
        return Ok(createdUser.Id);

    }

    [HttpGet("all-users")]
    public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAll()
    {
        var users = await _service.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("by-id/{id}")]
    public async Task<ActionResult<UserReadDto>> GetById(Guid id)
    {

        var user = await _service.GetUserByIdAsync(id);
        return Ok(user);

    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult<UserReadDto>> Put(Guid id, [FromBody] UserUpdateDto user)
    {
        var updatedUser = await _service.UpdateUserAsync(id, user);
        return Ok(updatedUser);

    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteUserAsync(id);
        return NoContent();

    }
}