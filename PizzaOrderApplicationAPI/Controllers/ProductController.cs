using Microsoft.AspNetCore.Mvc;
using PizzaOrderApplication.Core.Exception;
using PizzaOrderApplication.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace PizzaOrderApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet, Route("GetMenu")]
        public async Task<IActionResult> GetMenu()
        {
            try
            {
                return Ok(await _productService.GetMenu());
            }
            catch (Exception ex)
            {
                throw new CustomException("Error While getting Menu!!", ex);
            }
        }
    }
}
