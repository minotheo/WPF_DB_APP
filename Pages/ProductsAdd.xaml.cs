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
    /// Логика взаимодействия для ProductsAdd.xaml
    /// </summary>
    public partial class ProductsAdd : Page
    {
        public ProductsAdd()
        {
            InitializeComponent();

            Supplier.ItemsSource = DatabaseHandler.DBHandle.ProductSupplier.ToList();
            Category.ItemsSource = DatabaseHandler.DBHandle.ProductCatergory.ToList();
            Unit.ItemsSource = DatabaseHandler.DBHandle.ProductUnit.ToList();
            Manufactuer.ItemsSource = DatabaseHandler.DBHandle.ProductManufacter.ToList();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
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
                    Price.Name.Length <= 0 || Article.Text.Length <= 0)
                {
                    MessageBox.Show("Поля не должны быть пустыми!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var newProduct = new Product
                {
                    ProductSupplier1 = supplier,
                    ProductCatergory = category,
                    ProductUnit1 = unit,
                    ProductManufacter = manufactuer,

                    ProductQuantityInStock = Convert.ToInt32(QuantityInStock.Text),
                    ProductCost = Convert.ToDecimal(Price.Text),
                    ProductDescription = Description.Text,
                    ProductName = Name.Text,
                    ProductArticleNumber = Article.Text
                };

                DatabaseHandler.DBHandle.Product.Add(newProduct);
                DatabaseHandler.DBHandle.SaveChanges();

                MessageBox.Show("Продукт был создан!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

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
