using codex_backend.Application.Dtos;

namespace codex_backend.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto dto);
        Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync();
        Task<CategoryReadDto> GetCategoryByIdAsync(Guid id);
        Task<CategoryReadDto> UpdateCategoryAsync(Guid id, CategoryUpdateDto dto);
        Task DeleteCategoryAsync(Guid id);
    }
}