using FakeItEasy;
using PizzaOrderApplication.Core.Entities;
using PizzaOrderApplication.Core.Exception;
using PizzaOrderApplication.Core.Interfaces;
using PizzaOrderApplication.Infrastrucure.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PizzaOrderApplication.Tests.Infrastructure.Services
{
    public class OrderServiceTest
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderService _orderService;
        public OrderServiceTest()
        {
            _orderRepository = A.Fake<IOrderRepository>();
            _orderService = new OrderService(_orderRepository);
        }

        [Fact]
        public void Suceess_GetOrderDetailsById()
        {
            var orderDetails = GetOrderData();
            A.CallTo(() => _orderRepository.GetOrderDetailsById(A<int>._)).Returns(orderDetails);
            var result = _orderService.GetOrderDetailsById(6).Result;
            Assert.NotNull(result);
            Assert.IsAssignableFrom<Order>(result);
            var OrderValue = result;
            Assert.NotNull(OrderValue);
            Assert.Equal(OrderValue.OrderId, orderDetails.OrderId);
        }

        [Fact]
        public async Task Error_GetOrderDetailsById()
        {
            A.CallTo(() => _orderRepository.GetOrderDetailsById(A<int>._)).Throws(new Exception("Error While getting order"));
            var exception = await Assert.ThrowsAsync<CustomException>(() => _orderService.GetOrderDetailsById(6));

            Assert.Equal("A service exception occurred, check inner exception", exception.Message);
        }

        [Fact]
        public void Success_CreateOrder()
        {
            var orderDetails = GetOrderData();
            var orderInput =  new Order()
            {
                UserName = "Test",
                AddType = Core.Common.Enums.AddressType.House,
                Address = "Test Address",
                Country = "India",
                ZipCode = "400000",
                PhoneNumber = "11111111",
                SubTotal = 10,
                CartId = 1,
                IsDelivered = false,
                OrderedDate = DateTime.Now
            };

            A.CallTo(() => _orderRepository.CreateOrder(A<Order>._)).Returns(GetOrderData());

            var result = _orderRepository.CreateOrder(orderInput).Result;
            Assert.NotNull(result);
            Assert.IsAssignableFrom<Order>(result);
            var OrderValue = result;
            Assert.NotNull(OrderValue);
            Assert.Equal(OrderValue.UserName, orderDetails.UserName);
        }

        [Fact]
        public async Task Error_CreateOrder()
        {
            A.CallTo(() => _orderRepository.CreateOrder(A<Order>._)).Throws(new Exception("Error While creating order"));
            var exception = await Assert.ThrowsAsync<CustomException>(() => _orderService.CreateOrder(new Order()));

            Assert.Equal("A service exception occurred, check inner exception", exception.Message);
        }

        [Fact]
        public void Success_UpdateOrder()
        {
            var orderDetails = GetOrderData();
            var orderInput = new Order()
            {
                UserName = "Test",
                AddType = Core.Common.Enums.AddressType.House,
                Address = "Test Address",
                Country = "India",
                ZipCode = "400000",
                PhoneNumber = "11111111",
                SubTotal = 10,
                CartId = 1,
                IsDelivered = false,
                OrderedDate = DateTime.Now
            };

            A.CallTo(() => _orderRepository.UpdateOrder(A<Order>._)).Returns(GetOrderData());

            var result = _orderRepository.UpdateOrder(orderInput).Result;
            Assert.NotNull(result);
            Assert.IsAssignableFrom<Order>(result);
            var OrderValue = result;
            Assert.NotNull(OrderValue);
            Assert.Equal(OrderValue.UserName, orderDetails.UserName);
        }

        [Fact]
        public async Task Error_UpdateOrder()
        {
            A.CallTo(() => _orderRepository.UpdateOrder(A<Order>._)).Throws(new Exception("Error While updating order"));
            var exception = await Assert.ThrowsAsync<CustomException>(() => _orderService.UpdateOrder(new Order()));

            Assert.Equal("A service exception occurred, check inner exception", exception.Message);
        }


        private Order GetOrderData()
        {
           return new Order()
            {
                OrderId = 6,
                UserName = "Test",
                AddType = Core.Common.Enums.AddressType.House,
                Address = "Test Address",
                Country = "India",
                ZipCode = "400000",
                PhoneNumber = "11111111",
                SubTotal = 10,
                CartId = 1,
                IsDelivered = false,
                DeliveredOn = DateTime.Now,
                OrderedDate = DateTime.Now
            };
        }
    }
}
