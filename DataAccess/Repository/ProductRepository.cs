
using BusinessObject;
using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository()
        {
        }

        private Product MapToProduct(ProductObject productObject)
        {
            return new Product 
            { 
                ProductId = productObject.ProductId, 
                ProductName = productObject.ProductName, 
                CategoryId = productObject.CategoryId,
                Weight= productObject.Weight,
                UnitPrice= productObject.UnitPrice,
                UnitInStock= productObject.UnitsInStock
            };
        }

        private ProductObject MapToProductObject(Product product)
        {
            return new ProductObject
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                Weight = product.Weight,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitInStock
            };
        }

        public void AddProduct(ProductObject productObject)
        {
            Product product = MapToProduct(productObject);
            ProductDAO.Instance.AddProduct(product);
        }

        public void DeleteProduct(int id)
        {
            OrderDetailDAO.Instance.DeleteOrderDetailByProductId(id);
            ProductDAO.Instance.DeleteProduct(id);
        }

        public ProductObject? GetProduct(int id)
        {
            Product? product = ProductDAO.Instance.GetProduct(id);
            return MapToProductObject(product);
        }

        public List<ProductObject> GetProducts()
        {
            List<Product> products = ProductDAO.Instance.GetProducts();
            return products.ConvertAll<ProductObject>(product => MapToProductObject(product));
        }

        public void UpdateProduct(ProductObject productObject)
        {
            Product product = MapToProduct(productObject);
            ProductDAO.Instance.UpdateProduct(product);
        }

        public List<ProductObject> SearchByProductName(string productName)
        {
            List<Product> products = ProductDAO.Instance.SearchByProductName(productName);
            return products.ConvertAll(product => MapToProductObject(product));
        }

        public List<ProductObject> SearchByUnitPrice(decimal unitPrice)
        {
            List<Product> products = ProductDAO.Instance.SearchByUnitPrice(unitPrice);
            return products.ConvertAll(product => MapToProductObject(product));
        }

        public List<ProductObject> SearchByUnitsInStock(int unitsInStock)
        {
            List<Product> products = ProductDAO.Instance.SearchByUnitsInStock(unitsInStock);
            return products.ConvertAll(product => MapToProductObject(product));
        }
    }
}
