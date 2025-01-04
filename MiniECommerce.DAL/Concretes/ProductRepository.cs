using Microsoft.EntityFrameworkCore;
using MiniECommerce.Core.Entities;
using MiniECommerce.DAL.Abstracts;
using MiniECommerce.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.DAL.Concretes
{
    public class ProductRepository : IProductRepository
    {
        private readonly MiniECommerceDbContext _context;

        public ProductRepository(MiniECommerceDbContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                throw new Exception($"Product with ID {id} not found.");
            }

            return product;
        }


        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }
    }
}
