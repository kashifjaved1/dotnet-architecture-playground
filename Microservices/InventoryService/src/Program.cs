using Common.src.Helpers;
using InventoryService.src.Consumers;
using Shared.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEventBus>(sp =>
    new EventBusRabbitMQ(EnvironmentUtils.GetRabbitMqHost(), "InventoryQueue"));

builder.Services.AddSingleton<OrderCreatedConsumer>(); // Automatically subscribes

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Trigger subscription
using (var scope = app.Services.CreateScope())
{
    _ = scope.ServiceProvider.GetRequiredService<OrderCreatedConsumer>();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();
