using BusinessObject;
using DataAccess.DataAccess;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
    /// Interaction logic for AdminMemberManagement.xaml
    /// </summary>
    public partial class AdminMemberManagement : Window
    {
        private readonly IMemberRepository _memberRepository;
        public AdminMemberManagement(IMemberRepository memberRepository)
        {
            InitializeComponent();
            _memberRepository = memberRepository;
            ListAllMember();
        }

        public void ListAllMember()
        {
            lvMembers.ItemsSource = _memberRepository.GetMembers();
        }

        private Member GetMemberObject()
        {
            Member member = null;
            string text = txtMemberId.Text;
            try
            {
                if (text == "")
                {
                    member = new Member
                    {
                        Email = txtEmail.Text,
                        CompanyName = txtCompany.Text,
                        City = txtCity.Text,
                        Country = txtCountry.Text
                    };
                }
                else
                {
                    member = new Member
                    {
                        MemberId = int.Parse(txtMemberId.Text),
                        Email = txtEmail.Text,
                        CompanyName = txtCompany.Text,
                        City = txtCity.Text,
                        Country = txtCountry.Text
                    };
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Product");
            }
            return member;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            ListAllMember();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Member m = GetMemberObject();
                MemberObject memberObject = new MemberObject
                {
                    City = m.City,
                    Country = m.Country,
                    CompanyName = m.CompanyName,
                    Email = m.Email,
                };
                _memberRepository.AddMember(memberObject);
                ListAllMember();
                MessageBox.Show("Add member success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add fail");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Member m = GetMemberObject();
                _memberRepository.DeleteMember(m.MemberId);
                ListAllMember();
                MessageBox.Show("Delete success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete fail");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Member m = GetMemberObject();
                MemberObject memberObject = new MemberObject
                {
                    MemberId = m.MemberId,
                    City = m.City,
                    Country = m.Country,
                    CompanyName = m.CompanyName,
                    Email = m.Email,
                };
                _memberRepository.UpdateMember(memberObject);
                ListAllMember();
                MessageBox.Show("Update member success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update fail");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
