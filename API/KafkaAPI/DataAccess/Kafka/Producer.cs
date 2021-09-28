using Confluent.Kafka;
using DataAccess.Kafka;

namespace KafkaAPI
{
    public class Producer
    {
        public DeliveryResult<Null, string> ProduceMessage(string message)
        {
            KafkaConfig config = new();
            using var producer = new ProducerBuilder<Null, string>(config.ProducerConfig).Build();
            return producer.ProduceAsync(config.producerTopic, new Message<Null, string> { Value = message }).GetAwaiter().GetResult();
        }
    }
}