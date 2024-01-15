using BusinessObject;
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
    /// Interaction logic for AdminProductManagement.xaml
    /// </summary>
    public partial class AdminProductManagement : Window
    {
        private readonly IProductRepository _productRepository;
        public AdminProductManagement(IProductRepository productRepository)
        {
            InitializeComponent();
            _productRepository = productRepository;
            ListAllProducts();
        }

        public void ListAllProducts()
        {
            lvProducts.ItemsSource = _productRepository.GetProducts();
        }

        private Product GetProductObject()
        {
            Product product = null;
            string text = txtProductId.Text;
            try
            {
                if (text == "")
                {
                    product = new Product
                    {
                        CategoryId = int.Parse(txtCategoryId.Text),
                        ProductName = txtProductName.Text,
                        Weight = txtWeight.Text,
                        UnitPrice = decimal.Parse(txtPrice.Text),
                        UnitInStock = int.Parse(txtStock.Text)
                    };
                }
                else
                {
                    product = new Product
                    {
                        ProductId = int.Parse(txtProductId.Text),
                        CategoryId = int.Parse(txtCategoryId.Text),
                        ProductName = txtProductName.Text,
                        Weight = txtWeight.Text,
                        UnitPrice = decimal.Parse(txtPrice.Text),
                        UnitInStock = int.Parse(txtStock.Text)
                    };
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Product");
            }
            return product;
        }


        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            ListAllProducts();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product p = GetProductObject();
                ProductObject productObject = new ProductObject
                {
                    CategoryId = p.CategoryId,
                    ProductName = p.ProductName,
                    Weight = p.Weight,
                    UnitPrice = p.UnitPrice,
                    UnitsInStock = p.UnitInStock
                };
                _productRepository.AddProduct(productObject);
                ListAllProducts();
                MessageBox.Show("Add product success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add fail");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product p = GetProductObject();
                ProductObject productObject = new ProductObject
                {
                    ProductId = p.ProductId,
                    CategoryId = p.CategoryId,
                    ProductName = p.ProductName,
                    Weight = p.Weight,
                    UnitPrice = p.UnitPrice,
                    UnitsInStock = p.UnitInStock
                };
                _productRepository.UpdateProduct(productObject);
                ListAllProducts();
                MessageBox.Show("Update success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update fail");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product p = GetProductObject();
                _productRepository.DeleteProduct(p.ProductId);
                ListAllProducts();
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

        private void cboSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtSearch.Text = string.Empty;
            ListAllProducts();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<ProductObject> list;

            string search = txtSearch.Text.Trim();
            string searchBy = (cboSearch.SelectedItem as ComboBoxItem).Content.ToString();
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    ListAllProducts();
                    return;
                }

                switch (searchBy)
                {
                    case "Id":
                        if (int.TryParse(search, out int productId))
                        {
                            ProductObject product = _productRepository.GetProduct(productId);
                            if (product != null)
                            {
                                list = new List<ProductObject> { product };
                            }
                            else
                            {
                                MessageBox.Show("Product not found.", "Search Product");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid Id format. Please enter a valid integer.", "Search Product");
                            return;
                        }
                        break;

                    case "Product Name":
                        list = _productRepository.SearchByProductName(search);
                        break;

                    default:
                        if (int.TryParse(search, out int unitsInStock))
                        {
                            list = _productRepository.SearchByUnitsInStock(unitsInStock);
                        }
                        else
                        {
                            MessageBox.Show("Invalid Units in stock format. Please enter a valid integer.", "Search Product");
                            return;
                        }
                        break;
                }

                lvProducts.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Search Product");
            }
        }
    }
}
