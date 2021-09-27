using Confluent.Kafka;
using DataAccess.Kafka;

namespace KafkaAPI
{
    public static class Producer
    {
        public static DeliveryResult<Null, string> ProduceMessage(string message)
        {
            using var producer = new ProducerBuilder<Null, string>(KafkaConfig.ProducerConfig).Build();
            return producer.ProduceAsync(KafkaConfig.producerTopic, new Message<Null, string> { Value = message }).GetAwaiter().GetResult();
        }
    }
}