using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Database;
using codex_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace codex_backend.Infra.Repositories;

public class BookItemRepository(AppDbContext context) : IBookItemRepository
{
    private readonly AppDbContext _context = context;
    public async Task<BookItem> CreateBookItemAsync(BookItem bookItem)
    {
        await _context.BookItems.AddAsync(bookItem);
        await _context.SaveChangesAsync();
        return bookItem;
    }

    public async Task<IEnumerable<BookItem>> GetAllBookItemsAsync()
    {
        return await _context.BookItems
           .ToListAsync();
    }

    public async Task<BookItem?> GetBookItemByIdAsync(Guid bookItemId)
    {
        return await _context.BookItems.FirstOrDefaultAsync(bi => bi.Id == bookItemId);
    }

    public async Task<BookItem?> GetBookItemByNameAsync(Guid bookstoreId, string bookItemName)
    {
        return await _context.BookItems
                    .Include(bi => bi.Book)
                    .FirstOrDefaultAsync(bi => bi.BookstoreId == bookstoreId && bi.Book!.Title == bookItemName);
    }

    public async Task<bool> UpdateBookItemAsync(BookItem bookItem)
    {
        _context.BookItems.Update(bookItem);
        var updated = await _context.SaveChangesAsync();
        return updated > 0;
    }
}