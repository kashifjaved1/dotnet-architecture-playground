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
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("src/Web/appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddSingleton<IEventBus, EventBus>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<InventoryService>();

            return services;
        }
    }
}
