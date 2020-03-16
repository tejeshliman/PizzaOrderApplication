using PizzaOrderApplication.Core.ValueObjects.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaOrderApplication.Core.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResult>> GetMenu();
    }
}
