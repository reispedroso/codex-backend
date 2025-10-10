using codex_backend.Application.Authorization.Common.Exceptions;
using codex_backend.Application.Dtos;
using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Application.Services.Interfaces;
using codex_backend.Models;

namespace codex_backend.Application.Services.Implementations
{
    public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto dto)
        {
            if (await _categoryRepository.GetCategoryByNameAsync(dto.Name) is not null)
            {
                throw new DuplicateException($"Category with name '{dto.Name}' already exists.");
            }

            var newCategory = new Category
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                CreatedAt = DateTime.UtcNow,
            };

            await _categoryRepository.CreateCategoryAsync(newCategory);
            return MapToDto(newCategory);
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return categories.Select(MapToDto);
        }

        public async Task<CategoryReadDto> GetCategoryByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id) 
                ?? throw new NotFoundException($"Category with ID {id} not found.");
            
            return MapToDto(category);
        }

        public async Task<CategoryReadDto> UpdateCategoryAsync(Guid id, CategoryUpdateDto dto)
        {
            var categoryToUpdate = await _categoryRepository.GetCategoryByIdAsync(id)
                ?? throw new NotFoundException($"Category with ID {id} not found.");

            categoryToUpdate.Name = dto.Name;
            categoryToUpdate.UpdatedAt = DateTime.UtcNow;

            await _categoryRepository.UpdateCategoryAsync(categoryToUpdate);
            return MapToDto(categoryToUpdate);
        }
        
        public async Task DeleteCategoryAsync(Guid id)
        {
            var categoryToDelete = await _categoryRepository.GetCategoryByIdAsync(id)
                ?? throw new NotFoundException($"Category with ID {id} not found.");

            categoryToDelete.DeletedAt = DateTime.UtcNow;
            await _categoryRepository.UpdateCategoryAsync(categoryToDelete);
        }

        private static CategoryReadDto MapToDto(Category c) => new()
        {
            Id = c.Id,
            Name = c.Name!,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt
        };
    }
}