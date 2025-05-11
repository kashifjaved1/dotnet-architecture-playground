using Microsoft.EntityFrameworkCore;
using SupplyChainManagement.src.Core.Interfaces;
using SupplyChainManagement.src.Inventory.Application.Services;
using SupplyChainManagement.src.Inventory.Infrastructure.Bus;
using SupplyChainManagement.src.Inventory.Infrastructure.Persistence;

namespace SupplyChainManagement.src.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ProjectSetup(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Database
            services.AddDbContext<InventoryDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

            // Dependency Injection
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddSingleton<IEventBus, EventBus>();
            services.AddScoped<InventoryService>();

            return services;
        }
    }
}
