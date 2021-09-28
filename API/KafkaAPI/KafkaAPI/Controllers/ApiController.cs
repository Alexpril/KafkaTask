using Confluent.Kafka;
using DataAccess.Enums;
using DataAccess.Kafka;
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
        private readonly IResultRepository _resultRepository;
        private readonly IProducerKafka _producer;
        public ApiController(IProductRepository productRepository, IResultRepository resultRepository, IProducerKafka producer)
        {
            _productRepository = productRepository;
            _resultRepository = resultRepository;
            _producer = producer;
        }

        [HttpPost]
        public void Post([FromBody] List<Product> products)
        {
            _productRepository.InsertProducts(products);

            for (int i = 0; i < _productRepository.GetProducts().Count(); i++)
            {
                _producer.ProduceMessage(JsonSerializer.Serialize(products[i]), new string[] { "products"});
            }
        }

        [HttpGet]
        public string Get([FromBody] Results result)
        {
            IEnumerable<Product> filtered = from product in _resultRepository.GetProducts() where product.Result == result select product;
            return JsonSerializer.Serialize(filtered);
        }
    }
}

