using PizzaOrderApplication.Core.Entities;
using System.Threading.Tasks;

namespace PizzaOrderApplication.Core.Interfaces
{
    public interface IPricingStrategy
    {
        Task<decimal> CalculatePrice(CartItem cartItem);
    }
}
