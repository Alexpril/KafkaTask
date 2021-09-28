using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public class ResultRepository: IResultRepository
    {
        private static List<Product> Results { get; set; }

        public IEnumerable<Product> GetProducts()
        {
            return Results;
        }
        public Product GetProductByID(int productId)
        {
            return Results[productId];
        }
        public void InsertProduct(Product product)
        {
            if (Results == null) Results = new();
            Results.Add(product);
        }
    }
}
