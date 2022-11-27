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
using WPFKochetov.Utils;

namespace WPFKochetov.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void authButton_Click(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text == "" || passwordBox.Text == "")
                return;

            var user = Database.DatabaseHandler.DBHandle.User.FirstOrDefault(
                x => x.UserPassword == passwordBox.Text.ToString() && x.UserLogin == loginBox.Text.ToString());

            if (user == null)
            {
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show($"Здраствуйте, {user.UserName}, вы успешно авторизовались", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            PageManager.User = user;
            PageManager.SetPage(PageManager.PAGES.PRODUCTS_PAGE);
        }
    }
}
