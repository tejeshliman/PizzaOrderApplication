using PizzaOrderApplication.Core.Entities;
using PizzaOrderApplication.Core.ValueObjects.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaOrderApplication.Core.Interfaces 
{
    public interface ICartService
    {
        Task<Cart> GetCartDetails(int cartId);
        Task<Cart> CalculateCartPrice(Cart cart);
        Task<Cart> AddToCart(CartItem cartItem);
        Task<Cart> RemoveFromCart(CartItem cartItem);
    }
}
