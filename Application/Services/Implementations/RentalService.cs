using codex_backend.Application.Authorization.Common.Exceptions;
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
        ?? throw new NotFoundException("Reservation not found");

        Console.WriteLine(reservation.Status);

        if (reservation.Status != ReservationStatus.Ready)
            throw new NotFoundException("Reservation is not ready to be picked off yet");

        var newRental = _factory.CreateRentalFromReservation(reservation);

        await _rentalRepository.CreateRentalAsync(newRental);

        reservation.Status = ReservationStatus.Rented;
        reservation.UpdatedAt = DateTime.UtcNow;

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

    public async Task<RentalReadDto> GetRentalByIdAsync(Guid rentalId)
    {
        return await _rentalRepository.GetRentalByIdAsync(rentalId) is Rental rental
            ? MapToDto(rental)
            : throw new NotFoundException("Rental not found");
    }

    public async Task<IEnumerable<RentalReadDto>> GetAllRentalsAsync()
    {
        var rentals = await _rentalRepository.GetAllRentalsAsync();
        return rentals.Select(MapToDto);
    }

    public async Task<IEnumerable<RentalReadDto>> GetAllUserRentalsAsync(Guid userId)
    {
    var rentals = await _rentalRepository.GetAllUserRentalsAsync(userId);
    return rentals.Select(MapToDto);
    }

    public async Task<RentalReadDto> ReturnRentalAsync(Guid rentalId)
    {
        var rental = await _rentalRepository.GetRentalByIdAsync(rentalId)
            ?? throw new NotFoundException("Rental not found");

        if (rental.Status != RentalStatus.Active)
            throw new InvalidOperationException("Rental is not active and cannot be returned.");

        rental.Status = RentalStatus.Returned;
        rental.UpdatedAt = DateTime.UtcNow;

        await _rentalRepository.UpdateRentalAsync(rental);

        return MapToDto(rental);
    }
}