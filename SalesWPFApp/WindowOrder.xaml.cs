﻿using BusinessObject;
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
    /// Interaction logic for WindowOrder.xaml
    /// </summary>
    public partial class WindowOrder : Window
    {
        public WindowOrder(IOrderRepository orderRepository, MemberObject memberObject)
        {
            InitializeComponent();
            try
            {
                lvOrdersHistory.ItemsSource = orderRepository.GetOrdersByMemberId(memberObject.MemberId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load user's order history");
            }
        }


    }
}
