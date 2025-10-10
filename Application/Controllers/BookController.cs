using codex_backend.Application.Authorization.Wrappers;
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
    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Post([FromBody] BookCreateDto book)
    {
        var newBook = await _service.CreateBookAsync(book);

        var response = new ApiSingleResponse<BookReadDto>(true, "Book created successfully.", newBook);

        return Ok(response);
    }

    [HttpGet("get-all")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<BookReadDto>>> GetAll()
    {
        var books = await _service.GetAllBooksAsync();
        var response = new ApiListResponse<BookReadDto>(true, "", books);
        return Ok(response);
    }

    [HttpGet("by-id/{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<BookReadDto>> GetById(Guid Id)
    {
        var book = await _service.GetBookByIdAsync(Id);
        var response = new ApiSingleResponse<BookReadDto>(true, "Book by id", book);
        return Ok(response);
    }

    [HttpGet("by-name/{title}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<BookReadDto>>> GetByName(string title)
    {
        var books = await _service.SearchBooksByTitleAsync(title);
        var response = new ApiListResponse<BookReadDto>(true, "Book by name", books);
        return Ok(response);
    }

    [HttpPut("update/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BookReadDto>> Put(Guid id, [FromBody] BookUpdateDto book)
    {
        var updatedBook = await _service.UpdateBookAsync(id, book);
        var response = new ApiSingleResponse<BookReadDto>(true, "Book by name", updatedBook);
        return Ok(response);
    }

    [HttpDelete("delete/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteBookAsync(id);
        return NoContent();
    }
}