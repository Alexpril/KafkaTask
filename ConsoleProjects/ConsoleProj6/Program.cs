using System;

namespace ConsoleProj
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Consumer 6");

            Consumer consumer = new();
            consumer.StartConsumeMessages();
        }
    }
}
