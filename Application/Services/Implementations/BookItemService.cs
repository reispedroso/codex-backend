using codex_backend.Application.Dtos;
using codex_backend.Models;
using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Helpers;
using codex_backend.Application.Validators;
using codex_backend.Application.Services.Interfaces;
using codex_backend.Application.Common.Exceptions;

namespace codex_backend.Application.Services.Implementations;

public class BookItemService(IBookItemRepository bookItemRepository) : IBookItemService
{
    private readonly IBookItemRepository _bookItemRepository = bookItemRepository;

    public async Task<BookItemReadDto> CreateBookItemAsync(BookItemCreateDto dto)
    {
        InvalidFieldsHelper.ThrowIfInvalid(BookItemValidator.ValidateBookItem(dto));

        var newBookItem = new BookItem
        {
            Id = Guid.NewGuid(),
            BookId = dto.BookId,
            BookstoreId = dto.BookstoreId,
            Quantity = dto.Quantity,
            Condition = dto.Condition,
            CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-3), DateTimeKind.Utc),
            UpdatedAt = null,
            DeletedAt = null
        };
        await _bookItemRepository.CreateBookItemAsync(newBookItem);
        return MapToDto(newBookItem);
    }

    public async Task<BookItemReadDto> GetBookItemByIdAsync(Guid id)
    {
        var bookItemById = await _bookItemRepository.GetBookItemByIdAsync(id)
        ?? throw new NotFoundException($"Book not found in this Bookstore! Book id: {id}");

        return MapToDto(bookItemById);
    }

    public async Task<BookItemReadDto> UpdateBookItemAsync(
   Guid bookItemId,
   BookItemUpdateDto dto)
    {
        InvalidFieldsHelper.ThrowIfInvalid(BookItemValidator.ValidateBookItemUpdate(dto));

        var updateBookItem = await _bookItemRepository.GetBookItemByIdAsync(bookItemId);

        updateBookItem!.Quantity = dto.Quantity;
        updateBookItem.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-3), DateTimeKind.Utc);

        await _bookItemRepository.UpdateBookItemAsync(updateBookItem);
        return MapToDto(updateBookItem);
    }

    private static BookItemReadDto MapToDto(BookItem b) => new()
    {
        Id = b.Id,
        BookId = b.BookId,
        BookstoreId = b.BookstoreId,
        Quantity = b.Quantity,
        Condition = b.Condition,
        CreatedAt = b.CreatedAt,
        UpdatedAt = b.UpdatedAt,
        DeletedAt = b.DeletedAt
    };

}
