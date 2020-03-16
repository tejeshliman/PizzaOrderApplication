using PizzaOrderApplication.Core.Entities;
using PizzaOrderApplication.Core.ValueObjects.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaOrderApplication.Core.Interfaces
{
    public interface ICartItemService
    {
        Task<decimal> GetCartItemSubTotal(CartItem cartItem);
    }
}
