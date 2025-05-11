using Common.src.Helpers;
using Shared.Messaging;
using ShippingService.src.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEventBus>(sp =>
    new EventBusRabbitMQ(EnvironmentUtils.GetRabbitMqHost(), "ShippingQueue"));

builder.Services.AddSingleton<PaymentCompletedConsumer>(); // Automatically subscribes

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    _ = scope.ServiceProvider.GetRequiredService<PaymentCompletedConsumer>();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();
