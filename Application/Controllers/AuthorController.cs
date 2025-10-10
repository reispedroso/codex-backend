using codex_backend.Application.Authorization.Wrappers;
using codex_backend.Application.Dtos;
using codex_backend.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace codex_backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AuthorController(IAuthorService service) : ControllerBase
{
    private readonly IAuthorService _service = service;

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] AuthorCreateDto dto)
    {
        var createdAuthor = await _service.CreateAuthorAsync(dto);
        var response = new ApiSingleResponse<AuthorReadDto>(true, "Author created successfully", createdAuthor);
        return Ok(response);
    }

    [HttpGet("get-all")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<AuthorReadDto>>> GetAll()
    {
        var authors = await _service.GetAllAuthorsAsync();
        var response = new ApiListResponse<AuthorReadDto>(true, "", authors);
        return Ok(response);
    }

    [HttpGet("by-id/{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthorReadDto>> GetById(Guid id)
    {
        var author = await _service.GetAuthorByIdAsync(id);
        var response = new ApiSingleResponse<AuthorReadDto>(true, "", author);
        return Ok(response);
    }
    
    [HttpPut("update/{id}")]
    public async Task<ActionResult<AuthorReadDto>> Update(Guid id, [FromBody] AuthorUpdateDto dto)
    {
        var updatedAuthor = await _service.UpdateAuthorAsync(id, dto);
        var response = new ApiSingleResponse<AuthorReadDto>(true, "Author updated successfully", updatedAuthor);
        return Ok(response);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAuthorAsync(id);
        return NoContent();
    }
}