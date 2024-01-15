using DataAccess;
using DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        public static ProductDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductDAO();
                }
                return instance;
            }
        }
        private ProductDAO() { }

        public List<Product> GetProducts()
        {
            var dbProvider = new EStoreContext();
            return dbProvider.Products.ToList();
        }

        public Product? GetProduct(int id)
        {
            var dbProvider = new EStoreContext();
            return dbProvider.Products.SingleOrDefault(product => product.ProductId == id);
        }

        public void AddProduct(Product product)
        {
            var dbProvider = new EStoreContext();
            dbProvider.Products.Add(product);
            dbProvider.SaveChanges();
        }


        public void DeleteProduct(int id)
        {
            Product? product = GetProduct(id);
            if (product == null)
            {
                throw new Exception("This product does not already exist.");
            }
            var dbProvider = new EStoreContext();
            dbProvider.Products.Remove(product);
            dbProvider.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            Product? _product = GetProduct(product.ProductId);
            if (_product == null)
            {
                throw new Exception("This product does not already exist.");
            }
            var dbProvider = new EStoreContext();
            dbProvider.Entry<Product>(product).State = EntityState.Modified;
            dbProvider.SaveChanges();
        }

        public List<Product> SearchByProductName(string productName)
        {
            var dbProvider = new EStoreContext();
            return dbProvider.Products.Where(product => product.ProductName.Contains(productName)).ToList();
        }

        public List<Product> SearchByUnitPrice(decimal unitPrice)
        {
            var dbProvider = new EStoreContext();
            return dbProvider.Products.Where(product => product.UnitPrice == unitPrice).ToList();
        }

        public List<Product> SearchByUnitsInStock(int unitsInStock)
        {
            var dbProvider = new EStoreContext();
            return dbProvider.Products.Where(product => product.UnitInStock == unitsInStock).ToList();
        }
    }
}
