
using BusinessObject;
using DataAccess;
using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public OrderRepository(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        private Order MapToOrder(OrderObject orderObject)
        {
            return new Order
            {
                OrderDate = orderObject.OrderDate,
                OrderId = orderObject.OrderId,
                MemberId = orderObject.MemberId,
                RequiredDate = orderObject.RequierdDate,
                ShippedDate = orderObject.ShippedDate,
                Freight = orderObject.Freight,
            };
        }

        private OrderObject MapToOrderObject(Order order)
        {
            return new OrderObject
            {
                Freight = order.Freight,
                OrderDate = order.OrderDate,
                OrderId = order.OrderId,
                MemberId = order.MemberId,
                RequierdDate = order.RequiredDate,
                ShippedDate = order.ShippedDate

            };
        }

        public void AddOrder(OrderObject orderObject)
        {
            Member? member = MemberDAO.Instance.GetMember(orderObject.MemberId);
            if (member == null)
            {
                throw new Exception("Member is not already exist.");
            }
            Order order = MapToOrder(orderObject);
            OrderDAO.Instance.AddOrder(order);
        }

        public void DeleteOrder(int id)
        {
            _orderDetailRepository.DeleteOrderDetailByOrderId(id);
            OrderDAO.Instance.DeleteOrder(id);
        }

        public OrderObject? GetOrder(int id)
        {
            Order? order = OrderDAO.Instance.GetOrder(id);
            return MapToOrderObject(order);
        }

        public List<OrderObject> GetOrders()
        {
            List<Order> orders = OrderDAO.Instance.GetOrders();

            return orders.Select(order =>
            {
                List<OrderDetailObject>? orderDetailObjects = _orderDetailRepository.GetOrderDetailByOrderId(order.OrderId);

                return new OrderObject
                {
                    OrderId = order.OrderId,
                    Freight = order.Freight,
                    OrderDate = order.OrderDate,
                    MemberId = order.MemberId,
                    RequierdDate = order.RequiredDate,
                    ShippedDate = order.ShippedDate,
                    OrderDetailObjects = orderDetailObjects
                };
            }).ToList();
        }

        public void UpdateOrder(OrderObject orderObject)
        {
            Member? member = MemberDAO.Instance.GetMember(orderObject.MemberId);
            if (member == null)
            {
                throw new Exception("Member is not already exist.");
            }
            Order order = MapToOrder(orderObject);
            OrderDAO.Instance.UpdateOrder(order);
        }

        public void DeleteOrderByMemberId(int memberId)
        {
            OrderDAO.Instance.DeleteOrdersByMemberId(memberId);
        }

        public List<OrderObject> GetOrdersByMemberId(int memberId)
        {
            List<Order> orders = OrderDAO.Instance.GetOrdersByMemberId(memberId);

            return orders.Select(order =>
            {
                List<OrderDetailObject>? orderDetailObjects = _orderDetailRepository.GetOrderDetailByOrderId(order.OrderId);

                return new OrderObject
                {
                    OrderId = order.OrderId,
                    Freight = order.Freight,
                    OrderDate = order.OrderDate,
                    MemberId = order.MemberId,
                    RequierdDate = order.RequiredDate,
                    ShippedDate = order.ShippedDate,
                    OrderDetailObjects = orderDetailObjects
                };
            }).ToList();
        }

        public List<OrderObject> GetOrdersByDate(DateTime from, DateTime to)
        {
            List<Order> orders = OrderDAO.Instance.GetOrdersByDate(from, to);

            return orders.Select(order =>
            {
                List<OrderDetailObject>? orderDetailObjects = _orderDetailRepository.GetOrderDetailByOrderId(order.OrderId);

                return new OrderObject
                {
                    OrderId = order.OrderId,
                    Freight = order.Freight,
                    OrderDate = order.OrderDate,
                    MemberId = order.MemberId,
                    RequierdDate = order.RequiredDate,
                    ShippedDate = order.ShippedDate,
                    OrderDetailObjects = orderDetailObjects
                };
            }).ToList();
        }
    }
}
