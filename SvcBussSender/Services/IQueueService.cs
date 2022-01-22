using System.Threading.Tasks;

namespace SvcBussSender.Services
{
    public interface IQueueService
    {
        Task SendMessageAsync<T>(T serviceBusMessage, string queueName );
    }
}