using System.ComponentModel.DataAnnotations;
using static PizzaOrderApplication.Core.Common.Enums;

namespace PizzaOrderApplication.Core.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category CategoryType { get; set; }
        public int ProductType { get; set; }
        public string ImagePath { get; set; }
    }
}
