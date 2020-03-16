using PizzaOrderApplication.Core.Entities;
using System.Collections.Generic;

namespace PizzaOrderApplication.Core.ValueObjects.Result
{
    public class ProductResult: Product
    {
        public List<ProductPrice> PriceList { get; set; }
    }
}
