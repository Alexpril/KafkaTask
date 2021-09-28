using Confluent.Kafka;
using System;
using System.Configuration;

namespace DataAccess.Kafka
{
    public class ProducerKafka: IProducerKafka
    {
        public void ProduceMessage(string message, string[] topics = null)
        {
            KafkaConfig config = new();
            if(topics == null)
            {
                topics = new string[] { config.producerTopic };
            }

            using var producer = new ProducerBuilder<Null, string>(config.ProducerConfig).Build();
            foreach (string topic in topics)
            {
                var result = producer.ProduceAsync(topic, new Message<Null, string> { Value = message }).GetAwaiter().GetResult();
                Console.WriteLine($"Message sent on Topic: {result.Topic} Partition: {result.Partition} with Offset: {result.Offset}");
            }
        }
    }
}
