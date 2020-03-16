using PizzaOrderApplication.Core.Entities;
using System.Threading.Tasks;

namespace PizzaOrderApplication.Core.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartDetails(int cartId);
        Task<Cart> UpdateCart(Cart cart);
        Task<Cart> AddToCart(CartItem cartItem);
        Task<Cart> RemoveFromCart(CartItem cartItem);
    }
}
