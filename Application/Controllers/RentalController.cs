using codex_backend.Application.Dtos;
using codex_backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace codex_backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentalController(RentalService service) : ControllerBase
{
    private readonly RentalService _service = service;

    [HttpPost("create-rental/{reservationId:guid}")]
    public async Task<IActionResult> Post(Guid reservationId)
    {
        try
        {
            var newRental = await _service.CreateRentalAsync(reservationId);
            return Ok(newRental);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}