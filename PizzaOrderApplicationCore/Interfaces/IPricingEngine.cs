using PizzaOrderApplication.Core.Entities;
using PizzaOrderApplication.Core.Interfaces;
using System.Threading.Tasks;

namespace PizzaOrderApplication.Core.Kernel
{
    public interface IPricingEngine
    {
        void SetPricingStrategy(IPricingStrategy pricingStrategy);

        Task<decimal> CalculateCartPrice(CartItem cartItem);
    }
}
