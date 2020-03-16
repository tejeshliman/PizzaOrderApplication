using FakeItEasy;
using PizzaOrderApplication.Core.Entities;
using PizzaOrderApplication.Core.Exception;
using PizzaOrderApplication.Core.Interfaces;
using PizzaOrderApplication.Core.Kernel;
using PizzaOrderApplication.Infrastrucure.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static PizzaOrderApplication.Core.Common.Enums;

namespace PizzaOrderApplication.Tests.Infrastructure.Services
{
    public class CartItemServiceTest
    {
        private readonly IPricingEngine _pricingEngine;
        private readonly IProductRepository _productRepository;
        private readonly ICartItemService _cartItemService;
        public CartItemServiceTest()
        {
            _productRepository = A.Fake<IProductRepository>();
            _pricingEngine = new Infrastrucure.PricingEngine.PricingEngine();
            _cartItemService = new CartItemService(_pricingEngine, _productRepository);
            A.CallTo(() => _productRepository.GetProductPriceById(A<int>._)).Returns(10);
            A.CallTo(() => _productRepository.GetProductPriceByCategoryAndType(A<Category>._, A<int>._)).Returns(20);
        }

        [Fact]
        public void GetCartItemSubTotal_As_Zero_When_CartIsEmpty()
        {
            CartItem cartItem = new CartItem();
            var result = _cartItemService.GetCartItemSubTotal(cartItem).Result;
            Assert.Equal(0, result);
        }

        [Fact]
        public void GetCartItemSubTotal_As_Ten_When_CartWithDefaultItem()
        {
            CartItem cartItem = new CartItem()
            {
                CartItemId = 1,
                ExtraChese = false,
                ProductSize = ProductSize.Small,
                Quantity = 1,
                Toppings = new List<Topping>()
            };

            var result = _cartItemService.GetCartItemSubTotal(cartItem).Result;
            Assert.Equal(10, result);
        }

        [Fact]
        public void GetCartItemSubTotal_As_30_When_CartItem_WithExtraChese()
        {
            CartItem cartItem = new CartItem()
            {
                CartItemId = 1,
                ExtraChese = true,
                ProductSize = ProductSize.Small,
                Quantity = 1,
                Toppings = new List<Topping>()
            };

            var result = _cartItemService.GetCartItemSubTotal(cartItem).Result;
            Assert.Equal(30, result);
        }

        [Fact]
        public void GetCartItemSubTotal_As_30_When_CartItem_WithSauce()
        {
            CartItem cartItem = new CartItem()
            {
                CartItemId = 1,
                ExtraChese = false,
                ProductSize = ProductSize.Small,
                Quantity = 1,
                Sauce = SauceType.Marinara,
                Toppings = new List<Topping>()
            };

            var result = _cartItemService.GetCartItemSubTotal(cartItem).Result;
            Assert.Equal(30, result);
        }

        [Fact]
        public void GetCartItemSubTotal_As_50_When_CartItem_WithSauce_And_ExtraCheese()
        {
            CartItem cartItem = new CartItem()
            {
                CartItemId = 1,
                ExtraChese = true,
                ProductSize = ProductSize.Small,
                Quantity = 1,
                Sauce = SauceType.Marinara,
                Toppings = new List<Topping>()
            };

            var result = _cartItemService.GetCartItemSubTotal(cartItem).Result;
            Assert.Equal(50, result);
        }

        [Fact]
        public void GetCartItemSubTotal_As_20_When_CartItem_WithOneTopping_ExceptsCheese_Sauce()
        {
            CartItem cartItem = new CartItem()
            {
                CartItemId = 1,
                ExtraChese = false,
                ProductSize = ProductSize.Small,
                Quantity = 1,
                Sauce = SauceType.None,
                Toppings = new List<Topping>()
                {
                    new Topping() {  ToppingId=101, CartItemId=1, ProductId=1}
                }
            };

            var result = _cartItemService.GetCartItemSubTotal(cartItem).Result;
            Assert.Equal(20, result);
        }

        [Fact]
        public void GetCartItemSubTotal_As_40_When_CartItem_WithTwoToppings_Cheese_ExceptSauce()
        {
            CartItem cartItem = new CartItem()
            {
                CartItemId = 1,
                ExtraChese = true,
                ProductSize = ProductSize.Small,
                Quantity = 1,
                Sauce = SauceType.None,
                Toppings = new List<Topping>()
                {
                    new Topping() {  ToppingId=101, CartItemId=1, ProductId=1}
                }
            };

            var result = _cartItemService.GetCartItemSubTotal(cartItem).Result;
            Assert.Equal(40, result);
        }

        [Fact]
        public void GetCartItemSubTotal_As_60_When_CartItem_WithSauce_And_ExtraCheese_WithOneTopping()
        {
            CartItem cartItem = new CartItem()
            {
                CartItemId = 1,
                ExtraChese = true,
                ProductSize = ProductSize.Small,
                Quantity = 1,
                Sauce = SauceType.Marinara,
                Toppings = new List<Topping>()
                {
                    new Topping() {  ToppingId=101, CartItemId=1, ProductId=1}
                }
            };

            var result = _cartItemService.GetCartItemSubTotal(cartItem).Result;
            Assert.Equal(60, result);
        }

        [Fact]
        public async Task Error_GetCartItemSubTotal_When_Exception()
        {
            A.CallTo(() => _productRepository.GetProductPriceById(A<int>._)).Throws(new Exception("Error While calculating CartItemSubTotal"));
            var exception = await Assert.ThrowsAsync<CustomException>(() => _cartItemService.GetCartItemSubTotal(new CartItem()));

            Assert.Equal("A service exception occurred, check inner exception", exception.Message);
        }
    }
}
