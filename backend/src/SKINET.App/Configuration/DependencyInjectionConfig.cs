using Microsoft.AspNetCore.Mvc;
using SKINET.App.Errors;
using SKINET.Business.Interfaces;
using SKINET.Business.Interfaces.IServices;
using SKINET.Business.Interfaces.Repositories;
using SKINET.Business.Services;
using SKINET.Data.Context;
using SKINET.Data.Repositories;

namespace SKINET.App.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependecies(this IServiceCollection services)
        {
            services.AddScoped<StoreContext>();

            services.AddScoped(typeof(IRepository<>), (typeof(Repository<>)));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IDeliveryMethodRepository, DeliveryMethodRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();   
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddAutoMapper(typeof(AutoMapperConfig));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.AddCors(option =>
            {
                option.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .WithOrigins("https://localhost:4200");
                });
            });

            return services;
        }
    }
}
