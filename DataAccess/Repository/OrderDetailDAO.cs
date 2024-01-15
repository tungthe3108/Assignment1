using DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        public static OrderDetailDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderDetailDAO();
                }
                return instance;
            }
        }
        private OrderDetailDAO() { }

        public List<OrderDetail>? GetOrderDetailByOrderId(int orderId)
        {
            var dbProvider = new EStoreContext();
            return dbProvider.OrderDetails.Where(orderDetail => orderDetail.OrderId == orderId).ToList();
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            var dbProvider = new EStoreContext();
            dbProvider.OrderDetails.Add(orderDetail);
            dbProvider.SaveChanges();
        }


        public void DeleteOrderDetailByOrderId(int orderId)
        {
            List<OrderDetail>? orderDetails = GetOrderDetailByOrderId(orderId);
            if (orderDetails == null)
            {
                throw new Exception("This orderDetail does not already exist.");
            }
            var dbProvider = new EStoreContext();
            orderDetails.ForEach(orderDetail => dbProvider.OrderDetails.Remove(orderDetail));
            dbProvider.SaveChanges();
        }

        private List<OrderDetail>? GetOrderDetailsByProductId(int productId)
        {
            var dbProvider = new EStoreContext();
            return dbProvider.OrderDetails.Where(od => od.ProductId == productId).ToList();
        }

        public void DeleteOrderDetailByProductId(int productId)
        {
            List<OrderDetail>? orderDetails = GetOrderDetailsByProductId(productId);
            if (orderDetails == null)
            {
                throw new Exception("This orderDetail does not already exist.");
            }
            var dbProvider = new EStoreContext();
            orderDetails.ForEach(orderDetail => dbProvider.OrderDetails.Remove(orderDetail));
            dbProvider.SaveChanges();
        }
    }
}
