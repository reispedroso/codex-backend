using codex_backend.Application.Dtos;
using codex_backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace codex_backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookItemController(BookItemService service) : ControllerBase
{
    private readonly BookItemService _service = service;

    [HttpPost("create-book-item")]
    public async Task<IActionResult> Post([FromBody] BookItemCreateDto bookItem)
    {
        try
        {
            var createdBookItem = await _service.CreateBookItemAsync(bookItem);
            return Ok(createdBookItem.Id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BookItemReadDto>> GetById(Guid id)
    {
        try
        {
            var bookItem = await _service.GetBookItemByIdAsync(id);
            return Ok(bookItem);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpPut("update-book-item/{id:guid}")]
    public async Task<ActionResult<BookItemReadDto>> Put(Guid id, [FromBody] BookItemUpdateDto bookItem)
    {
        try
        {
            var updatedBookItem = await _service.UpdateBookItemAsync(id, bookItem);
            return Ok(updatedBookItem);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}