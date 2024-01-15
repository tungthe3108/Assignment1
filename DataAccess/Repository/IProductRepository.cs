using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        List<ProductObject> GetProducts();
        ProductObject? GetProduct(int id);
        void AddProduct(ProductObject productObject);
        void DeleteProduct(int id);
        void UpdateProduct(ProductObject productObject);
        List<ProductObject> SearchByProductName(string productName);
        List<ProductObject> SearchByUnitPrice(decimal unitPrice);
        List<ProductObject> SearchByUnitsInStock(int unitsInStock);
    }
}
