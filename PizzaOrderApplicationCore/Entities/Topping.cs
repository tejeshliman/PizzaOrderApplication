using System.ComponentModel.DataAnnotations;

namespace PizzaOrderApplication.Core.Entities
{
    public class Topping
    {
        [Key]
        public int ToppingId { get; set; }
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
    }
}
