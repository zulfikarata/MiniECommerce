using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Core.DTOs
{
    public class BasketDto
    {
        public Guid UserId { get; set; }
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
