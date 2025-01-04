using MiniECommerce.Core.DTOs;
using MiniECommerce.Core.Entities;
using MiniECommerce.DAL.Abstracts;
using MiniECommerce.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Services.Concrates
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            });
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                throw new Exception("Category not found");
            }

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task AddCategoryAsync(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };
            await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };
            await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}
