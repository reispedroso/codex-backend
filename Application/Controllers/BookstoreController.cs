    using System.Security.Claims;
    using codex_backend.Application.Dtos;
    using codex_backend.Application.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    namespace codex_backend.Application.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class BookstoreController(BookstoreService service) : ControllerBase
    {
        private readonly BookstoreService _service = service;

        [HttpPost("create-bookstore")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] BookstoreCreateDto bookstore)
        {
            try
            {
                var loggedInUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var createdBookstore = await _service.CreateBookstoreAsync(bookstore, loggedInUserId);
                return Ok(createdBookstore.Name);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-all-bookstores")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<BookstoreReadDto>>> GetAll()
        {
            try
            {
                var bookstores = await _service.GetAllBookstoresAsync();
                return Ok(bookstores);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("my-bookstores")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<BookstoreReadDto>>> GetMyBookstores()
        {
            try
            {
                var loggedInUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var bookstores = await _service.GetBookstoresByAdminIdAsync(loggedInUserId);
                return Ok(bookstores);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BookstoreReadDto>> GetById(Guid id)
        {
            try
            {
                var bookstore = await _service.GetBookstoreByIdAsync(id);
                return Ok(bookstore);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<BookstoreReadDto>> GetByName(string name)
        {
            try
            {
                var bookstore = await _service.GetBookstoreByNameAsync(name);
                return Ok(bookstore);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("by-owner/{adminId:guid}")]
        public async Task<ActionResult<IEnumerable<BookstoreReadDto>>> GetByAdminId(Guid adminId)
        {
            try
            {
                var bookstores = await _service.GetBookstoresByAdminIdAsync(adminId);
                return Ok(bookstores);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("update-bookstore/{id:guid}")]
        public async Task<ActionResult<BookstoreReadDto>> Put(Guid id, [FromBody] BookstoreUpdateDto bookstore)
        {
            try
            {
                var updatedBookstore = await _service.UpdateBookstoreAsync(id, bookstore);
                return Ok(updatedBookstore);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("delete-bookstore/{id:guid}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteBookstoreAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }