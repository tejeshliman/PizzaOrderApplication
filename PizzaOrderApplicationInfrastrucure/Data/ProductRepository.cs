using PizzaOrderApplication.Core.Interfaces;
using PizzaOrderApplication.Core.ValueObjects.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PizzaOrderApplication.Core.Common.Enums;

namespace PizzaOrderApplication.Infrastrucure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly PizzaSystemDbContext _dbContext;
        public ProductRepository(PizzaSystemDbContext productContext)
        {
            _dbContext = productContext;
        }

        public async Task<IEnumerable<ProductResult>> GetMenu()
        {
            try
            {
                return await Task.FromResult(_dbContext.Product.Where(p => (int)p.CategoryType < 4).Select(prod => new ProductResult()
                {
                    ProductId = prod.ProductId,
                    Name = prod.Name,
                    Description = prod.Description,
                    CategoryType = prod.CategoryType,
                    ProductType = prod.ProductType,
                    ImagePath = prod.ImagePath,
                    PriceList = _dbContext.ProductPrice.Where(pl => pl.ProductId == prod.ProductId).ToList(),
                }).ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<decimal> GetProductPriceById(int ProductId)
        {
            try
            {
                return await Task.FromResult(_dbContext.ProductPrice.FirstOrDefault(prod => prod.ProductPriceId == ProductId)?.Price ?? 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<decimal> GetProductPriceByCategoryAndType(Category category, int productType)
        {
            try
            {
                var productId = _dbContext.Product.FirstOrDefault(x => x.CategoryType == category && x.ProductType == productType)?.ProductId ?? 0;
                return await Task.FromResult(_dbContext.ProductPrice.FirstOrDefault(prod => prod.ProductPriceId == productId)?.Price ?? 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
