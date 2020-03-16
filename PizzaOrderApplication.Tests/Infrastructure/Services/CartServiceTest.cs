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
    public class CartServiceTest
    {
        private readonly IPricingEngine _pricingEngine;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemService _cartItemService;
        private readonly ICartService _cartService;
        public CartServiceTest()
        {
            _productRepository = A.Fake<IProductRepository>();
            _cartRepository = A.Fake<ICartRepository>();
            _pricingEngine = new Infrastrucure.PricingEngine.PricingEngine();
            _cartItemService = new CartItemService( _pricingEngine, _productRepository);
            _cartService = new CartService(_cartRepository, _cartItemService);
            A.CallTo(() => _productRepository.GetProductPriceById(A<int>._)).Returns(10);
            A.CallTo(() => _productRepository.GetProductPriceByCategoryAndType(A<Category>._, A<int>._)).Returns(20);
        }

        [Fact]
        public void CalculateCartPrice_As_Zero_When_CartIsEmpty()
        {
            Cart cart = new Cart();
            _cartService.CalculateCartPrice(cart);
            Assert.Equal(0, cart.SubTotal);
        }

        [Fact]
        public void CalculateCartPrice_As_Ten_When_CartWith_OneDefaultCartItem()
        {
            CartItem cartItem = new CartItem()
            {
                CartItemId = 1,
                ExtraChese = false,
                ProductSize = ProductSize.Small,
                Quantity = 1,
                Toppings = new List<Topping>()
            };
            Cart cart = new Cart()
            {
                CartId = 1,
                SubTotal = 0,
                CartItems = new List<CartItem>() { cartItem }
            };

            _cartService.CalculateCartPrice(cart);
            Assert.Equal(10, cart.SubTotal);
        }

        [Fact]
        public void CalculateCartPrice_As_30_When_CartItem_WithExtraChese()
        {
            CartItem cartItem = new CartItem()
            {
                CartItemId = 1,
                ExtraChese = true,
                ProductSize = ProductSize.Small,
                Quantity = 1,
                Toppings = new List<Topping>()
            };
            Cart cart = new Cart()
            {
                CartId = 1,
                SubTotal = 0,
                CartItems = new List<CartItem>() { cartItem }
            };

            _cartService.CalculateCartPrice(cart);
            Assert.Equal(30, cart.SubTotal);
        }

        [Fact]
        public void CalculateCartPrice_As_30_When_CartItem_WithSauce()
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

            Cart cart = new Cart()
            {
                CartId = 1,
                SubTotal = 0,
                CartItems = new List<CartItem>() { cartItem }
            };

            _cartService.CalculateCartPrice(cart);
            Assert.Equal(30, cart.SubTotal);
        }

        [Fact]
        public void CalculateCartPrice_As_50_When_CartItem_WithSauce_And_ExtraCheese()
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

            Cart cart = new Cart()
            {
                CartId = 1,
                SubTotal = 0,
                CartItems = new List<CartItem>() { cartItem }
            };

            _cartService.CalculateCartPrice(cart);
            Assert.Equal(50, cart.SubTotal);
        }

        [Fact]
        public void CalculateCartPrice_As_20_When_CartItem_WithOneTopping_ExceptsCheese_Sauce()
        {
            var cart = _cartService.CalculateCartPrice(GetCartWithDefaultCartItemHavingTopping()).Result;
            Assert.Equal(20, cart.SubTotal);
        }

        [Fact]
        public void CalculateCartPrice_As_40_When_CartItem_WithTwoToppings_Cheese_ExceptSauce()
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

            Cart cart = new Cart()
            {
                CartId = 1,
                SubTotal = 0,
                CartItems = new List<CartItem>() { cartItem }
            };

            _cartService.CalculateCartPrice(cart);
            Assert.Equal(40, cart.SubTotal);
        }

        [Fact]
        public void CalculateCartPrice_As_60_When_CartItem_WithSauce_And_ExtraCheese_WithOneTopping()
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
                    new Topping() {  ToppingId=101, CartItemId=1, ProductId=1},
                }
            };

            Cart cart = new Cart()
            {
                CartId = 1,
                SubTotal = 0,
                CartItems = new List<CartItem>() { cartItem }
            };

            _cartService.CalculateCartPrice(cart);
            Assert.Equal(60, cart.SubTotal);
        }

        [Fact]
        public void CalculateCartPrice_As_100_When_DifferentCartItems()
        {
            CartItem cartItemWithAll = new CartItem()
            {
                CartItemId = 1,
                ExtraChese = true,
                ProductSize = ProductSize.Small,
                Quantity = 1,
                Sauce = SauceType.Marinara,
                Toppings = new List<Topping>()
                {  new Topping() {  ToppingId=101, CartItemId=1, ProductId=1} }
            };
            CartItem cartItemWithoutOther = new CartItem()
            {
                CartItemId = 1,
                ExtraChese = false,
                ProductSize = ProductSize.Small,
                Quantity = 1,
                Sauce = SauceType.None,
                Toppings = new List<Topping>()
            };
            CartItem cartItemWithCheeseOnly = new CartItem()
            {
                CartItemId = 1,
                ExtraChese = true,
                ProductSize = ProductSize.Small,
                Quantity = 1,
                Sauce = SauceType.None,
                Toppings = new List<Topping>()
            };

            Cart cart = new Cart()
            {
                CartId = 1,
                SubTotal = 0,
                CartItems = new List<CartItem>() { cartItemWithAll, cartItemWithoutOther, cartItemWithCheeseOnly }
            };

            _cartService.CalculateCartPrice(cart);
            Assert.Equal(100, cart.SubTotal);
        }

        [Fact]
        public async Task Error_CalculateCartPrice_When_Exception()
        {
            A.CallTo(() => _productRepository.GetProductPriceById(A<int>._)).Throws(new Exception("Error While calculating CartItemSubTotal"));
            var exception = await Assert.ThrowsAsync<CustomException>(() => _cartItemService.GetCartItemSubTotal(new CartItem()));

            Assert.Equal("A service exception occurred, check inner exception", exception.Message);
        }

        [Fact]
        public void AddToCart_WhenEmptyCart()
        {
            CartItem cartItemWithAll = new CartItem()
            {
                CartItemId = 1,
                ExtraChese = true,
                ProductSize = ProductSize.Small,
                Quantity = 1,
                Sauce = SauceType.Marinara,
                Toppings = new List<Topping>()
                {
                    new Topping() {  ToppingId=101, CartItemId=1, ProductId=1},
                    new Topping() {  ToppingId=102, CartItemId=1, ProductId=2}
                }
            };
            var cart = _cartService.AddToCart(cartItemWithAll).Result;
            Assert.Single(cart.CartItems);
            Assert.Equal(70, cart.SubTotal);
        }

        [Fact]
        public void AddToCart_WhenCart_With_OneItem()
        {
            CartItem cartItemWithAll = new CartItem()
            {
                CartItemId = 1,
                CartId = 1,
                ExtraChese = true,
                ProductSize = ProductSize.Small,
                Quantity = 1,
                Sauce = SauceType.Marinara,
                Toppings = new List<Topping>()
                { new Topping() {  ToppingId=101, CartItemId=1, ProductId=1} }
            };

            A.CallTo(() => _cartRepository.GetCartDetails(A<int>._)).Returns(GetCartWithDefaultCartItemHavingTopping());

            var cart = _cartService.AddToCart(cartItemWithAll).Result;
            Assert.Equal(2, cart.CartItems.Count);
            Assert.Equal(80, cart.SubTotal);
        }

        [Fact]
        public void RemoveFromCart_WhenEmptyCart()
        {
            CartItem cartItemWithAll = new CartItem()
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
            var cart = _cartService.RemoveFromCart(cartItemWithAll).Result;
            Assert.Empty(cart.CartItems);
            Assert.Equal(0, cart.SubTotal);
        }

        [Fact]
        public void RemoveFromCart_WhenCart_With_OneItem()
        {
            CartItem cartItemWithAll = new CartItem()
            {
                CartItemId = 1,
                CartId = 1,
                ExtraChese = true,
                ProductSize = ProductSize.Small,
                Quantity = 1,
                Sauce = SauceType.Marinara,
                Toppings = new List<Topping>()
                {
                    new Topping() {  ToppingId=101, CartItemId=1, ProductId=1}
                }
            };

            A.CallTo(() => _cartRepository.GetCartDetails(A<int>._)).Returns(new Cart());

            var cart = _cartService.RemoveFromCart(cartItemWithAll).Result;
            Assert.Empty(cart.CartItems);
            Assert.Equal(0, cart.SubTotal);
        }

        private Cart GetCartWithDefaultCartItemHavingTopping()
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
                    new Topping() {  ToppingId=102, CartItemId=1, ProductId=2}
                }
            };

            return new Cart()
            {
                CartId = 1,
                SubTotal = 0,
                CartItems = new List<CartItem>() { cartItem }
            };
        }
    }
}
