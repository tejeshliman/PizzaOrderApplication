using PizzaOrderApplication.Core.Entities;
using PizzaOrderApplication.Core.Exception;
using PizzaOrderApplication.Core.Interfaces;
using System;
using System.Threading.Tasks;
using static PizzaOrderApplication.Core.Common.Enums;

namespace PizzaOrderApplication.Infrastrucure.PricingEngine
{
    public class ApplyCheeseStrategy : IPricingStrategy
    {
        private readonly IProductRepository _productRepository;
        public ApplyCheeseStrategy(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<decimal> CalculatePrice(CartItem cartItem)
        {
            try
            {
                return await _productRepository.GetProductPriceByCategoryAndType(Category.Cheese, cartItem.ExtraChese ? 1 : 0) * cartItem.Quantity;
            }
            catch (Exception ex)
            {
                throw new CustomException("Error While Calculating price using ApplyCheeseStrategy!!", ex);
            }
        }
    }
}
