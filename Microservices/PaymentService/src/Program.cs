using Common.src.Helpers;
using MassTransit;
using PaymentService.src.Consumers;
using Shared.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEventBus>(sp =>
    new EventBusRabbitMQ(EnvironmentUtils.GetRabbitMqHost(), "PaymentQueue"));

builder.Services.AddSingleton<StockReservedConsumer>(); // Automatically subscribes

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Trigger consumer
using (var scope = app.Services.CreateScope())
{
    _ = scope.ServiceProvider.GetRequiredService<StockReservedConsumer>();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();
