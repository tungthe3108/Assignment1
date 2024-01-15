using BusinessObject;
using DataAccess;
using DataAccess.DataAccess;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for AdminOrdermanagement.xaml
    /// </summary>
    public partial class AdminOrdermanagement : Window
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        public AdminOrdermanagement(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            InitializeComponent();
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            ListAllOrders();
        }

        private Order GetOrderObject()
        {
            Order order = null;
            string text = txtOrderId.Text;
            try
            {
                if (text == "")
                {
                    order = new Order
                    {
                        MemberId = int.Parse(txtOrderId.Text),
                        OrderDate = (DateTime)orderDate.SelectedDate,
                        RequiredDate = (DateTime)dpRequiredDate.SelectedDate,
                        ShippedDate = (DateTime)dpShipDate.SelectedDate,
                        Freight = decimal.Parse(txtFreight.Text)
                    };
                }
                else
                {
                    order = new Order
                    {
                        OrderId = int.Parse(txtOrderId.Text),
                        MemberId = int.Parse(txtOrderId.Text),
                        OrderDate = (DateTime)orderDate.SelectedDate,
                        RequiredDate = (DateTime)dpRequiredDate.SelectedDate,
                        ShippedDate = (DateTime)dpShipDate.SelectedDate,
                        Freight = decimal.Parse(txtFreight.Text)
                    };
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Product");
            }
            return order;
        }

        public void ListAllOrders()
        {
            lvOrder.ItemsSource = _orderRepository.GetOrders();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            /*try
            {
                Order p = GetOrderObject();
                ProductObject productObject = new ProductObject
                {
                    ProductId = p.ProductId,
                    CategoryId = p.CategoryId,
                    ProductName = p.ProductName,
                    Weight = p.Weight,
                    UnitPrice = p.UnitPrice,
                    UnitsInStock = p.UnitsInStock
                };
                _productRepository.UpdateProduct(productObject);
                ListAllProducts();
                MessageBox.Show("Update success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update fail");
            }*/
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order p = GetOrderObject();
                OrderObject orderObject = new OrderObject
                {
                    MemberId = p.MemberId,
                    OrderDate = p.OrderDate,
                    RequierdDate = p.RequiredDate,
                    ShippedDate = p.ShippedDate,
                    Freight = p.Freight
                };
                _orderRepository.AddOrder(orderObject);
                ListAllOrders();
                MessageBox.Show("Add success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add fail");
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order o = GetOrderObject();
                _orderRepository.DeleteOrder(o.OrderId);
                ListAllOrders();
                MessageBox.Show("Delete success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete fail");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            DateTime start = dateFrom.SelectedDate.Value;
            DateTime end = dateTo.SelectedDate.Value;
            lvOrder.ItemsSource = OrderDAO.Instance.GetOrdersByDate(start, end);
        }
    }
}
