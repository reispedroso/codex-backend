using codex_backend.Models;

namespace codex_backend.Application.Repositories.Interfaces;

public interface IBookReviewRepository
{
    Task<BookReview> CreateBookReviewAsync(BookReview bookReview);
    Task<BookReview?> GetBookReviewByIdAsync(Guid bookReviewId);

    // aqui um livro pode ter várias avaliações
    Task<IEnumerable<BookReview>> GetBookReviewsByBookIdAsync(Guid bookId);
    Task<IEnumerable<BookReview>> GetBookReviewsByBookNameAsync(string bookName);

    Task<bool> UpdateBookReviewAsync(BookReview bookReview);
}
