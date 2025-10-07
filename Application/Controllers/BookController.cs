using codex_backend.Application.Dtos;
using codex_backend.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace codex_backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController(BookService service) : ControllerBase
{
    private readonly BookService _service = service;
    [HttpPost("create-book")]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] BookCreateDto book)
    {
        try
        {
           var createdBook = await _service.CreateBookAsync(book);
           return Ok(createdBook.Title);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get-all-books")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<BookReadDto>>> GetAll()
    {
        try
        {
            var books = await _service.GetAllBooksAsync();
            return Ok(books);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<BookReadDto>> BetById(Guid id)
    {
        try
        {
            var book = await _service.GetBookByIdAsync(id);
            return Ok(book);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("by-name/{title}")]
    [Authorize]
    public async Task<ActionResult<BookReadDto>> GetByName(string title)
    {
        try
        {
            var book = await _service.GetBookByNameAsync(title);
            return Ok(book);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpPut("update-book/{id:guid}")]
    public async Task<ActionResult<BookReadDto>> Put(Guid id, [FromBody] BookUpdateDto book)
    {
        try
        {
            var updatedBook = await _service.UpdateBookAsync(id, book);
            return Ok(updatedBook);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpDelete("delete-book/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _service.DeleteBookAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}