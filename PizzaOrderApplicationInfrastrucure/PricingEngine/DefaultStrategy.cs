using PizzaOrderApplication.Core.Entities;
using PizzaOrderApplication.Core.Exception;
using PizzaOrderApplication.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace PizzaOrderApplication.Infrastrucure.PricingEngine
{
    public class DefaultStrategy : IPricingStrategy
    {
        private readonly IProductRepository _productRepository;
        public DefaultStrategy(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<decimal> CalculatePrice(CartItem cartItem)
        {
            try
            {
                return await _productRepository.GetProductPriceById(cartItem.ProductId) * cartItem.Quantity;
            }
            catch (Exception ex)
            {
                throw new CustomException("Error While Calculating price using DefaultStrategy!!", ex);
            }
        }
    }
}
