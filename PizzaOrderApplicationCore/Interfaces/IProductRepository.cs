using PizzaOrderApplication.Core.ValueObjects.Result;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PizzaOrderApplication.Core.Common.Enums;

namespace PizzaOrderApplication.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductResult>> GetMenu();
        Task<decimal> GetProductPriceById(int ProductId);
        Task<decimal> GetProductPriceByCategoryAndType(Category category, int productType);
    }
}
