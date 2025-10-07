using codex_backend.Application.Dtos;
using codex_backend.Application.Factories;
using codex_backend.Application.Handlers;
using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Application.Services.Interfaces;
using codex_backend.Models;

namespace codex_backend.Application.Services;

public class ReservationService(
    IReservationRepository reservationRepository,
    IReservationFactory reservationFactory,
    InventoryHandler inventory
    )
: IReservationService
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly IReservationFactory _reservationFactory = reservationFactory;
    private readonly InventoryHandler _inventory = inventory;

    public async Task<ReservationReadDto> CreateReservationAsync(
    ReservationCreateDto reservationDto
    )
    {
        var newReservation = await _reservationFactory.CreateReservationAsync(reservationDto);

        await _inventory.ReserveBookItem(reservationDto.BookItemId);

        await _reservationRepository.CreateReservationAsync(newReservation);

        return MapToDto(newReservation);
    }

    public static ReservationReadDto MapToDto(Reservation res) => new()
    {
        Id = res.Id,
        UserId = res.UserId,
        BookItemId = res.BookItemId,
        PoliciesId = res.PoliciesId,
        Status = res.Status,
        PickupDate = res.PickupDate,
        DueDate = res.DueDate,
        PriceAmountSnapshot = res.PriceAmountSnapshot,
        CurrencySnapshot = res.CurrencySnapshot,
        CreatedAt = res.CreatedAt,
        UpdatedAt = res.UpdatedAt
    };
    
}