using codex_backend.Models;

namespace codex_backend.Application.Repositories.Interfaces;

public interface IRentalRepository
{
    Task<Rental> CreateRentalAsync(Rental rental);
    Task<Rental?> GetRentalByIdAsync(Guid rentalId);
    Task<IEnumerable<Rental>> GetAllRentalsAsync();
    Task<IEnumerable<Rental>> GetAllUserRentalsAsync(Guid userId);
    Task<bool> UpdateRentalAsync(Rental rental);
}
