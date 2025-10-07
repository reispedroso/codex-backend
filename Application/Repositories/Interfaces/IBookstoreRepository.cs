using codex_backend.Models;

namespace codex_backend.Application.Repositories.Interfaces;

public interface IBookstoreRepository
{
    Task<Bookstore> CreateBookstoreAsync(Bookstore bookstore);
    Task<Bookstore?> GetBookstoreByNameAsync(string bookstoreName);
    Task<Bookstore?> GetBookstoreByIdAsync(Guid bookstoreId);
    Task<IEnumerable<Bookstore>> GetBookstoresByAdminIdAsync(Guid adminId);
    Task<Bookstore> GetSingleBookstoreByAdminIdAsync(Guid adminId);

    Task<IEnumerable<Bookstore>> GetAllBookstoresAsync();
    Task<bool> UpdateBookstoreAsync(Bookstore bookstore);
}
