using System;

namespace ConsoleProj
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args[0]);
            KafkaConfig.ConsumerConfig.GroupId = args[0];
            Consumer consumer = new();
            consumer.StartConsumeMessages();
        }
    }
}
