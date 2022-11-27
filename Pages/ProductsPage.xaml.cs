using System;
using System.Linq;
using System.Windows;
using WPFKochetov.Database;
using System.Windows.Controls;
using System.Collections.Generic;
using WPFKochetov.Utils;

namespace WPFKochetov.Pages
{
    public partial class ProductsPage : Page
    {
        private int _selectedSort = -1;
        private int _selectedFilter = -1;

        private string _searchText = "";

        private readonly string[] _sortParams = 
            { "Названию", "Описанию", "Цене", "Производителю" };

        private readonly List<ProductManufacter> _manufacturers =
            DatabaseHandler.DBHandle.ProductManufacter.ToList();

        public ProductsPage()
        {
            InitializeComponent();

            LoadFields();
            FormatList();
            IsAdminToolsAllowed();
        }

        private void FormatList ()
        {
            var products = DatabaseHandler.DBHandle.Product.ToList();

            if(_selectedFilter != -1)
                products = products.FindAll(x => x.ProductManufacter.ManufacterID == _manufacturers[_selectedFilter].ManufacterID);

            switch (_selectedSort)
            {
                case 0: products = products.OrderBy(x => x.ProductName).ToList();
                    break;
                case 1: products = products.OrderByDescending(x => x.ProductName).ToList();
                    break;
                case 2: products = products.OrderBy(x => x.ProductDescription).ToList();
                    break;
                case 3: products = products.OrderByDescending(x => x.ProductDescription).ToList();
                    break;
                case 4: products = products.OrderBy(x => x.ProductCost).ToList();
                    break;
                case 5: products = products.OrderByDescending(x => x.ProductCost).ToList();
                    break;
                case 6: products = products.OrderBy(x => x.ProductManufacter.ManufacterName).ToList();
                    break;
                case 7: products = products.OrderByDescending(x => x.ProductManufacter.ManufacterName).ToList();
                    break;
            }

            if (!_searchText.Equals(""))
                products = products.FindAll(x => 
                    x.ProductName.Contains(_searchText) ||
                    x.ProductDescription.Contains(_searchText) ||
                    x.ProductCost.ToString().Contains(_searchText) ||
                    x.ProductManufacter.ManufacterName.Contains(_searchText)
                );

            ListView.ItemsSource = products;
        }

        private void IsAdminToolsAllowed()
        {
            AdminTools.Visibility = PageManager.User.UserRole == 3 ? Visibility.Visible : Visibility.Hidden;
        }

        private void LoadFields()
        {
            Filter.ItemsSource = _manufacturers;

            foreach (var sortParam in _sortParams)
            {
                Sorter.Items.Add($"По возрастанию {sortParam}");
                Sorter.Items.Add($"По убыванию {sortParam}");
            }
        }
        private void Filter_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            if(comboBox == null)
                return;

            _selectedFilter = comboBox.SelectedIndex;

            FormatList();
        }

        private void Sorter_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            if (comboBox == null)
                return;

            _selectedSort = comboBox.SelectedIndex;

            FormatList();
        }

        private void SearchBox_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox == null)
                return;

            _searchText = textBox.Text;

            FormatList();
        }

        private void RemoveFilter_OnClick(object sender, RoutedEventArgs e)
        {
            _selectedFilter = -1;

            Filter.SelectedIndex = -1;

            FormatList();
        }

        private void RemoveSorter_OnClick(object sender, RoutedEventArgs e)
        {
            _selectedSort = -1;

            Sorter.SelectedIndex = -1;

            FormatList();
        }

        private void CreateItem_OnClick(object sender, RoutedEventArgs e)
        {
            PageManager.CurrentFrame.Navigate(new ProductsAdd());
        }

        private void EditItem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedProduct = DatabaseHandler.DBHandle.Product.ToList()[ListView.SelectedIndex];

                if (selectedProduct == null)
                {
                    MessageBox.Show("Вы не выбрали элемент для редактирования!", "Ошибка", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                PageManager.CurrentFrame.Navigate(new ProductsEdit(selectedProduct));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Вы не выбрали элемент для редактирования!", "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void RemoveItem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var listViewSelectedItem in ListView.SelectedItems)
                {
                    var selectedProduct = listViewSelectedItem as Product;

                    if (selectedProduct == null)
                    {
                        MessageBox.Show("Вы не выбрали элемент для удаления!", "Ошибка", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return;
                    }

                    DatabaseHandler.DBHandle.Product.Remove(selectedProduct);
                }

                DatabaseHandler.DBHandle.SaveChanges();

                MessageBox.Show("Вы удалили данный(е) элемент(ы)!", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Вы не выбрали элемент для удаления!", "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void GoBack_OnClick(object sender, RoutedEventArgs e)
        {
            PageManager.CurrentFrame.Navigate(new AuthPage());
        }
    }
}
