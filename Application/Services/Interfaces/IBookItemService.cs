using codex_backend.Application.Dtos;

namespace codex_backend.Application.Services.Interfaces;

public interface IBookItemService
{
    Task<BookItemReadDto> CreateBookItemAsync(BookItemCreateDto dto);
    Task<BookItemReadDto> GetBookItemByIdAsync(Guid id);
    Task<BookItemReadDto> UpdateBookItemAsync(Guid bookItemId, BookItemUpdateDto dto);
}
