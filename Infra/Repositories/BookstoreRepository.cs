using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Database;
using codex_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace codex_backend.Infra.Repositories;

public class BookstoreRepository(AppDbContext context) : IBookstoreRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Bookstore> CreateBookstoreAsync(Bookstore bookstore)
    {
        await _context.Bookstores.AddAsync(bookstore);
        await _context.SaveChangesAsync();
        return bookstore;
    }

    public async Task<IEnumerable<Bookstore>> GetAllBookstoresAsync()
    {
        return await _context.Bookstores.ToListAsync();
    }

    public async Task<Bookstore?> GetBookstoreByIdAsync(Guid bookstoreId)
    {
        return await _context.Bookstores.FirstOrDefaultAsync(bs => bs.Id == bookstoreId);
    }

    public async Task<Bookstore?> GetBookstoreByNameAsync(string bookstoreName)
    {
        return await _context.Bookstores.FirstOrDefaultAsync(bs => bs.Name == bookstoreName);

    }
    public async Task<IEnumerable<Bookstore>> GetBookstoresByAdminIdAsync(Guid adminId)
    {
        return await _context.Bookstores
        .Include(bs => bs.User)
    .Where(bs => bs.User!.Id == adminId)
        .ToListAsync();
    }

    public async Task<Bookstore> GetSingleBookstoreByAdminIdAsync(Guid adminId)
    {
        return await _context.Bookstores.FirstOrDefaultAsync(bs => bs.OwnerUserId == adminId);
    }

    public async Task<bool> UpdateBookstoreAsync(Bookstore bookstore)
    {
        _context.Bookstores.Update(bookstore);
        var updated = await _context.SaveChangesAsync();
        return updated > 0;
    }

}