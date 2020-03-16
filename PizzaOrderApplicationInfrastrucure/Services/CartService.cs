using PizzaOrderApplication.Core.Entities;
using PizzaOrderApplication.Core.Exception;
using PizzaOrderApplication.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace PizzaOrderApplication.Infrastrucure.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemService _cartItemService;
        public CartService(ICartRepository repository, ICartItemService cartItemService)
        {
            _cartRepository = repository;
            _cartItemService = cartItemService;
        }

        public Task<Cart> CalculateCartPrice(Cart cart)
        {
            try
            {
                decimal subTotal = 0;
                foreach (var item in cart.CartItems)
                {
                    subTotal += _cartItemService.GetCartItemSubTotal(item).Result;
                }
                cart.SubTotal = subTotal;
                return Task.FromResult(cart);
            }
            catch (Exception ex)
            {
                throw new CustomException("Error while CalculateCartPrice!!", ex);
            }
        }

        public async Task<Cart> GetCartDetails(int cartId)
        {
            try
            {
                return await _cartRepository.GetCartDetails(cartId);
            }
            catch (Exception ex)
            {
                throw new CustomException("Error while GetCartDetails", ex);
            }
        }

        public async Task<Cart> AddToCart(CartItem cartItem)
        {
            try
            {
                Cart cart = new Cart();
                if (cartItem != null)
                {
                    if (cartItem.CartId > 0)
                    {
                        cart = GetCartDetails(cartItem.CartId).Result;
                    }
                    cart.CartItems.Add(cartItem);
                    await CalculateCartPrice(cart);
                    await _cartRepository.UpdateCart(cart);
                }
                return await Task.FromResult(cart);
            }
            catch (Exception ex)
            {
                throw new CustomException("Error while AddToCart", ex);
            }
        }

        public Task<Cart> RemoveFromCart(CartItem cartItem)
        {
            try
            {
                Cart cart = new Cart();
                if (cartItem != null)
                {
                    if (cartItem.CartId > 0)
                    {
                        cart = GetCartDetails(cartItem.CartId).Result;
                    }
                    cart.CartItems.Remove(cartItem);
                    CalculateCartPrice(cart);
                    _cartRepository.UpdateCart(cart);
                }
                return Task.FromResult(cart);
            }
            catch (Exception ex)
            {
                throw new CustomException("Error while RemoveFromCart", ex);
            }
        }
    }
}
