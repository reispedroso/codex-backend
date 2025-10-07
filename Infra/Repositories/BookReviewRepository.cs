using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Database;
using codex_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace codex_backend.Infra.Repositories;

public class BookReviewRepository(AppDbContext context) : IBookReviewRepository
{
    private readonly AppDbContext _context = context;

    public async Task<BookReview> CreateBookReviewAsync(BookReview bookReview)
    {
        await _context.BookReviews.AddAsync(bookReview);
        await _context.SaveChangesAsync();
        return bookReview;
    }

    public async Task<BookReview?> GetBookReviewByIdAsync(Guid bookReviewId)
    {
        return await _context.BookReviews
            .Include(br => br.Book)
            .FirstOrDefaultAsync(br => br.Id == bookReviewId);
    }

    public async Task<IEnumerable<BookReview>> GetBookReviewsByBookIdAsync(Guid bookId)
    {
        return await _context.BookReviews
            .Include(br => br.Book)
            .Where(br => br.Book.Id == bookId)
            .ToListAsync();
    }

    public async Task<IEnumerable<BookReview>> GetBookReviewsByBookNameAsync(string bookName)
    {
        return await _context.BookReviews
            .Include(br => br.Book)
            .Where(br => br.Book.Title == bookName)
            .ToListAsync();
    }

    public async Task<bool> UpdateBookReviewAsync(BookReview bookReview)
    {
        _context.BookReviews.Update(bookReview);
        var updated = await _context.SaveChangesAsync();
        return updated > 0;
    }
}
