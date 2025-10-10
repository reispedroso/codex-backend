using codex_backend.Application.Dtos;
using codex_backend.Models;
using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Application.Services.Interfaces;
using codex_backend.Application.Authorization.Common.Exceptions;

namespace codex_backend.Application.Services.Implementations;

public class AuthorService(IAuthorRepository AuthorRepository) : IAuthorService
{
    private readonly IAuthorRepository _AuthorRepository = AuthorRepository;

    public async Task<AuthorReadDto> CreateAuthorAsync(AuthorCreateDto dto)
    {

        if (await _AuthorRepository.GetAuthorByNameAsync(dto.Name) is not null) throw new DuplicateException($"Author with name {dto.Name} already registered");

        var newAuthor = new Author
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Biography = dto.Biography,
            Nationality = dto.Nationality,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            DeletedAt = null
        };

        await _AuthorRepository.CreateAuthorAsync(newAuthor);
        return MapToDto(newAuthor);
    }
    public async Task<IEnumerable<AuthorReadDto>> GetAllAuthorsAsync()
    {
        var allAuthors = await _AuthorRepository.GetAllAuthorsAsync() ?? throw new Exception("No Authorstores available");
        return allAuthors.Select(MapToDto);
    }

    public async Task<AuthorReadDto> GetAuthorByIdAsync(Guid id)
    {
        var AuthorById = await _AuthorRepository.GetAuthorByIdAsync(id)
        ?? throw new NotFoundException($"Author with {id} not found");
        return MapToDto(AuthorById);
    }

    public async Task<AuthorReadDto> GetAuthorByNameAsync(string name)
    {
        var AuthorByName = await _AuthorRepository.GetAuthorByNameAsync(name)
        ?? throw new NotFoundException($"Author: {name} not founded");
        return MapToDto(AuthorByName);
    }

    public async Task<AuthorReadDto> UpdateAuthorAsync(Guid id, AuthorUpdateDto dto)
    {
        var updateAuthor = await _AuthorRepository.GetAuthorByIdAsync(id);

        updateAuthor!.Name = dto.Name;
        updateAuthor!.Biography = dto.Biography;
        updateAuthor!.Nationality = dto.Nationality;
        updateAuthor.UpdatedAt = DateTime.UtcNow;

        await _AuthorRepository.UpdateAuthorAsync(updateAuthor);
        return MapToDto(updateAuthor);
    }
    public async Task DeleteAuthorAsync(Guid id)
    {
        var deleteAuthor = await _AuthorRepository.GetAuthorByIdAsync(id);

        deleteAuthor!.DeletedAt = DateTime.UtcNow;
        await _AuthorRepository.UpdateAuthorAsync(deleteAuthor);
    }

    private static AuthorReadDto MapToDto(Author a) => new()
    {
        Id = a.Id,
        Name = a.Name!,
        Nationality = a.Nationality!,
        Biography = a.Biography!,
        CreatedAt = a.CreatedAt,
        UpdatedAt = a.UpdatedAt
    };
}
