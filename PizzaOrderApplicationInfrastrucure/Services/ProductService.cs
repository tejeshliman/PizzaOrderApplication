using PizzaOrderApplication.Core.Exception;
using PizzaOrderApplication.Core.Interfaces;
using PizzaOrderApplication.Core.ValueObjects.Result;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaOrderApplication.Infrastrucure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository repository)
        {
            _productRepository = repository;
        }

        public async Task<IEnumerable<ProductResult>> GetMenu()
        {
            try
            {
                return await _productRepository.GetMenu();
            }
            catch (Exception ex)
            {
                throw new CustomException("Error While GetMenu From ProductService!!", ex);
            }
        }
    }
}
