using codex_backend.Application.Dtos;

namespace codex_backend.Application.Services.Interfaces;

public interface IReservationService
{
    Task<ReservationReadDto> CreateReservationAsync(ReservationCreateDto reservationDto);
}
