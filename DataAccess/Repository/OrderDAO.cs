
using DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        public static OrderDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderDAO();
                }
                return instance;
            }
        }
        private OrderDAO() { }

        public List<Order> GetOrders()
        {
            var dbProvider = new EStoreContext();
            return dbProvider.Orders.ToList();
        }

        public Order? GetOrder(int id)
        {
            var dbProvider = new EStoreContext();
            return dbProvider.Orders.SingleOrDefault(order => order.OrderId == id);
        }

        public void AddOrder(Order order)
        {
            var dbProvider = new EStoreContext();
            dbProvider.Orders.Add(order);
            dbProvider.SaveChanges();
        }


        public void DeleteOrder(int id)
        {
            Order? order = GetOrder(id);
            if (order == null)
            {
                throw new Exception("This order does not already exist.");
            }
            var dbProvider = new EStoreContext();
            dbProvider.Orders.Remove(order);
            dbProvider.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            Order? _order = GetOrder(order.OrderId);
            if (_order == null)
            {
                throw new Exception("This order does not already exist.");
            }
            var dbProvider = new EStoreContext();
            dbProvider.Entry<Order>(order).State = EntityState.Modified;
            dbProvider.SaveChanges();
        }

        public List<Order> GetOrdersByMemberId(int memberId)
        {
            var dbProvider = new EStoreContext();
            return dbProvider.Orders.Where(order => order.MemberId == memberId).ToList();
        }

        public void DeleteOrdersByMemberId(int memberId)
        {
            List<Order>? orders = GetOrdersByMemberId(memberId);
            orders.ForEach(o =>
            {
                OrderDetailDAO.Instance.DeleteOrderDetailByOrderId(o.OrderId);
                DeleteOrder(o.OrderId);
            });
        }

        public List<Order> GetOrdersByDate(DateTime from, DateTime to)
        {
            var dbProvider = new EStoreContext();
            return dbProvider.Orders
                .Where(x => DateTime.Compare(from, x.OrderDate) <= 0 && DateTime.Compare(to, x.OrderDate) >=0).ToList();
        }
    }
}
