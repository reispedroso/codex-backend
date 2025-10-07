using System.Security.Claims;
using codex_backend.Application.Dtos;
using codex_backend.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace codex_backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReservationController(IReservationService service) : ControllerBase
{
    private readonly IReservationService _service = service;

    [HttpPost("create-reservation")]
    public async Task<IActionResult> Post([FromBody] ReservationCreateDto reservationDto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        
        var createdReservation = await _service.CreateReservationAsync(reservationDto, userId);
        return Ok(createdReservation);
     
    }

}
