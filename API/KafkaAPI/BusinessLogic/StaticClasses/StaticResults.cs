using System.Collections.Generic;

namespace KafkaAPI
{
    public static class StaticResults
    {
        public static List<Product> Products { get; set; }

        static StaticResults()
        {
            Products = new();
        }
    }
}
