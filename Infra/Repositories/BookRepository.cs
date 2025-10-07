using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Database;
using codex_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace codex_backend.Infra.Repositories;

public class BookRepository(AppDbContext context) : IBookRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Book> CreateBookAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _context.Books
           .ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(Guid bookId)
    {
        return await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
    }

    public async Task<Book?> GetBookByNameAsync(string bookName)
    {
        return await _context.Books
         .FirstOrDefaultAsync(b => b.Title == bookName);
    }

    public async Task<bool> UpdateBookAsync(Book book)
    {
        var updated = await _context.SaveChangesAsync();
        return updated > 0;
    }
}