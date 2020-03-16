using Microsoft.AspNetCore.Mvc;
using PizzaOrderApplication.Core.Entities;
using PizzaOrderApplication.Core.Exception;
using PizzaOrderApplication.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace PizzaOrderApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost, Route("AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody]CartItem cartItem)
        {
            try
            {
                return Ok(await _cartService.AddToCart(cartItem));
            }
            catch (Exception ex)
            {
                throw new CustomException("Error While Adding to cart!!", ex);
            }
        }

        [HttpGet, Route("RemoveFromCart")]
        public async Task<IActionResult> RemoveFromCart([FromBody]CartItem cartItem)
        {
            try
            {
                return Ok(await _cartService.AddToCart(cartItem));
            }
            catch (Exception ex)
            {
                throw new CustomException("Error While Removing from cart!!", ex);
            }
        }
    }
}
