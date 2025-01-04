using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MiniECommerce.Core.DTOs;
using MiniECommerce.Core.Entities;
using MiniECommerce.DAL.Abstracts;
using MiniECommerce.DAL.Contexts;
using MiniECommerce.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Services.Concrates
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;

        public BasketService(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task AddProductToBasketAsync(Guid userId, int productId)
        {
            await _basketRepository.AddProductToBasketAsync(userId, productId);
        }

        public async Task RemoveProductFromBasketAsync(Guid userId, int productId)
        {
            await _basketRepository.RemoveProductFromBasketAsync(userId, productId);
        }

        public async Task<BasketDto> GetBasketDetailsAsync(Guid userId)
        {
            var basket = await _basketRepository.GetBasketDetailsAsync(userId);
            if (basket == null)
            {
                throw new Exception("Basket not found.");
            }

            return new BasketDto
            {
                UserId = basket.UserId,
                Products = basket.Products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Image = p.Image
                }).ToList()
            };
        }
    }

}
