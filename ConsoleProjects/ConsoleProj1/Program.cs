using System;

namespace ConsoleProj
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Consumer 1");

            Consumer consumer = new();
            consumer.StartConsumeMessages();
        }
    }
}
