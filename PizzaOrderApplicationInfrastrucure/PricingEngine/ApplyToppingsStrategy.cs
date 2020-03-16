using PizzaOrderApplication.Core.Entities;
using PizzaOrderApplication.Core.Exception;
using PizzaOrderApplication.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace PizzaOrderApplication.Infrastrucure.PricingEngine
{
    public class ApplyToppingsStrategy : IPricingStrategy
    {
        private readonly IProductRepository _productRepository;
        public ApplyToppingsStrategy(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<decimal> CalculatePrice(CartItem cartItem)
        {
            try
            {
                decimal total = 0;
                foreach (var topping in cartItem?.Toppings)
                {
                    total += await _productRepository.GetProductPriceById(topping.ToppingId) * cartItem.Quantity;
                }
                return total;
            }
            catch (Exception ex)
            {
                throw new CustomException("Error While Calculating price using ApplyToppingsStrategy!!", ex);
            }
        }
    }
}
