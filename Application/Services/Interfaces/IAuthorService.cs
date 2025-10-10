using codex_backend.Application.Dtos;

namespace codex_backend.Application.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorReadDto> CreateAuthorAsync(AuthorCreateDto dto);
        Task<IEnumerable<AuthorReadDto>> GetAllAuthorsAsync();
        Task<AuthorReadDto> GetAuthorByIdAsync(Guid id);
        Task<AuthorReadDto> UpdateAuthorAsync(Guid id, AuthorUpdateDto dto);
        Task DeleteAuthorAsync(Guid id);
    }
}