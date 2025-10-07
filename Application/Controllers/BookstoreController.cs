using System.Security.Claims;
using codex_backend.Application.Dtos;
using codex_backend.Application.Services.Interfaces;
using codex_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace codex_backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookstoreController(
        IBookstoreService bookstoreService,
        IAuthorizationService authorizationService) : ControllerBase
{
    private readonly IBookstoreService _bookstoreService = bookstoreService;
    private readonly IAuthorizationService _authorizationService = authorizationService;


    [HttpPost("create-bookstore")]
    public async Task<IActionResult> Post([FromBody] BookstoreCreateDto dto)
    {
        var loggedInUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var createdBookstore = await _bookstoreService.CreateBookstoreAsync(dto, loggedInUserId);

        return CreatedAtAction(nameof(GetById), new { id = createdBookstore.Id }, createdBookstore);
    }

    [HttpGet("get-all-bookstores")]
    public async Task<ActionResult<IEnumerable<BookstoreReadDto>>> GetAll()
    {
        var bookstores = await _bookstoreService.GetAllBookstoresAsync();
        return Ok(bookstores);
    }

    [HttpGet("my-bookstores")]
    public async Task<ActionResult<IEnumerable<BookstoreReadDto>>> GetMyBookstores()
    {
        var loggedInUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var bookstores = await _bookstoreService.GetBookstoresByOwnerIdAsync(loggedInUserId);
        return Ok(bookstores);
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<ActionResult<BookstoreReadDto>> GetById(Guid id)
    {
        var bookstore = await _bookstoreService.GetBookstoreByIdAsync(id);
        return Ok(bookstore);
    }

    [HttpPut("update-bookstore/{id}")]
    public async Task<ActionResult<BookstoreReadDto>> Put(Guid id, [FromBody] BookstoreUpdateDto dto)
    {
        var bookstoreModel = await _bookstoreService.GetBookstoreModelByIdAsync(id);
        if (bookstoreModel is null)
        {
            return NotFound(); 
        }

        var authorizationResult = await _authorizationService
            .AuthorizeAsync(User, bookstoreModel, "CanManageBookstorePolicy");

        if (!authorizationResult.Succeeded)
        {
            return Forbid();
        }

        var updatedBookstore = await _bookstoreService.UpdateBookstoreAsync(id, dto);
        return Ok(updatedBookstore);
    }

    [HttpDelete("delete-bookstore/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var bookstoreModel = await _bookstoreService.GetBookstoreModelByIdAsync(id);
        if (bookstoreModel is null)
        {
            return NotFound();
        }

        var authorizationResult = await _authorizationService
            .AuthorizeAsync(User, bookstoreModel, "CanManageBookstorePolicy");

        if (!authorizationResult.Succeeded)
        {
            return Forbid();
        }

        await _bookstoreService.DeleteBookstoreAsync(id);
        return NoContent();
    }
}
