using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Database;
using codex_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace codex_backend.Infra.Repositories;

public class AuthorRepository(AppDbContext context) : IAuthorRepository
{
    private readonly AppDbContext _context = context;
    public async Task<Author> CreateAuthorAsync(Author author)
    {
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
        return author;
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        return await _context.Authors
           .ToListAsync();
    }

    public async Task<Author?> GetAuthorByIdAsync(Guid authorId)
    {
        return await _context.Authors.FirstOrDefaultAsync(a => a.Id == authorId);
    }

    public async Task<Author?> GetAuthorByNameAsync(string authorName)
    {
        return await _context.Authors
                 .FirstOrDefaultAsync(a => a.Name == authorName);
    }

    public async Task<bool> UpdateAuthorAsync(Author author)
    {
        _context.Authors.Update(author);
        var updated = await _context.SaveChangesAsync();
        return updated > 0;

    }
}