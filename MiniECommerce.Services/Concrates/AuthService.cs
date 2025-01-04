using Microsoft.AspNetCore.Identity;
using MiniECommerce.Core.Entities;
using MiniECommerce.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Services.Concrates
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(string email, string password)
        {
            var user = new User { UserName = email, Email = email };
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            if (!result.Succeeded)
                throw new Exception("Invalid login attempt.");

            return "Login successful";
        }
    }
}
