using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Database;
using codex_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace codex_backend.Infra.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid categoryId)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

    }

    public async Task<Category?> GetCategoryByNameAsync(string categoryName)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
    }

    public async Task<bool> UpdateCategoryAsync(Category category)
    {
        _context.Categories.Update(category);
        var updated = await _context.SaveChangesAsync();
        return updated > 0;
    }


}