using codex_backend.Application.Common.Exceptions;
using codex_backend.Application.Dtos;
using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Application.Services.Interfaces;
using codex_backend.Models;

namespace codex_backend.Application.Services.Implementations;

public class BookstoreService(IBookstoreRepository bookstoreRepository) : IBookstoreService
{
    private readonly IBookstoreRepository _bookstoreRepository = bookstoreRepository;

    public async Task<Bookstore?> GetBookstoreModelByIdAsync(Guid id)
    {

        return await _bookstoreRepository.GetBookstoreByIdAsync(id);
    }

    public async Task<BookstoreReadDto> CreateBookstoreAsync(BookstoreCreateDto dto, Guid ownerUserId)
    {
        if (await _bookstoreRepository.GetBookstoreByNameAsync(dto.Name) is not null)
        {
            throw new DuplicateException($"A livraria com o nome '{dto.Name}' já existe.");
        }

        var newBookstore = new Bookstore
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            OwnerUserId = ownerUserId,
            Street = dto.Street,
            City = dto.City,
            State = dto.State,
            ZipCode = dto.ZipCode,
            StoreLogoUrl = dto.StoreLogoUrl,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            DeletedAt = null
        };

        await _bookstoreRepository.CreateBookstoreAsync(newBookstore);
        return MapToDto(newBookstore);
    }

    public async Task<IEnumerable<BookstoreReadDto>> GetAllBookstoresAsync()
    {
        var allBookstores = await _bookstoreRepository.GetAllBookstoresAsync();
        if (allBookstores == null || !allBookstores.Any())
        {
            return Enumerable.Empty<BookstoreReadDto>();
        }
        return allBookstores.Select(MapToDto);
    }

    public async Task<BookstoreReadDto> GetBookstoreByIdAsync(Guid id)
    {
        var bookstore = await _bookstoreRepository.GetBookstoreByIdAsync(id)
            ?? throw new NotFoundException($"Livraria com ID: {id} não encontrada.");

        return MapToDto(bookstore);
    }

    public async Task<BookstoreReadDto> GetBookstoreByNameAsync(string name)
    {
        var bookstore = await _bookstoreRepository.GetBookstoreByNameAsync(name)
            ?? throw new NotFoundException($"Livraria com nome: {name} não encontrada.");
        return MapToDto(bookstore);
    }

    public async Task<IEnumerable<BookstoreReadDto>> GetBookstoresByOwnerIdAsync(Guid ownerId)
    {
        var bookstores = await _bookstoreRepository.GetBookstoresByAdminIdAsync(ownerId);
        if (bookstores == null || !bookstores.Any())
        {
            return Enumerable.Empty<BookstoreReadDto>();
        }
        return bookstores.Select(MapToDto);
    }

    public async Task<BookstoreReadDto> UpdateBookstoreAsync(Guid id, BookstoreUpdateDto dto)
    {
        var bookstoreToUpdate = await _bookstoreRepository.GetBookstoreByIdAsync(id)
            ?? throw new NotFoundException($"Livraria com ID: {id} não encontrada para atualização.");

        bookstoreToUpdate.Name = dto.Name;
        bookstoreToUpdate.Street = dto.Street;
        bookstoreToUpdate.City = dto.City;
        bookstoreToUpdate.State = dto.State;
        bookstoreToUpdate.ZipCode = dto.ZipCode;
        bookstoreToUpdate.StoreLogoUrl = dto.StoreLogoUrl;
        bookstoreToUpdate.UpdatedAt = DateTime.UtcNow;

        await _bookstoreRepository.UpdateBookstoreAsync(bookstoreToUpdate);
        return MapToDto(bookstoreToUpdate);
    }

    public async Task DeleteBookstoreAsync(Guid id)
    {
        var bookstoreToDelete = await _bookstoreRepository.GetBookstoreByIdAsync(id)
            ?? throw new NotFoundException($"Livraria com ID: {id} não encontrada para exclusão.");

        bookstoreToDelete.DeletedAt = DateTime.UtcNow;
        await _bookstoreRepository.UpdateBookstoreAsync(bookstoreToDelete);
    }

    private static BookstoreReadDto MapToDto(Bookstore b) => new()
    {
        Id = b.Id,
        Name = b.Name!,
        OwnerUserId = b.OwnerUserId,
        Street = b.Street!,
        City = b.City!,
        State = b.State!,
        ZipCode = b.ZipCode!,
        Country = b.Country!,
        StoreLogoUrl = b.StoreLogoUrl!,
        CreatedAt = b.CreatedAt,
        UpdatedAt = b.UpdatedAt,
        DeletedAt = b.DeletedAt
    };
}
