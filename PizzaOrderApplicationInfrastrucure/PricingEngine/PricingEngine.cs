using PizzaOrderApplication.Core.Entities;
using PizzaOrderApplication.Core.Interfaces;
using PizzaOrderApplication.Core.Kernel;
using System.Threading.Tasks;

namespace PizzaOrderApplication.Infrastrucure.PricingEngine
{
    public class PricingEngine : IPricingEngine
    {
        private IPricingStrategy _pricingStrategy;
        public PricingEngine()
        {
        }

        public void SetPricingStrategy(IPricingStrategy pricingStrategy)
        {
            _pricingStrategy = pricingStrategy;
        }

        public async Task<decimal> CalculateCartPrice(CartItem cartItem)
        {
            return await _pricingStrategy.CalculatePrice(cartItem);
        }
    }
}
