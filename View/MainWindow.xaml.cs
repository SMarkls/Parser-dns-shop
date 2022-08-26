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
using Parser_dns_shop.Model;


namespace Parser_dns_shop.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProductData data;
        public MainWindow()
        {
            InitializeComponent();
            data = new ProductData();
            LinksTextBlock.DataContext = data;
        }

        private void CloseBtnClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BorderMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void AddBtnClick(object sender, RoutedEventArgs e)
        {
            string link = InputTextBox.Text;
            if (!link.Contains("https://") || !link.Contains("dns-shop.ru"))
            {
                MessageBox.Show("Неверная ссылка. Нужно вставлять вмемсте с https://");
                return;
            }
            try
            {
                data.products.Add(link, 0);
            }
            catch (ArgumentException ex)
            {
                if (ex.Message == "Элемент с тем же ключом уже был добавлен.")
                    MessageBox.Show("Эта ссылка уже была добвалена.");
            }
            data.OnPropertyChanged("ProductList");
            InputTextBox.Text = data.ProductList;
        }
    }
}
