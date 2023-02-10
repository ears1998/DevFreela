using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace DevFreela.Infrastructure.MessageBus
{
    public class MessageBusService : IMessageBusService
    {
        private readonly ConnectionFactory _connectionFactory;
        public MessageBusService(IConfiguration configuration)
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = configuration.GetSection("RabbitMq:HostName").Value
            };
        }

        public void Publish(string queue, byte[] message)
        {

            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    //Assert that the queue is created
                    channel.QueueDeclare(
                        queue: queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: queue,
                        basicProperties: null,
                        body: message
                        );
                }
            }
        }
    }
}
