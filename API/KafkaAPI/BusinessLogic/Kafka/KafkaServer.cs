using System;
using System.Text.Json;
using System.Threading;
using Confluent.Kafka;

namespace KafkaAPI
{
    public static class KafkaConfig
    {
        public const string consumerTopic = "results";
        public const string producerTopic = "products";
        public const string bootstrapServers = "kafka:9093";

        public static ConsumerConfig ConsumerConfig { get; set; } = new()
        {
            BootstrapServers = bootstrapServers,
            GroupId = "MainConsumer"
        };

        public static ProducerConfig ProducerConfig { get; set; } = new()
        {
            BootstrapServers = bootstrapServers,
        };
    }

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
            Console.WriteLine($"Consumed message '{consumerResult?.Message?.Value}' at: '{consumerResult?.TopicPartitionOffset}'.");
            StaticResults.Products.Add(JsonSerializer.Deserialize<Product>(consumerResult.Message.Value));
        }
    }

    public static class Producer
    {
        public static DeliveryResult<Null, string> ProduceMessage(string message)
        {
            using var producer = new ProducerBuilder<Null, string>(KafkaConfig.ProducerConfig).Build();
            return producer.ProduceAsync(KafkaConfig.producerTopic, new Message<Null, string> { Value = message }).GetAwaiter().GetResult();
        }
    }
}