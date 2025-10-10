using codex_backend.Application.Dtos;
using codex_backend.Models;

namespace codex_backend.Application.Services.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationReadDto> CreateReservationAsync(ReservationCreateDto dto, Guid userId);
        Task<ReservationReadDto> PrepareReservationForPickup(Guid reservationId);
        Task<IEnumerable<ReservationReadDto>> GetMyReservationsAsync(Guid userId);
        Task<ReservationReadDto> GetReservationByIdAsync(Guid id);
        Task CancelReservationAsync(Guid id);
        Task<Reservation?> GetReservationModelByIdAsync(Guid id);
    }
}