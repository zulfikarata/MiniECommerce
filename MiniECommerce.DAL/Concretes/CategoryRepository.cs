using MiniECommerce.Core.Entities;
using MiniECommerce.DAL.Abstracts;
using MiniECommerce.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MiniECommerce.DAL.Concretes
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MiniECommerceDbContext _context;

        public CategoryRepository(MiniECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync() => await _context.Categories.ToListAsync();

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                throw new Exception(" Category not found");
            }

            return category;
        }

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
