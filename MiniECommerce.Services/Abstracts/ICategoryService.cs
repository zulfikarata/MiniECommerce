using MiniECommerce.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Services.Abstracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(CategoryDto categoryDto);
        Task UpdateCategoryAsync(CategoryDto categoryDto);
        Task DeleteCategoryAsync(int id);
    }
}
