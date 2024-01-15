using BusinessObject;
using DataAccess.DataAccess;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
    /// Interaction logic for UserHome.xaml
    /// </summary>
    public partial class UserHome : Window
    {
        private readonly MemberObject _memberObject;
        private readonly IMemberRepository _memberRepository;
        private readonly IOrderRepository _orderRepository;

        public UserHome(MemberObject memberObject, IOrderRepository orderRepository, IMemberRepository memberRepository)
        {
            InitializeComponent();
            _memberObject = memberObject;
            _memberRepository = memberRepository;
            _orderRepository = orderRepository;

            txtId.Text = memberObject.MemberId.ToString();
            txtCity.Text = memberObject.City.ToString();
            txtCompany.Text = memberObject.CompanyName.ToString();
            txtCountry.Text = memberObject.Country.ToString();
            txtPassword.Password = memberObject.Password;
            txtEmail.Text = memberObject.Email;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string city = txtCity.Text;
                string companyName = txtCompany.Text;
                string country = txtCountry.Text;
                string password = txtPassword.Password;
                string email = txtEmail.Text;

                if (!string.IsNullOrEmpty(city)
                    && !string.IsNullOrEmpty(companyName)
                    && !string.IsNullOrEmpty(country)
                    && !string.IsNullOrEmpty(email)
                    && !string.IsNullOrEmpty(password))
                {
                    _memberObject.City = city;
                    _memberObject.CompanyName = companyName;
                    _memberObject.Country = country;
                    _memberObject.Password = password;
                    _memberObject.Email = email;

                    _memberRepository.UpdateMember(_memberObject);
                    MessageBox.Show("Update profile successfully.", "Update profile");
                }
                else
                {
                    MessageBox.Show("Please enter all.", "Update profile");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update profile");
            }
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            Window newWindow = new WindowOrder(_orderRepository, _memberObject);
            newWindow.Show();

            this.Close();
        }
    }
}
