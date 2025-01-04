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
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Services.Concrates
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task CreateProductAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Title = productDto.Title,
                Description = productDto.Description,
                Image = productDto.Image,
                CategoryId = productDto.CategoryId,
                Price = productDto.Price
            };
            await _productRepository.AddProductAsync(product);
        }

        public async Task UpdateProductAsync(int id, ProductDto productDto)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product != null)
            {
                product.Title = productDto.Title;
                product.Description = productDto.Description;
                product.Image = productDto.Image;
                product.CategoryId = productDto.CategoryId;
                product.Price = productDto.Price;
                await _productRepository.UpdateProductAsync(product);
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteProductAsync(id);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new Exception($"Product with ID {id} not found.");
            }

            return new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Image = product.Image,
                CategoryName = product.Category?.Name,
                Price = product.Price
            };
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Image = p.Image,
                CategoryId = p.CategoryId,
                Price = p.Price
            });
        }
    }

}
