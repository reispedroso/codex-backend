using codex_backend.Models;

namespace codex_backend.Application.Repositories.Interfaces;

public interface IAuthorRepository
{
    Task<Author> CreateAuthorAsync(Author author);
    Task<Author?> GetAuthorByNameAsync(string authorName);
    Task<Author?> GetAuthorByIdAsync(Guid authorId);
    Task<IEnumerable<Author>> GetAllAuthorsAsync();
    Task<bool> UpdateAuthorAsync(Author author);

}
