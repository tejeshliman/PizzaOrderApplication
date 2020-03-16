using Newtonsoft.Json;
using PizzaOrderApplication.Core.Kernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PizzaOrderApplication.Core.Common.Enums;

namespace PizzaOrderApplication.Core.Entities
{
    public class CartItem 
    {
        public CartItem()
        {
            Toppings = new List<Topping>();
        }

        [Key]
        public  int CartItemId { get; set; }
        [ForeignKey("CartId")]
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public ProductSize ProductSize { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public List<Topping> Toppings { get; set; }
        public SauceType Sauce { get; set; }
        public bool ExtraChese { get; set; }
    }
}
