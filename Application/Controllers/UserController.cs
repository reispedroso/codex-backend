using codex_backend.Application.Dtos;
using codex_backend.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace codex_backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    private readonly IUserService _service = service;

    [HttpPost("create-user")]
    // [Authorize]
    public async Task<IActionResult> Post([FromBody] UserCreateDto user)
    {
        try
        {
            var createdUser = await _service.CreateUserAsync(user);
            return Ok(createdUser.Id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get-all-users")]
    public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAll()
    {
        var users = await _service.GetAllUsersAsync();
        return Ok(users);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserReadDto>> GetById(Guid id)
    {
        try
        {
            var user = await _service.GetUserByIdAsync(id);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpPut("update-user/{id:guid}")]
    public async Task<ActionResult<UserReadDto>> Put(Guid id, [FromBody] UserUpdateDto user)
    {
        try
        {
            var updatedUser = await _service.UpdateUserAsync(id, user);
            return Ok(updatedUser);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpDelete("delete-user/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _service.DeleteUserAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}