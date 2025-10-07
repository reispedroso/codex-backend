using codex_backend.Application.Dtos;
using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Enums;
using codex_backend.Models;

namespace codex_backend.Application.Factories;

public class RentalFactory : IRentalFactory
{

    public Rental CreateRentalFromReservation(Reservation reservation)
    {

        return new Rental
        {
            Id = Guid.NewGuid(),
            ReservationId = reservation.Id,
            UserId = reservation.UserId,
            Status = RentalStatus.Active,
            RentedAt = DateTime.UtcNow,
            DueDate = reservation.DueDate,
            CurrencyCode = reservation.CurrencySnapshot,
            PriceAmount = reservation.PriceAmountSnapshot,
            LateDays = 0,
            LateFeeAmount = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}