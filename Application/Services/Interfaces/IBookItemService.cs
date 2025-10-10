using codex_backend.Application.Dtos;
using codex_backend.Models;

namespace codex_backend.Application.Services.Interfaces;

public interface IBookItemService
{
    Task<BookItemReadDto> CreateBookItemAsync(BookItemCreateDto dto);
    Task<BookItemReadDto> GetBookItemByIdAsync(Guid id);
    Task<BookItem?> GetBookItemWithBookstoreAsync(Guid id);
    Task<BookItemReadDto> UpdateBookItemAsync(Guid bookItemId, BookItemUpdateDto dto);
      Task<BookItemReadDto> DeleteBookItemAsync(Guid bookItemId); 
}
