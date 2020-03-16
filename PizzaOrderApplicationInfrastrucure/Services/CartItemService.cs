using PizzaOrderApplication.Core.Entities;
using PizzaOrderApplication.Core.Exception;
using PizzaOrderApplication.Core.Interfaces;
using PizzaOrderApplication.Core.Kernel;
using PizzaOrderApplication.Infrastrucure.PricingEngine;
using System;
using System.Threading.Tasks;
using static PizzaOrderApplication.Core.Common.Enums;

namespace PizzaOrderApplication.Infrastrucure.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly IPricingEngine _pricingEngine;
        private readonly IProductRepository _productRepository;
        public CartItemService( IPricingEngine pricingEngine, IProductRepository productRepository)
        {
            _pricingEngine = pricingEngine;
            _productRepository = productRepository;
        }

        public Task<decimal> GetCartItemSubTotal(CartItem cartItem)
        {
            try
            {
                decimal subTotal = 0;
                _pricingEngine.SetPricingStrategy(new DefaultStrategy(_productRepository));
                subTotal += _pricingEngine.CalculateCartPrice(cartItem).Result;

                if (cartItem.ExtraChese)
                {
                    _pricingEngine.SetPricingStrategy(new ApplyCheeseStrategy(_productRepository));
                    subTotal += _pricingEngine.CalculateCartPrice(cartItem).Result;
                }
                if (cartItem.Sauce != SauceType.None)
                {
                    _pricingEngine.SetPricingStrategy(new ApplySauceStrategy(_productRepository));
                    subTotal += _pricingEngine.CalculateCartPrice(cartItem).Result;
                }
                if (cartItem.Toppings?.Count > 0)
                {
                    _pricingEngine.SetPricingStrategy(new ApplyToppingsStrategy(_productRepository));
                    subTotal += _pricingEngine.CalculateCartPrice(cartItem).Result;
                }
                return Task.FromResult(subTotal);
            }
            catch (Exception ex)
            {
                throw new CustomException("Error While calculating CartItemSubTotal!!", ex);
            }
        }
    }
}
