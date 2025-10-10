using codex_backend.Application.Dtos;

namespace codex_backend.Application.Services.Interfaces;

public interface IRentalService
{
    Task<RentalReadDto> CreateRentalAsync(Guid reservationId);
    Task<RentalReadDto> GetRentalByIdAsync(Guid rentalId);
    Task<IEnumerable<RentalReadDto>> GetAllRentalsAsync();
    Task<IEnumerable<RentalReadDto>> GetAllUserRentalsAsync(Guid userId);
    Task<RentalReadDto> ReturnRentalAsync(Guid rentalId);
}