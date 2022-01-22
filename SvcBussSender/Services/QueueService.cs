using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace SvcBussSender.Services
{
    
    public class QueueService : IQueueService
    {
        private readonly IConfiguration _config;

        public QueueService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendMessageAsync<T>(T serviceBusMessage, string queueName ) // we created "personqueue" in Azure for our buss "usssvcbuss"
        {
            // create queue client
            QueueClient queueClient = new QueueClient(_config.GetConnectionString("AzureServiceBus"), queueName);

            Message message = CreateMessage(serviceBusMessage);
         
            await queueClient.SendAsync(message);
        }

        private Message CreateMessage<T>(T serviceBusMessage)
        {
            // convert to json
            string messageBody = JsonSerializer.Serialize(serviceBusMessage);
            // convert to a encoded byteArray message as a payload for transmission efficiency
            return new Message(Encoding.UTF8.GetBytes(messageBody));
        }
    }
}
