using MiniECommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.DAL.Abstracts
{
    public interface IBasketRepository
    {
        Task AddProductToBasketAsync(Guid userId, int productId);
        Task RemoveProductFromBasketAsync(Guid userId, int productId);
        Task<Basket> GetBasketDetailsAsync(Guid userId);
    }
}
