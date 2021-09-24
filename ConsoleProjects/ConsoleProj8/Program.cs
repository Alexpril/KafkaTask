using System;

namespace ConsoleProj
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Consumer 8");

            Consumer consumer = new();
            consumer.StartConsumeMessages();
        }
    }
}
