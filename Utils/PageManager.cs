using System.Windows;
using System.Windows.Controls;
using WPFKochetov.Database;
using WPFKochetov.Pages;

namespace WPFKochetov.Utils
{
    class PageManager
    {
        public enum PAGES {
            AUTH = 1,
            PRODUCTS_PAGE = 2,
        }

        public static User User;
        public static Frame CurrentFrame;

        public static void SetPage (PAGES page)
        {
            if (User == null) {
                CurrentFrame.Navigate(new AuthPage());
                return;
            }

            switch(page)
            {
                case PAGES.AUTH: {
                    User = null;
                    CurrentFrame.Navigate(new AuthPage());
                    break;
                }

                case PAGES.PRODUCTS_PAGE:
                    CurrentFrame.Navigate(new ProductsPage());
                    break;

                default:
                    MessageBox.Show("Страница не найдена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); ;
                    break;
            }
        }
    }
}
