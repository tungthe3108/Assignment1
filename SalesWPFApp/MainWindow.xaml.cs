using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IProductRepository _prodRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        public MainWindow(IMemberRepository memberRepository,
            IProductRepository prodRepository,
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository)
        {
            InitializeComponent();
            _memberRepository = memberRepository;
            _prodRepository = prodRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            var option = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                WriteIndented = true
            };

            try
            {
                string json = File.ReadAllText("appsettings.json");
                Admin admin = JsonSerializer.Deserialize<Admin>(json, option);

                if (email == admin.Email && password == admin.Password)
                {
                    new AdminHome(_memberRepository, _prodRepository, _orderRepository, _orderDetailRepository).Show();
                    this.Close();
                }
                else if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    MemberObject member = _memberRepository.Login(email, password);
                    if (member == null)
                    {
                        MessageBox.Show("Wrong email or password.");
                        return;
                    }
                    //show member window((
                    new UserHome(member, _orderRepository, _memberRepository).Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please input all email and password.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login");
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
