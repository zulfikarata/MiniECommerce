using MiniECommerce.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Services.Abstracts
{
    public interface IBasketService
    {
        Task AddProductToBasketAsync(Guid userId, int productId);
        Task RemoveProductFromBasketAsync(Guid userId, int productId);
        Task<BasketDto> GetBasketDetailsAsync(Guid userId);
    }

}
