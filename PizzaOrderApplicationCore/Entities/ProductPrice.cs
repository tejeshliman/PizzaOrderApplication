using System.ComponentModel.DataAnnotations;
using static PizzaOrderApplication.Core.Common.Enums;

namespace PizzaOrderApplication.Core.Entities
{
    public class ProductPrice
    {
        [Key]
        public int ProductPriceId { get; set; }
        public int ProductId { get; set; }
        public ProductSize Size { get; set; }
        public decimal Price { get; set; }
    }
}
