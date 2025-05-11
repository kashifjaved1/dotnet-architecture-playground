using Common.src.Helpers;
using InventoryService.src.Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderService.src.Application.Handlers;
using OrderService.src.Infrastructure;
using Shared.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(EnvironmentUtils.GetRabbitMqHost(), "/", h => {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context); // 🚨 This is crucial
    });
});

builder.Services.AddDbContext<OrderDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("OrderDb")));

builder.Services.AddSingleton<IEventBus>(sp =>
    new EventBusRabbitMQ(EnvironmentUtils.GetRabbitMqHost(), "OrderQueue"));

builder.Services.AddScoped<CreateOrderHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();