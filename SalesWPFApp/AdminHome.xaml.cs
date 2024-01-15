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
    /// Interaction logic for AdminHome.xaml
    /// </summary>
    public partial class AdminHome : Window
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IProductRepository _prodRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public AdminHome(IMemberRepository memberRepository,
                        IProductRepository productRepository,
                        IOrderRepository orderRepository,
                        IOrderDetailRepository orderDetailRepository)
        {
            InitializeComponent();
            _memberRepository = memberRepository;
            _prodRepository = productRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        private void btnMember_Click(object sender, RoutedEventArgs e)
        {
            new AdminMemberManagement(_memberRepository).Show();
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            new AdminProductManagement(_prodRepository).Show();
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            new AdminOrdermanagement(_orderRepository, _orderDetailRepository).Show();
        }
    }
}
