using FakeItEasy;
using PizzaOrderApplication.Core.Entities;
using PizzaOrderApplication.Core.Exception;
using PizzaOrderApplication.Core.Interfaces;
using PizzaOrderApplication.Infrastrucure.PricingEngine;
using System;
using System.Threading.Tasks;
using Xunit;
using static PizzaOrderApplication.Core.Common.Enums;

namespace PizzaOrderApplication.Tests.Infrastructure.PricingEngine
{
    public class ApplySauceStrategyTest
    {
        private readonly IProductRepository _productRepository;
        private readonly IPricingStrategy _pricingStrategy;
        public ApplySauceStrategyTest()
        {
            _productRepository = A.Fake<IProductRepository>();
            _pricingStrategy = new ApplySauceStrategy(_productRepository);
        }

        [Fact]
        public void CalculatePrice_Zero_WhenCartEmpty()
        {
            CartItem cartItem = new CartItem();
            var result = _pricingStrategy.CalculatePrice(cartItem).Result;
            Assert.Equal(0, result);
        }

        [Fact]
        public void CalculatePrice_Ten_WhenCartWithValues()
        {
            CartItem cartItem = new CartItem() { CartItemId = 1, ExtraChese = false, ProductSize = ProductSize.Small, Quantity = 1 };
            A.CallTo(() => _productRepository.GetProductPriceByCategoryAndType(A<Category>._, A<int>._)).Returns(10);
            var result = _pricingStrategy.CalculatePrice(cartItem).Result;
            Assert.Equal(10, result);
        }

        [Fact]
        public void CalculatePrice_Zero_WhenCart_Zero_Quanity()
        {
            CartItem cartItem = new CartItem() { CartItemId = 1, ExtraChese = false, ProductSize = ProductSize.Small, Quantity = 0 };
            A.CallTo(() => _productRepository.GetProductPriceByCategoryAndType(A<Category>._, A<int>._)).Returns(10);
            var result = _pricingStrategy.CalculatePrice(cartItem).Result;
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task CalculatePrice_Error_WhenException()
        {
            CartItem cartItem = new CartItem() { CartItemId = 1, ExtraChese = false, ProductSize = ProductSize.Small, Quantity = 1 };
            A.CallTo(() => _productRepository.GetProductPriceByCategoryAndType(A<Category>._, A<int>._)).Throws(new Exception("Error While Calculating price using ApplySauceStrategy"));
            var exception = await Assert.ThrowsAsync<CustomException>(() => _pricingStrategy.CalculatePrice(cartItem));

            Assert.Equal("A service exception occurred, check inner exception", exception.Message);
        }
    }
}
