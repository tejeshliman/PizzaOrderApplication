using FluentValidation;
using PizzaOrderApplication.Core.ValueObjects.Request;

namespace PizzaOrderApplication.Web.Model_Validator
{
    public class OrderRequsetValidator : AbstractValidator<OrderRequest>
    {
        public OrderRequsetValidator()
        {
            RuleFor(order => order.OrderId).NotEmpty();
        }
    }
}
