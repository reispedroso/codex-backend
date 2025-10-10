using System.Security.Claims;
using codex_backend.Application.Authorization.Wrappers;
using codex_backend.Application.Dtos;
using codex_backend.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace codex_backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReservationController(
    IReservationService service,
    IAuthorizationService authorizationService
    ) : ControllerBase
{
    private readonly IReservationService _service = service;
    private readonly IAuthorizationService _authorizationService = authorizationService;

    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] ReservationCreateDto reservationDto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var createdReservation = await _service.CreateReservationAsync(reservationDto, userId);
        return Ok(createdReservation);

    }

    [HttpPost("set-ready/{reservationId}")]
    public async Task<IActionResult> SetReady(Guid reservationId)
    {
        var reservation = await _service.PrepareReservationForPickup(reservationId);
        var response = new ApiSingleResponse<ReservationReadDto>(true, "Reservation in now available for pickup", reservation);
        return Ok(response);
    }

    [HttpGet("my-reservations")]
    public async Task<ActionResult<IEnumerable<ReservationReadDto>>> GetMyReservations()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var reservations = await _service.GetMyReservationsAsync(userId);
        return Ok(reservations);
    }

    // Renomeei a rota para evitar conflito de nome com o CreatedAtAction
    [HttpGet("{id:guid}", Name = "GetReservationById")]
    public async Task<ActionResult<ReservationReadDto>> GetById(Guid id)
    {
        // Note que para visualizar, também aplicamos a mesma segurança do cancelamento
        var reservationModel = await _service.GetReservationModelByIdAsync(id);
        if (reservationModel is null)
        {
            return NotFound();
        }

        var authorizationResult = await _authorizationService
            .AuthorizeAsync(User, reservationModel, "CanManageReservationPolicy");

        if (!authorizationResult.Succeeded)
        {
            return Forbid();
        }

        var reservationDto = await _service.GetReservationByIdAsync(id);
        return Ok(reservationDto);
    }

    [HttpDelete("cancel/{id}")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        // 1. BUSCAR: Pega o modelo completo da reserva.
        var reservationModel = await _service.GetReservationModelByIdAsync(id);
        if (reservationModel is null)
        {
            // Se a reserva não existe, retorna 404 Not Found.
            return NotFound();
        }

        // 2. AUTORIZAR: Pergunta ao sistema de autorização se o usuário atual pode
        // gerenciar este modelo de reserva específico.
        var authorizationResult = await _authorizationService
            .AuthorizeAsync(User, reservationModel, "CanManageReservationPolicy");

        if (!authorizationResult.Succeeded)
        {
            // Se não tiver sucesso, retorna 403 Forbidden.
            return Forbid();
        }

        // 3. AGIR: Se a autorização passou, executa a ação de negócio.
        await _service.CancelReservationAsync(id);

        // Retorna 204 No Content, indicando sucesso na exclusão/cancelamento.
        return NoContent();
    }

}

