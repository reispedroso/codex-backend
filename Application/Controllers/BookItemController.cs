using codex_backend.Application.Dtos;
using codex_backend.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace codex_backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookItemController(IBookItemService service) : ControllerBase
{
    private readonly IBookItemService _service = service;

    [HttpPost("create-book-item")]
    public async Task<IActionResult> Post([FromBody] BookItemCreateDto bookItem)
    {
        var createdBookItem = await _service.CreateBookItemAsync(bookItem);
        return Ok(createdBookItem.Id);

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookItemReadDto>> GetById(Guid id)
    {
        var bookItem = await _service.GetBookItemByIdAsync(id);
        return Ok(bookItem);

    }

    [HttpPut("update-book-item/{id}")]
    public async Task<ActionResult<BookItemReadDto>> Put(Guid id, [FromBody] BookItemUpdateDto bookItem)
    {
        var updatedBookItem = await _service.UpdateBookItemAsync(id, bookItem);
            return Ok(updatedBookItem);
     
    }
}