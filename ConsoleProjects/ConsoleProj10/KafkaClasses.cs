using Confluent.Kafka;
using System;
using System.Text.Json;
using System.Threading;

namespace ConsoleProj
{
    public static class KafkaConfig
    {
        public const string consumerTopic = "products";
        public const string producerTopic = "results";
        public const string bootstrapServers = "localhost:9092";

        public static ConsumerConfig ConsumerConfig { get; set; } = new()
        {
            BootstrapServers = bootstrapServers,
            GroupId = "consumer10"
        };

        public static ProducerConfig ProducerConfig { get; set; } = new()
        {
            BootstrapServers = bootstrapServers,
        };
    }

    class Consumer
    {
        public void StartConsumeMessages()
        {
            using var consumer = new ConsumerBuilder<Ignore, string>(KafkaConfig.ConsumerConfig).Build();
            {
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
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occurred: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                }
            }
        }

        private static void OnResult(ConsumeResult<Ignore, string> consumerResult)
        {
            Producer producer = new();
            Random random = new();
            Console.WriteLine($"Consumed message '{consumerResult.Message.Value}' at: '{consumerResult.TopicPartitionOffset}'.");

            Product product = JsonSerializer.Deserialize<Product>(consumerResult.Message.Value);
            Results randomResult = (random.Next(101) > 40) ? Results.Success : Results.Failed;
            product.Result = randomResult;
            Thread.Sleep(10000);
            string producerMessage = JsonSerializer.Serialize(product);
            DeliveryResult<Null, string> producerResult = producer.ProduceMessage(producerMessage);
            Console.WriteLine($"Event sent on Partition: {producerResult.Partition} with Offset: {producerResult.Offset}");
        }
    }

    class Producer
    {
        public DeliveryResult<Null, string> ProduceMessage(string message)
        {
            using var producer = new ProducerBuilder<Null, string>(KafkaConfig.ProducerConfig).Build();
            return producer.ProduceAsync(KafkaConfig.producerTopic, new Message<Null, string> { Value = message }).GetAwaiter().GetResult();
        }

    }
}
