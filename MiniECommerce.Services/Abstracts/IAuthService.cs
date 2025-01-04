using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Services.Abstracts
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(string email, string password);
        Task<string> LoginAsync(string email, string password);
    }
}
