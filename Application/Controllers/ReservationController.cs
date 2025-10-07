using codex_backend.Application.Dtos;
using codex_backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace codex_backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController(ReservationService service) : ControllerBase
{
    private readonly ReservationService _service = service;

    [HttpPost("create-reservation")]
    public async Task<IActionResult> Post([FromBody] ReservationCreateDto reservationDto)
    {
        try
        {
            var createdReservation = await _service.CreateReservationAsync(reservationDto);
            return CreatedAtAction(nameof(GetById), new { id = createdReservation.Id }, createdReservation);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Exemplo de endpoint extra para o CreatedAtAction funcionar
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        // TODO: puxar do service/repository
        return Ok($"Retornar reserva {id} aqui futuramente");
    }
}
