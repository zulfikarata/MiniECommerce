using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Services.Abstracts;

namespace MiniECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string email, string password)
        {
            var result = await _authService.RegisterAsync(email, password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var token = await _authService.LoginAsync(email, password);
            return Ok(token);
        }
    }
}
