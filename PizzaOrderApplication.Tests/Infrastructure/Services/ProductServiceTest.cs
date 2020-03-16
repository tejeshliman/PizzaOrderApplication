using FakeItEasy;
using PizzaOrderApplication.Core.Entities;
using PizzaOrderApplication.Core.Exception;
using PizzaOrderApplication.Core.Interfaces;
using PizzaOrderApplication.Core.ValueObjects.Result;
using PizzaOrderApplication.Infrastrucure.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static PizzaOrderApplication.Core.Common.Enums;

namespace PizzaOrderApplication.Tests.Infrastructure.Services
{
    public class ProductServiceTest
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        public ProductServiceTest()
        {
            _productRepository = A.Fake<IProductRepository>();
            _productService = new ProductService(_productRepository);
        }

        [Fact]
        public void Suceess_GetMenu()
        {
            List<ProductResult> productList = new List<ProductResult>()
            {
                new ProductResult()
                {
                    Name="Test1",
                    CategoryType=Category.Pizza,
                    Description="test Desc",
                    ProductType = 1,
                    ProductId =1,
                    ImagePath = "test.jpg",
                    PriceList = new List<ProductPrice>()
                    {
                        new ProductPrice() { Size=ProductSize.Small, ProductPriceId=1, Price=99, ProductId=1},
                        new ProductPrice() { Size=ProductSize.Medium, ProductPriceId=1, Price=199, ProductId=2}
                    }
                }
            };
            A.CallTo(() => _productRepository.GetMenu()).Returns(productList);
            var result = _productService.GetMenu().Result;
            Assert.NotNull(result);
            Assert.IsAssignableFrom<List<ProductResult>>(result);
            var productresultValue = (List<ProductResult>)result;
            Assert.NotNull(productresultValue);
            Assert.Equal(productList.Count, productresultValue.Count);
        }

        [Fact]
        public async Task Error_GetMenu()
        {
            A.CallTo(() => _productRepository.GetMenu()).Throws(new Exception("Error While getting Menu"));
            var exception = await Assert.ThrowsAsync<CustomException>(() => _productService.GetMenu());

            Assert.Equal("A service exception occurred, check inner exception", exception.Message);
        }
    }
}
