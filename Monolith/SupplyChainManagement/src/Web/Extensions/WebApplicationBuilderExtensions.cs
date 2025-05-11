using SupplyChainManagement.src.Core.Domain.Events;
using SupplyChainManagement.src.Core.Interfaces;

namespace SupplyChainManagement.src.Web.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplication BuildAndConfigureRequestPipeline(this WebApplicationBuilder builder)
        {
            var app = builder.Build();

            // Event Subscriptions
            var eventBus = app.Services.GetRequiredService<IEventBus>();
            eventBus.Subscribe<LowStockEvent>(evt =>
                Console.WriteLine($"LOW STOCK ALERT: Item {evt.ItemId} has {evt.RemainingStock} left"));

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
