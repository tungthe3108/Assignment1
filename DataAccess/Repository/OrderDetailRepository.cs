using BusinessObject;
using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public OrderDetailRepository()
        {
        }

        private OrderDetailObject MapToOrderDetailObject(OrderDetail orderDetail)
        {
            return new OrderDetailObject
            {
                Discount = orderDetail.Discount,
                OrderId = orderDetail.OrderId,
                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity,
                UnitPrice = orderDetail.UnitPrice,
            };
        }

        private OrderDetail MapToOrderDetail(OrderDetailObject orderDetail)
        {
            return new OrderDetail
            {
                OrderId = orderDetail.OrderId,
                Discount = orderDetail.Discount,
                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity,
                UnitPrice = orderDetail.UnitPrice
            };
        }

        public void AddOrderDetail(OrderDetailObject orderDetailObject)
        {
            Product? product = ProductDAO.Instance.GetProduct(orderDetailObject.ProductId);
            if (product == null)
            {
                throw new Exception("Product does not already exist.");
            }
            OrderDetail orderDetail = MapToOrderDetail(orderDetailObject);
            orderDetail.UnitPrice = product.UnitPrice;
            OrderDetailDAO.Instance.AddOrderDetail(orderDetail);
        }

        public void DeleteOrderDetailByOrderId(int orderId)
        {
            OrderDetailDAO.Instance.DeleteOrderDetailByOrderId(orderId);
        }

        public List<OrderDetailObject>? GetOrderDetailByOrderId(int orderId)
        {
            List<OrderDetail>? orderDetails = OrderDetailDAO.Instance.GetOrderDetailByOrderId(orderId);
            return orderDetails.ConvertAll(orderDetail => MapToOrderDetailObject(orderDetail));
        }

        public void DeleteOrderDetailByProductId(int productId)
        {
            OrderDetailDAO.Instance.DeleteOrderDetailByProductId(productId);
        }

    }
}
