using Confluent.Kafka;
using DataAccess.Enums;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace KafkaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        [HttpPost]
        public void Post([FromBody] List<Product> products)
        {
            ProductRepository Products = new();
            for (int i = 0; i < products.Count; i++)
            {
                Products.InsertProduct(products[i]);
            }

            DeliveryResult<Null, string> result;
            for (int i = 0; i < Products.GetProducts().Count(); i++)
            {
                result = Producer.ProduceMessage(JsonSerializer.Serialize(products[i]));
                Console.WriteLine($"Event sent on Partition: {result.Partition} with Offset: {result.Offset}");
            }
        }

        [HttpGet]
        public string Get([FromBody] Results result)
        {
            ResultsRepository results = new();
            IEnumerable<Product> filtered = from product in results.GetProducts() where product.Result == result select product;
            return JsonSerializer.Serialize(filtered);
        }
    }
}

