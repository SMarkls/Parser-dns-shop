using System.Windows;
using System.Windows.Input;
using Parser_dns_shop.Model;
using Parser_dns_shop.ViewModel;

namespace Parser_dns_shop.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataViewModel dataViewModel;
        public MainWindow()
        {
            InitializeComponent();
            ProductData data = new ProductData();
            dataViewModel = new DataViewModel(data);
            LinksTextBlock.DataContext = data;
        }

        private void CloseBtnClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BorderMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void AddBtnClick(object sender, RoutedEventArgs e)
        {
            string link = InputTextBox.Text;
            if (!link.Contains("https://") || !link.Contains("dns-shop.ru"))
            {
                MessageBox.Show("Неверная ссылка. Нужно вставлять вмемсте с https://");
                return;
            }
            if (dataViewModel.ProductsContains(link))
            {
                MessageBox.Show("Эта ссылка уже добавлена.");
                return;
            }
            InputTextBox.Clear();
            StatusLabel.Text = "Ищем цену продукта...";
            dataViewModel.AddProduct(link);
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StatusLabel.Text = "Закрываем...";
            dataViewModel.updater.parser.DisposeDriver();
        }

        private void DelBtnClick(object sender, RoutedEventArgs e)
        {
            string link = InputTextBox.Text;
            if (!link.Contains("https://") || !link.Contains("dns-shop.ru"))
            {
                MessageBox.Show("Неверная ссылка. Нужно вставлять вмемсте с https://");
                return;
            }
            if (!dataViewModel.ProductsContains(link))
            {
                MessageBox.Show("Данной ссылки нет");
                return;
            }
            dataViewModel.DelProduct(link);
        }
    }
}
