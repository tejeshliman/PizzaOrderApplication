using Microsoft.AspNetCore.Mvc;
using PizzaOrderApplication.Core.Entities;
using PizzaOrderApplication.Core.Exception;
using PizzaOrderApplication.Core.Interfaces;
using PizzaOrderApplication.Core.ValueObjects.Request;
using System;
using System.Threading.Tasks;

namespace PizzaOrderApplication.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController( IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create([FromBody]Order order)
        {
            try
            {
                return Ok(await _orderService.CreateOrder(order));
            }
            catch (Exception ex)
            {
                throw new CustomException("Error While creating order!!", ex);
            }
        }

        [HttpPost, Route("Details")]
        public async Task<IActionResult> Details([FromBody]OrderRequest orderRequest)
        {
            try
            {
                return Ok(await _orderService.GetOrderDetailsById(orderRequest.OrderId));
            }
            catch (Exception ex)
            {
                throw new CustomException("Error While getting order details!!", ex);
            }
        }

        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update([FromBody]Order order)
        {
            try
            {
                return Ok(await _orderService.UpdateOrder(order));
            }
            catch (Exception ex)
            {
                throw new CustomException("Error While updating order!!", ex);
            }
        }
    }
}