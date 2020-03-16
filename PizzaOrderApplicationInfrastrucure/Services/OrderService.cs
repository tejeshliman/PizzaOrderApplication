using PizzaOrderApplication.Core.Entities;
using PizzaOrderApplication.Core.Exception;
using PizzaOrderApplication.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace PizzaOrderApplication.Infrastrucure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository repository)
        {
            _orderRepository = repository;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            try
            {
                var result = await _orderRepository.CreateOrder(order);
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw new CustomException("Error While Create Order!!", ex);
            }
        }

        public async Task<Order> GetOrderDetailsById(int orderId)
        {
            try
            {
                return await _orderRepository.GetOrderDetailsById(orderId);
            }
            catch (Exception ex)
            {
                throw new CustomException("Error While Get OrderDetails ById!!", ex);
            }
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            try
            {
                var result = await _orderRepository.UpdateOrder(order);
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw new CustomException("Error While Update Order!!", ex);
            }
        }
    }
}
