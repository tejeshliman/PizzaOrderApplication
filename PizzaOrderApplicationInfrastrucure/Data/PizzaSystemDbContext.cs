using Microsoft.EntityFrameworkCore;
using PizzaOrderApplication.Core.Entities;
using PizzaOrderApplication.Core.Kernel;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PizzaOrderApplication.Infrastrucure.Data
{
    public class PizzaSystemDbContext : DbContext
    {
        public PizzaSystemDbContext(DbContextOptions<PizzaSystemDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<ProductPrice> ProductPrice { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            int result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        protected List<BaseEntity> GetChangedEntities(EntityState entityState)
        {
            return ChangeTracker.Entries()
                .Where(p => p.State == entityState)
                .Select(p => p.Entity).OfType<BaseEntity>().ToList();
        }
    }
}
