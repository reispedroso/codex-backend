using codex_backend.Models;

namespace codex_backend.Application.Repositories.Interfaces;

public interface IReservationRepository
{
    Task<Reservation> CreateReservationAsync(Reservation reservation);
    Task<Reservation?> GetReservationByIdAsync(Guid reservationId);
    Task<Reservation?> GetReservationWithDetailsAsync(Guid reservationId);
    Task<IEnumerable<Reservation>> GetAllReservationsAsync();
    Task<IEnumerable<Reservation>> GetAllUserReservationsAsync(Guid userId);
    Task<bool> UpdateReservationAsync(Reservation reservation);

}
