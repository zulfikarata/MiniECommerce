using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Services.Abstracts;
using System.Security.Claims;

namespace MiniECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost("{productId}")]
        public async Task<IActionResult> AddProductToBasket(int productId)
        {
            var userId = GetUserId();
            if (!userId.HasValue)
                return Unauthorized("User not logged in.");

            await _basketService.AddProductToBasketAsync(userId.Value, productId);
            return Ok("Product added to basket.");
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveProductFromBasket(int productId)
        {
            var userId = GetUserId();
            if (!userId.HasValue)
                return Unauthorized("User not logged in.");

            await _basketService.RemoveProductFromBasketAsync(userId.Value, productId);
            return Ok("Product removed from basket.");
        }

        [HttpGet]
        public async Task<IActionResult> GetBasketDetails()
        {
            var userId = GetUserId();
            if (!userId.HasValue)
                return Unauthorized("User not logged in.");

            var basketDetails = await _basketService.GetBasketDetailsAsync(userId.Value);
            if (basketDetails == null)
                return NotFound("Basket not found.");

            return Ok(basketDetails);
        }

        private Guid? GetUserId()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userIdClaim, out var userId) ? userId : (Guid?)null;
        }
    }

}
