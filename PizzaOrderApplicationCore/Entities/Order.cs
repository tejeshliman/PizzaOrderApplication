using System;
using System.ComponentModel.DataAnnotations;
using static PizzaOrderApplication.Core.Common.Enums;

namespace PizzaOrderApplication.Core.Entities
{
    public class Order
    {

        [Key]
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public AddressType AddType { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public decimal SubTotal { get; set; }
        public int CartId { get; set; }
        public bool IsDelivered { get; set; }
        public DateTime DeliveredOn { get; set; }
        public DateTime OrderedDate { get; set; }
    }
}
