using codex_backend.Models;

namespace codex_backend.Application.Repositories.Interfaces;

public interface IBookRepository
{
    Task<Book> CreateBookAsync(Book book);
    Task<Book?> GetBookByIdAsync(Guid bookId);
    Task<Book?> GetBookByTitleAsync(string title);
    Task<IEnumerable<Book?>> SearchBooksByTitleAsync(string searchTerm);
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<bool> UpdateBookAsync(Book book);

}