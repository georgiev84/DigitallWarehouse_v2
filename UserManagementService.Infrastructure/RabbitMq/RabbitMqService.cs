using RabbitMQ.Client;
using UserManagementService.Application.Common.Interfaces.MessageBroker;

namespace UserManagementService.Infrastructure.RabbitMq;
public class RabbitMqService : IRabbitMqService
{
    private readonly ConnectionFactory _connectionFactory;

    public RabbitMqService(ConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void CreateTopicExchange(string exchangeName)
    {
        using (var connection = _connectionFactory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Topic, durable: true);
        }
    }
}
