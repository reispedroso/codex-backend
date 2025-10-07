using codex_backend.Application.Dtos;
using codex_backend.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace codex_backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookController(IBookService service) : ControllerBase
{
    private readonly IBookService _service = service;
    [HttpPost("create-book")]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] BookCreateDto book)
    {
        var createdBook = await _service.CreateBookAsync(book);
        return Ok(createdBook.Title);

    }

    [HttpGet("get-all-books")]
    public async Task<ActionResult<IEnumerable<BookReadDto>>> GetAll()
    {
        var books = await _service.GetAllBooksAsync();
        return Ok(books);

    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BookReadDto>> BetById(Guid id)
    {
        var book = await _service.GetBookByIdAsync(id);
        return Ok(book);

    }

    [HttpGet("by-name/{title}")]
    public async Task<ActionResult<BookReadDto>> GetByName(string title)
    {
        var book = await _service.GetBookByNameAsync(title);
        return Ok(book);

    }

    [HttpPut("update-book/{id}")]
    public async Task<ActionResult<BookReadDto>> Put(Guid id, [FromBody] BookUpdateDto book)
    {
        var updatedBook = await _service.UpdateBookAsync(id, book);
        return Ok(updatedBook);

    }

    [HttpDelete("delete-book/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteBookAsync(id);
        return NoContent();

    }
}