using codex_backend.Application.Dtos;

namespace codex_backend.Application.Services.Interfaces;

public interface IBookService
{
    Task<BookReadDto> CreateBookAsync(BookCreateDto dto);
    Task<IEnumerable<BookReadDto>> GetAllBooksAsync();
    Task<BookReadDto> GetBookByIdAsync(Guid id);
    Task<BookReadDto> GetBookByTitleAsync(string title);
    Task<IEnumerable<BookReadDto>> SearchBooksByTitleAsync(string name);
    Task<BookReadDto> UpdateBookAsync(Guid id, BookUpdateDto dto);
    Task DeleteBookAsync(Guid id);
}
