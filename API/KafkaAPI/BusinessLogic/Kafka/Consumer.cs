using Confluent.Kafka;
using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Text.Json;
using System.Threading;

namespace DataAccess.Kafka
{
    public static class Consumer
    {
        public static void StartConsumeMessages()
        {
            using var consumer = new ConsumerBuilder<Ignore, string>(KafkaConfig.ConsumerConfig).Build();

            consumer.Subscribe(KafkaConfig.consumerTopic);

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

        private static void OnResult(ConsumeResult<Ignore, string> consumerResult)
        {
            ResultsRepository results = new();
            Console.WriteLine($"Consumed message '{consumerResult?.Message?.Value}' at: '{consumerResult?.TopicPartitionOffset}'.");
            results.InsertProduct(JsonSerializer.Deserialize<Product>(consumerResult.Message.Value));
        }
    }
}
