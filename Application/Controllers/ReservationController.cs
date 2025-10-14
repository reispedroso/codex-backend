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
    public async Task<IActionResult> GetMyReservations()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var reservations = await _service.GetMyReservationsAsync(userId);
        return Ok(reservations);
    }

    [HttpGet("by-id/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
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

        await _service.CancelReservationAsync(id);

        return NoContent();
    }

}

