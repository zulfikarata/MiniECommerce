using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Core.Entities
{
    public class User : IdentityUser<Guid>
    {
    }
}
