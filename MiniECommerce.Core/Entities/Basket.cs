
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Core.Entities
{
    public class Basket
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = new User();
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
