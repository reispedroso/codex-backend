using codex_backend.Application.Dtos;

namespace codex_backend.Application.Services.Interfaces;

public interface IBookstoreService
{
    Task<BookstoreReadDto> CreateBookstoreAsync(BookstoreCreateDto dto, Guid ownerUserId);
    Task<IEnumerable<BookstoreReadDto>> GetAllBookstoresAsync();
    Task<BookstoreReadDto> GetBookstoreByIdAsync(Guid id);
    Task<BookstoreReadDto> GetSingleBookstoreByAdminId(Guid adminId);
    Task<BookstoreReadDto> GetBookstoreByNameAsync(string name);
    Task<IEnumerable<BookstoreReadDto>> GetBookstoresByAdminIdAsync(Guid adminId);
    Task<BookstoreReadDto> UpdateBookstoreAsync(Guid id, BookstoreUpdateDto dto);
    Task DeleteBookstoreAsync(Guid id);
}
