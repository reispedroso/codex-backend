using codex_backend.Application.Dtos;
using codex_backend.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace codex_backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookItemController(
    IBookItemService service,
    IAuthorizationService authorizationService
    ) : ControllerBase
{
    private readonly IBookItemService _service = service;
    private readonly IAuthorizationService _authorizationService = authorizationService;

    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] BookItemCreateDto bookItem)
    {
        var createdBookItem = await _service.CreateBookItemAsync(bookItem);
        return CreatedAtAction(nameof(GetById), new { id = createdBookItem.Id }, createdBookItem);

    }

    [HttpGet("by-id/{id}")]
    public async Task<ActionResult<BookItemReadDto>> GetById(Guid id)
    {
        var bookItem = await _service.GetBookItemByIdAsync(id);
        return Ok(bookItem);
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult<BookItemReadDto>> Put(Guid id, [FromBody] BookItemUpdateDto bookItem)
    {
        var bookItemModel = await _service.GetBookItemWithBookstoreAsync(id);
        if (bookItemModel?.Bookstore is null)
        {
            return NotFound();
        }

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, bookItemModel.BookstoreId, "CanManageBookstorePolicy");
        if (!authorizationResult.Succeeded)
        {
            return Forbid();
        }

        var updatedBookItem = await _service.UpdateBookItemAsync(id, bookItem);
        return Ok(updatedBookItem);

    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<BookItemReadDto>> Delete(Guid id)
    {
        var bookItemModel = await _service.GetBookItemWithBookstoreAsync(id);
        if (bookItemModel?.Bookstore is null)
        {
            return NotFound();
        }
        ;

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, bookItemModel.BookstoreId, "CanManageBookstorePolicy");
        if (!authorizationResult.Succeeded)
        {
            return Forbid();
        }

        var updatedBookItem = await _service.DeleteBookItemAsync(id);
        return Ok(updatedBookItem);
    }
}