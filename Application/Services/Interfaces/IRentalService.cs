using codex_backend.Application.Dtos;

namespace codex_backend.Application.Services.Interfaces;

public interface IRentalService
{
    Task<RentalReadDto> CreateRentalAsync(Guid reservationId);
}
