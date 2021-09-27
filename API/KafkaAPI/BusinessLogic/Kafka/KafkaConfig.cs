using Confluent.Kafka;
using System.Configuration;

namespace DataAccess.Kafka
{
    public class KafkaConfig
    {
        public static string consumerTopic = ConfigurationManager.AppSettings.Get("consumerTopic");
        public static string producerTopic = ConfigurationManager.AppSettings.Get("producerTopic");
        public static string bootstrapServers = ConfigurationManager.AppSettings.Get("bootstrapServers");
        public static string consumerGroupId = ConfigurationManager.AppSettings.Get("consumerGroupId");

        public static ConsumerConfig ConsumerConfig { get; set; } = new()
        {
            BootstrapServers = bootstrapServers,
            GroupId = consumerGroupId
        };

        public static ProducerConfig ProducerConfig { get; set; } = new()
        {
            BootstrapServers = bootstrapServers,
        };
    }
}
