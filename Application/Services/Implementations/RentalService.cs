using codex_backend.Application.Dtos;
using codex_backend.Application.Factories;
using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Application.Services.Interfaces;
using codex_backend.Enums;
using codex_backend.Models;

namespace codex_backend.Application.Services.Implementations;

public class RentalService(
    IRentalRepository rentalRepository,
    IRentalFactory factory,
    IReservationRepository reservationRepository
    )
: IRentalService
{
    private readonly IRentalFactory _factory = factory;
    private readonly IRentalRepository _rentalRepository = rentalRepository;
    private readonly IReservationRepository _reservationRepository = reservationRepository;

    public async Task<RentalReadDto> CreateRentalAsync(Guid reservationId)
    {
        var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId)
        ?? throw new Exception("Reservation not found");

        if (reservation.Status != ReservationStatus.Ready)
            throw new Exception("Reservation is not ready to be picked off yet");

        var newRental = _factory.CreateRentalFromReservation(reservation);

        await _rentalRepository.CreateRentalAsync(newRental);

        reservation.Status = ReservationStatus.Rented;
        reservation.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-3), DateTimeKind.Utc);

        await _reservationRepository.UpdateReservationAsync(reservation);

        return MapToDto(newRental);
    }

    public static RentalReadDto MapToDto(Rental rental) => new()
    {
        Id = rental.Id,
        ReservationId = rental.ReservationId,
        UserId = rental.UserId,
        Status = rental.Status,
        RentedAt = rental.RentedAt,
        DueDate = rental.DueDate,
        LateDays = rental.LateDays,
        LateFeeAmount = rental.LateFeeAmount,
        CurrencyCode = rental.CurrencyCode,
        PriceAmount = rental.PriceAmount,
        CreatedAt = rental.CreatedAt,
        UpdatedAt = rental.UpdatedAt
    };
}