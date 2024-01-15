using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {
        List<OrderDetailObject>? GetOrderDetailByOrderId(int orderId);
        void AddOrderDetail(OrderDetailObject orderDetailObject);
        void DeleteOrderDetailByOrderId(int orderId);
        void DeleteOrderDetailByProductId(int ordertId);
    }
}
