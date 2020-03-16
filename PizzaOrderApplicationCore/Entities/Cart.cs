using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaOrderApplication.Core.Entities
{
    public class Cart 
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }

        [Key]
        public int CartId { get; set; }
        public decimal SubTotal { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
