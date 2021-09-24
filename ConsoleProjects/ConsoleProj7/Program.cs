using System;

namespace ConsoleProj
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Consumer 7");

            Consumer consumer = new();
            consumer.StartConsumeMessages();
        }
    }
}
