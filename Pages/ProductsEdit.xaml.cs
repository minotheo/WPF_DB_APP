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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFKochetov.Database;
using WPFKochetov.Utils;

namespace WPFKochetov.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductsEdit.xaml
    /// </summary>
    public partial class ProductsEdit : Page
    {
        private Product _editedProduct;

        public ProductsEdit(Product product)
        {
            InitializeComponent();

            Unit.ItemsSource = DatabaseHandler.DBHandle.ProductUnit.ToList();
            Supplier.ItemsSource = DatabaseHandler.DBHandle.ProductSupplier.ToList();
            Category.ItemsSource = DatabaseHandler.DBHandle.ProductCatergory.ToList();
            Manufactuer.ItemsSource = DatabaseHandler.DBHandle.ProductManufacter.ToList();

            _editedProduct = product;

            QuantityInStock.Text = _editedProduct.ProductQuantityInStock.ToString();
            Price.Text = _editedProduct.ProductCost.ToString();
            Description.Text = _editedProduct.ProductDescription;
            Name.Text = _editedProduct.ProductName;

            Supplier.SelectedIndex = DatabaseHandler.DBHandle.ProductSupplier.ToList()
                .IndexOf(_editedProduct.ProductSupplier1);
            Category.SelectedIndex = DatabaseHandler.DBHandle.ProductCatergory.ToList()
                .IndexOf(_editedProduct.ProductCatergory);
            Unit.SelectedIndex = DatabaseHandler.DBHandle.ProductUnit.ToList()
                .IndexOf(_editedProduct.ProductUnit1);
            Manufactuer.SelectedIndex = DatabaseHandler.DBHandle.ProductManufacter.ToList()
                .IndexOf(_editedProduct.ProductManufacter);
        }

        private void Edit_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var supplier = DatabaseHandler.DBHandle.ProductSupplier.ToList()[Supplier.SelectedIndex];
                var category = DatabaseHandler.DBHandle.ProductCatergory.ToList()[Category.SelectedIndex];
                var unit = DatabaseHandler.DBHandle.ProductUnit.ToList()[Unit.SelectedIndex];
                var manufactuer = DatabaseHandler.DBHandle.ProductManufacter.ToList()[Manufactuer.SelectedIndex];

                if (supplier == null || category == null || unit == null || manufactuer == null)
                {
                    MessageBox.Show("Поля не должны быть пустыми!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (QuantityInStock.Text.Length <= 0 || Price.Text.Length <= 0 || Description.Text.Length <= 0 ||
                    Price.Name.Length <= 0)
                {
                    MessageBox.Show("Поля не должны быть пустыми!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                _editedProduct.ProductSupplier1 = supplier;
                _editedProduct.ProductCatergory = category;
                _editedProduct.ProductUnit1 = unit;
                _editedProduct.ProductManufacter = manufactuer;

                _editedProduct.ProductQuantityInStock = Convert.ToInt32(QuantityInStock.Text);
                _editedProduct.ProductCost = Convert.ToDecimal(Price.Text);
                _editedProduct.ProductDescription = Description.Text;
                _editedProduct.ProductName = Name.Text;
                
                DatabaseHandler.DBHandle.SaveChanges();

                MessageBox.Show("Продукт был изменен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                PageManager.SetPage(PageManager.PAGES.PRODUCTS_PAGE);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Поля не должны быть пустыми!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GoBack_OnClick(object sender, RoutedEventArgs e)
        {
            PageManager.SetPage(PageManager.PAGES.PRODUCTS_PAGE);
        }
    }
}
