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
        private readonly IProductRepository _productRepository;
        public ApiController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        public void Post([FromBody] List<Product> products)
        {
            Producer producer = new();
            DeliveryResult<Null, string> result;
            _productRepository.InsertProducts(products);

            for (int i = 0; i < _productRepository.GetProducts().Count(); i++)
            {
                result = producer.ProduceMessage(JsonSerializer.Serialize(products[i]));
                Console.WriteLine($"Event sent on Partition: {result.Partition} with Offset: {result.Offset}");
            }
        }

        [HttpGet]
        public string Get([FromBody] Results result)
        {
            ResultRepository results = new();
            IEnumerable<Product> filtered = from product in results.GetProducts() where product.Result == result select product;
            return JsonSerializer.Serialize(filtered);
        }
    }
}

