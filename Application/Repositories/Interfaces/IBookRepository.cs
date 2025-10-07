using codex_backend.Models;

namespace codex_backend.Application.Repositories.Interfaces;

public interface IBookRepository
{
    Task<Book> CreateBookAsync(Book book);
    Task<Book?> GetBookByNameAsync(string bookName);
    Task<Book?> GetBookByIdAsync(Guid bookId);
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<bool> UpdateBookAsync(Book book);

}