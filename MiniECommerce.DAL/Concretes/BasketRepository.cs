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
    public class BasketRepository : IBasketRepository
    {
        private readonly MiniECommerceDbContext _context;

        public BasketRepository(MiniECommerceDbContext context)
        {
            _context = context;
        }

        public async Task AddProductToBasketAsync(Guid userId, int productId)
        {
            var basket = await _context.Baskets.Include(b => b.Products).FirstOrDefaultAsync(b => b.UserId == userId);
            if (basket == null)
            {
                basket = new Basket { UserId = userId, Products = new List<Product>() };
                _context.Baskets.Add(basket);
            }

            if (!basket.Products.Any(p => p.Id == productId))
            {
                var product = await _context.Products.FindAsync(productId);
                if (product != null)
                    basket.Products.Add(product);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveProductFromBasketAsync(Guid userId, int productId)
        {
            var basket = await _context.Baskets.Include(b => b.Products).FirstOrDefaultAsync(b => b.UserId == userId);
            if (basket != null)
            {
                var product = basket.Products.FirstOrDefault(p => p.Id == productId);
                if (product != null)
                    basket.Products.Remove(product);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Basket> GetBasketDetailsAsync(Guid userId)
        {
            var basket = await _context.Baskets.Include(b => b.Products).FirstOrDefaultAsync(b => b.UserId == userId);

            if (basket == null)
            {
                throw new Exception("Basket not found.");
            }

            return basket;
        }
    }

}
