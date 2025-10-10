using codex_backend.Application.Dtos;
using codex_backend.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace codex_backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class CategoryController(ICategoryService service) : ControllerBase
{
    private readonly ICategoryService _service = service;

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
    {
        var createdCategory = await _service.CreateCategoryAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
    }

    [HttpGet("get-all")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetAll()
    {
        var categories = await _service.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("by-id/{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<CategoryReadDto>> GetById(Guid id)
    {
        var category = await _service.GetCategoryByIdAsync(id);
        return Ok(category);
    }
    
    [HttpPut("update/{id}")]
    public async Task<ActionResult<CategoryReadDto>> Update(Guid id, [FromBody] CategoryUpdateDto dto)
    {
        var updatedCategory = await _service.UpdateCategoryAsync(id, dto);
        return Ok(updatedCategory);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteCategoryAsync(id);
        return NoContent();
    }
}