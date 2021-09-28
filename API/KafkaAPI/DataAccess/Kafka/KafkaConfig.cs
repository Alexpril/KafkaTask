using Confluent.Kafka;
using System.Configuration;

namespace DataAccess.Kafka
{
    public class KafkaConfig
    {
        public string consumerTopic = ConfigurationManager.AppSettings.Get("consumerTopic");
        public string producerTopic = ConfigurationManager.AppSettings.Get("producerTopic");

        public ConsumerConfig ConsumerConfig { get; set; } = new()
        {
            BootstrapServers = ConfigurationManager.AppSettings.Get("bootstrapServers"),
            GroupId = ConfigurationManager.AppSettings.Get("consumerGroupId")
        };

        public ProducerConfig ProducerConfig { get; set; } = new()
        {
            BootstrapServers = ConfigurationManager.AppSettings.Get("bootstrapServers"),
        };
    }
}
