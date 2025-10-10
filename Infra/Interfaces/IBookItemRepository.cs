using codex_backend.Models;

namespace codex_backend.Application.Repositories.Interfaces;

public interface IBookItemRepository
{
    Task<BookItem> CreateBookItemAsync(BookItem bookItem);
    Task<BookItem?> GetBookItemByNameAsync(Guid bookstoreId, string bookItemName); // se dentro de uma bookstore eu quiser ver os livros dela por nome, sim isso faz sentido.
    Task<BookItem?> GetBookItemByIdAsync(Guid bookItemId); 
    Task<IEnumerable<BookItem>> GetAllBookItemsAsync();
    Task<bool> UpdateBookItemAsync(BookItem bookItem);
    Task<BookItem?> GetBookItemWithBookstoreAsync(Guid id);
}