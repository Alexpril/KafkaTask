using Confluent.Kafka;
using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Text.Json;
using System.Threading;

namespace DataAccess.Kafka
{
    public class Consumer
    {
        public void StartConsumeMessages()
        {
            KafkaConfig config = new();

            using var consumer = new ConsumerBuilder<Ignore, string>(config.ConsumerConfig).Build();
            consumer.Subscribe(config.consumerTopic);

            CancellationTokenSource cancellationToken = new();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cancellationToken.Cancel();
            };

            try
            {
                while (true)
                {
                    try
                    {
                        OnResult(consumer.Consume(cancellationToken.Token));
                    }
                    catch (ConsumeException exception)
                    {
                        Console.WriteLine($"Error occurred: {exception.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }

        private void OnResult(ConsumeResult<Ignore, string> consumerResult)
        {
            ResultRepository results = new();
            Console.WriteLine($"Consumed message '{consumerResult?.Message?.Value}' at: '{consumerResult?.TopicPartitionOffset}'.");
            results.InsertProduct(JsonSerializer.Deserialize<Product>(consumerResult.Message.Value));
        }
    }
}
