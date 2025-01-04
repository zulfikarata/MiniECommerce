using MiniECommerce.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Services.Abstracts
{
    public interface IProductService
    {
        Task CreateProductAsync(ProductDto productDto);
        Task UpdateProductAsync(int id, ProductDto productDto);
        Task DeleteProductAsync(int id);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    }

}
