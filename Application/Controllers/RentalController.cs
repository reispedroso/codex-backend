using codex_backend.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace codex_backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RentalController(IRentalService service) : ControllerBase
{
    private readonly IRentalService _service = service;

    [HttpPost("create/{reservationId}")]
    public async Task<IActionResult> Post(Guid reservationId)
    {
        var newRental = await _service.CreateRentalAsync(reservationId);
        return Ok(newRental);

    }
}