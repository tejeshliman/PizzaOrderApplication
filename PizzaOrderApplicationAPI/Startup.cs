using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PizzaOrderApplication.Core.Interfaces;
using PizzaOrderApplication.Core.Kernel;
using PizzaOrderApplication.Infrastrucure.Data;
using PizzaOrderApplication.Infrastrucure.PricingEngine;
using PizzaOrderApplication.Infrastrucure.Services;
using PizzaOrderApplication.Web.Middleware;

namespace PizzaOrderApplication
{
    public class Startup
    {
        private readonly IApplicationConfiguration _applicationConfig;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _applicationConfig = new ApplicationConfiguration();
            Configuration.Bind(_applicationConfig);

            _applicationConfig.DefaultConnectionString = Configuration["DataBaseConnStr"];
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.WithOrigins("http://localhost:9000")
                                                                           .AllowAnyMethod()
                                                                           .AllowAnyHeader()
                                                                           .AllowCredentials()));

            services.AddMvc(opts =>
            {
            }).AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssemblyContaining<Startup>();
                config.ImplicitlyValidateChildProperties = true;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            services.AddSingleton<IApplicationConfiguration>(_applicationConfig);
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<ICartItemService, CartItemService>();
            services.AddTransient<IOrderService, OrderService>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddTransient<IPricingEngine, PricingEngine>();
            services.AddDbContext<PizzaSystemDbContext>(options => options.UseSqlServer(_applicationConfig.DefaultConnectionString), contextLifetime: ServiceLifetime.Transient); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
