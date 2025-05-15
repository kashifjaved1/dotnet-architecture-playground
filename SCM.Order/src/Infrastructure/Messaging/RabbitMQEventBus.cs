using System.Text.Json;
using System.Text;

namespace SCM.Order.src.Infrastructure.Messaging
{
    public class RabbitMQEventBus : IEventBus
    {
        private readonly IConnection _connection;
        private readonly IServiceProvider _services;

        public RabbitMQEventBus(IConnection connection, IServiceProvider services)
        {
            _connection = connection;
            _services = services;
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            using var channel = _connection.CreateModel();
            channel.ExchangeDeclare("scm_events", ExchangeType.Fanout);
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("scm_events", "", body: body);
        }

        public void Subscribe<T>() where T : IEvent
        {
            var channel = _connection.CreateModel();
            channel.ExchangeDeclare("scm_events", ExchangeType.Fanout);
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queueName, "scm_events", "");

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (_, ea) =>
            {
                var body = ea.Body.ToArray();
                var @event = JsonSerializer.Deserialize<T>(body);
                using var scope = _services.CreateScope();
                var handler = scope.ServiceProvider.GetService<IEventHandler<T>>();
                await handler.Handle(@event);
            };
            channel.BasicConsume(queueName, true, consumer);
        }
    }
}
