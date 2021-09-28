using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private static List<Product> Products { get; set; }


        public IEnumerable<Product> GetProducts()
        {
            return Products;
        }
        public Product GetProductByID(int productId)
        {
            return Products[productId];
        }
        public void InsertProduct(Product product)
        {
            if(Products == null)
            {
                Products = new();
            }
            Products.Add(product);
        }
        public void InsertProducts(IEnumerable<Product> products)
        {
            if (Products == null)
            {
                Products = new();
            }
            foreach (Product product in products) {
                Products.Add(product);
            }
        }
    }
}
