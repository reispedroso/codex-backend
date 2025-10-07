using codex_backend.Models;

namespace codex_backend.Application.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<Category> CreateCategoryAsync(Category category);
    Task<Category?> GetCategoryByNameAsync(string categoryName);
    Task<Category?> GetCategoryByIdAsync(Guid categoryId);
    Task<IEnumerable<Category>> GetAllCategorysAsync();
    Task<bool> UpdateCategoryAsync(Category category);
}
