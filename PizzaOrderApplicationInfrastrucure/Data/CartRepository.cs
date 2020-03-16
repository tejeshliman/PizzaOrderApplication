using PizzaOrderApplication.Core.Entities;
using PizzaOrderApplication.Core.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrderApplication.Infrastrucure.Data
{
    public class CartRepository : ICartRepository
    {
        private readonly PizzaSystemDbContext _dbContext;
        public CartRepository(PizzaSystemDbContext productContext)
        {
            _dbContext = productContext;
        }

        public async Task<Cart> GetCartDetails(int cartId)
        {
            try
            {
                var cartItems = _dbContext.CartItem.Where(cartItm => cartItm.CartId == cartId);
                var cart = _dbContext.Cart.FirstOrDefault(ct => ct.CartId == cartId);
                cart?.CartItems.AddRange(cartItems);
                return await Task.FromResult(cart);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Cart> UpdateCart(Cart cart)
        {
            try
            {
                _dbContext.Cart.Update(cart);
                await _dbContext.SaveChangesAsync();
                return await Task.FromResult(cart);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Cart> AddToCart(CartItem cartItem)
        {
            try
            {
                var cart = GetCartDetails(cartItem.CartId).Result ?? new Cart();
                cart.CartItems.Add(cartItem);
                _dbContext.Cart.Update(cart);
                await _dbContext.SaveChangesAsync();
                return await Task.FromResult(cart);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Cart> RemoveFromCart(CartItem cartItem)
        {
            try
            {
                var cart = GetCartDetails(cartItem.CartId).Result;
                cart.CartItems.Remove(cartItem);
                _dbContext.Cart.Update(cart);
                await _dbContext.SaveChangesAsync();
                return await Task.FromResult(cart);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
