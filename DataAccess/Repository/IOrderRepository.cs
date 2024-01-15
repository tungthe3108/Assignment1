using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        List<OrderObject> GetOrders();
        OrderObject ? GetOrder(int id);
        void AddOrder(OrderObject orderObject);
        void DeleteOrder(int id);
        void UpdateOrder(OrderObject orderObject);
        void DeleteOrderByMemberId(int memberId);
        List<OrderObject> GetOrdersByMemberId(int memberId);
        List<OrderObject> GetOrdersByDate(DateTime from, DateTime to);
    }
}
