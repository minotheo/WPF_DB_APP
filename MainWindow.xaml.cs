using System.IO;
using System.Linq;
using System.Windows;
using WPFKochetov.Database;
using WPFKochetov.Utils;

namespace WPFKochetov
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
 
            DatabaseHandler.DBHandle = new programmer_10Entities();

            loadImages();

            PageManager.CurrentFrame = MainFrame;
            PageManager.SetPage(PageManager.PAGES.AUTH);
        }

        private void loadImages()
        {
            DatabaseHandler.DBHandle.Product.ToList().ForEach(product => { if (product.ProductImageLink == null) return;
                product.ImageBitmap = File.ReadAllBytes("./Images/Products/" + product.ProductImageLink);});
        }
    }
}
