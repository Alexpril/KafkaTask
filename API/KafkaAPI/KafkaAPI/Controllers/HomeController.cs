using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace KafkaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        public void Post([FromBody] List<Product> Products)
        {
            StaticProducts.Products = Products;
            DeliveryResult<Null, string> result;
            for(int i = 0; i < StaticProducts.Products.Count; i++)
            {
                result = Producer.ProduceMessage(JsonSerializer.Serialize(Products[i]));
                Console.WriteLine($"Event sent on Partition: {result.Partition} with Offset: {result.Offset}");
            }
        }

        [HttpGet]
        public string Get([FromBody] Results result)
        {
            IEnumerable<Product> filtered = from product in StaticResults.Products where product.Result == result select product;
            return JsonSerializer.Serialize(filtered);
        }
    }
}
