namespace PizzaOrderApplication.Core.Common
{
    public class Enums
    {
        public enum AddressType
        {
            House = 1,
            Appartment = 2,
            Office = 3,
            Other = 4
        }

        public enum Category
        {
            Pizza = 1,
            Dessert = 2,
            Cheese = 3,
            Sauce = 4,
            Toppings = 5
        }

        public enum ProductSize
        {
            None,
            Small = 1,
            Medium = 2,
            Large = 3
        }

        public enum SauceType
        {
            None,
            Marinara = 1,
            Cheese = 2,
            Ranch = 3,
            other = 4
        }
    }
}
