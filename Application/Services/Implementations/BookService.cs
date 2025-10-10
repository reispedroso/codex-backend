using codex_backend.Application.Dtos;
using codex_backend.Models;
using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Helpers;
using codex_backend.Application.Validators;
using codex_backend.Application.Services.Interfaces;
using codex_backend.Application.Authorization.Common.Exceptions;

namespace codex_backend.Application.Services.Implementations;

public class BookService(IBookRepository bookRepository) : IBookService
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<BookReadDto> CreateBookAsync(BookCreateDto dto)
    {
        InvalidFieldsHelper.ThrowIfInvalid(BookValidator.ValidateBook(dto));

        if (await _bookRepository.GetBookByTitleAsync(dto.Title) is not null) throw new DuplicateException($"Book with name {dto.Title} already registered");

        var newBook = new Book
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Synposis = dto.Synposis,
            PublicationDate = dto.PublicationDate.ToUniversalTime(),
            Language = dto.Language,
            Publisher = dto.Publisher,
            PageCount = dto.PageCount,
            CoverUrl = dto.CoverUrl,
            AuthorId = dto.AuthorId,
            CategoryId = dto.CategoryId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            DeletedAt = null
        };

        await _bookRepository.CreateBookAsync(newBook);
        return MapToDto(newBook);
    }
    public async Task<IEnumerable<BookReadDto>> GetAllBooksAsync()
    {
        var allBooks = await _bookRepository.GetAllBooksAsync() ?? throw new NotFoundException("No bookstores available");
        return allBooks.Select(MapToDto);
    }

    public async Task<BookReadDto> GetBookByIdAsync(Guid id)
    {
        var bookById = await _bookRepository.GetBookByIdAsync(id)
        ?? throw new NotFoundException($"Book with {id} not found");
        return MapToDto(bookById);
    }

    public async Task<BookReadDto> GetBookByTitleAsync(string title)
    {
        var specificBook = await _bookRepository.GetBookByTitleAsync(title)
        ?? throw new NotFoundException($"Book with {title} not found");

        return MapToDto(specificBook);
        
    }

    public async Task<IEnumerable<BookReadDto>> SearchBooksByTitleAsync(string name)
    {
        var booksByTitle = await _bookRepository.SearchBooksByTitleAsync(name)
        ?? throw new NotFoundException($"Book: {name} not founded");
        return booksByTitle.Select(MapToDto!);
    }

    public async Task<BookReadDto> UpdateBookAsync(Guid id, BookUpdateDto dto)
    {
        InvalidFieldsHelper.ThrowIfInvalid(BookValidator.ValidateBookUpdate(dto));
        var updateBook = await _bookRepository.GetBookByIdAsync(id);

        updateBook!.Title = dto.Title;
        updateBook.Synposis = dto.Synposis;
        updateBook.PublicationDate = dto.PublicationDate.ToUniversalTime();
        updateBook.Language = dto.Language;
        updateBook.Publisher = dto.Publisher;
        updateBook.PageCount = dto.PageCount;
        updateBook.CoverUrl = dto.CoverUrl;
        updateBook.AuthorId = dto.AuthorId;
        updateBook.CategoryId = dto.CategoryId;
        updateBook.UpdatedAt = DateTime.UtcNow;

        await _bookRepository.UpdateBookAsync(updateBook);
        return MapToDto(updateBook);
    }
    public async Task DeleteBookAsync(Guid id)
    {
        var deleteBook = await _bookRepository.GetBookByIdAsync(id);

        deleteBook!.DeletedAt = DateTime.UtcNow;
        await _bookRepository.UpdateBookAsync(deleteBook);
    }

    private static BookReadDto MapToDto(Book b) => new()
    {
        Id = b.Id,
        Title = b.Title!,
        Synposis = b.Synposis!,
        AuthorId = b.AuthorId,
        PublicationDate = b.PublicationDate,
        Language = b.Language!,
        Publisher = b.Publisher!,
        PageCount = b.PageCount,
        CoverUrl = b.CoverUrl!,
        CategoryId = b.CategoryId,
        CreatedAt = b.CreatedAt,
        UpdatedAt = b.UpdatedAt,
    };
}
