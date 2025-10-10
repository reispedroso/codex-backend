using codex_backend.Application.Authorization.Common.Exceptions;
using codex_backend.Application.Dtos;
using codex_backend.Application.Factories;
using codex_backend.Application.Handlers;
using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Application.Services.Interfaces;
using codex_backend.Enums;
using codex_backend.Models;

namespace codex_backend.Application.Services.Implementations
{
    public class ReservationService(
        IReservationRepository reservationRepository,
        IReservationFactory reservationFactory,
        InventoryHandler inventoryHandler) : IReservationService
    {
        private readonly IReservationRepository _reservationRepository = reservationRepository;
        private readonly IReservationFactory _reservationFactory = reservationFactory;
        private readonly InventoryHandler _inventoryHandler = inventoryHandler;

        public async Task<ReservationReadDto> CreateReservationAsync(ReservationCreateDto dto, Guid userId)
        {
            var newReservation = await _reservationFactory.CreateReservationAsync(dto, userId);

            await _inventoryHandler.ReserveBookItem(dto.BookItemId);

            await _reservationRepository.CreateReservationAsync(newReservation);

            return MapToDto(newReservation);
        }

        public async Task<ReservationReadDto> PrepareReservationForPickup(Guid reservationId)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId)
            ?? throw new NotFoundException("reservation not found");
            reservation.Status = ReservationStatus.Ready;
            reservation.UpdatedAt = DateTime.UtcNow;

            await _reservationRepository.UpdateReservationAsync(reservation);

            return MapToDto(reservation);
        }



        public async Task<IEnumerable<ReservationReadDto>> GetMyReservationsAsync(Guid userId)
        {
            var reservations = await _reservationRepository.GetAllUserReservationsAsync(userId);
            return reservations.Select(MapToDto);
        }

        public async Task<ReservationReadDto> GetReservationByIdAsync(Guid id)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(id)
                ?? throw new NotFoundException($"Reserva com ID {id} não encontrada.");

            return MapToDto(reservation);
        }

        public async Task CancelReservationAsync(Guid id)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(id)
                ?? throw new NotFoundException($"Reserva com ID {id} não encontrada para cancelamento.");


            if (reservation.Status is ReservationStatus.Cancelled or ReservationStatus.Rented)
            {
                throw new InvalidOperationException($"Não é possível cancelar uma reserva com status '{reservation.Status}'.");
            }

            reservation.Status = ReservationStatus.Cancelled;
            reservation.UpdatedAt = DateTime.UtcNow;


            await _reservationRepository.UpdateReservationAsync(reservation);


            await _inventoryHandler.ReturnBookItemToStock(reservation.BookItemId);
        }

        public async Task<Reservation?> GetReservationModelByIdAsync(Guid id)
        {
            return await _reservationRepository.GetReservationWithDetailsAsync(id);
        }

        private static ReservationReadDto MapToDto(Reservation res) => new()
        {
            Id = res.Id,
            UserId = res.UserId,
            BookItemId = res.BookItemId,
            PoliciesId = res.PoliciesId,
            Status = res.Status,
            PickupDate = res.PickupDate,
            DueDate = res.DueDate,
            PriceAmountSnapshot = res.PriceAmountSnapshot,
            CurrencySnapshot = res.CurrencySnapshot ?? string.Empty,
            CreatedAt = res.CreatedAt,
            UpdatedAt = res.UpdatedAt
        };
    }
}