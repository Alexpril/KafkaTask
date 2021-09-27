using DataAccess.Kafka;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaAPI.Services
{
    public class ConsumerService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Consumer.StartConsumeMessages();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
