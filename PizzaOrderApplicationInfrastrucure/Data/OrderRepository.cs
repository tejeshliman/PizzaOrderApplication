using PizzaOrderApplication.Core.Entities;
using PizzaOrderApplication.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrderApplication.Infrastrucure.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly PizzaSystemDbContext _dbContext;
        public OrderRepository(PizzaSystemDbContext productContext)
        {
            _dbContext = productContext;
        }

        public async Task<Order> GetOrderDetailsById(int orderId)
        {
            try
            {
                return await Task.FromResult(_dbContext.Order.FirstOrDefault(prod => prod.OrderId == orderId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Order> CreateOrder(Order order)
        {
            try
            {
                order.OrderedDate = DateTime.Now;
                var result= _dbContext.Order.Add(order);
               await _dbContext.SaveChangesAsync();
                return await Task.FromResult(result.Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            try
            {
                order.OrderedDate = DateTime.Now;
                var result = _dbContext.Order.Update(order);
                await _dbContext.SaveChangesAsync();
                return await Task.FromResult(result.Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
