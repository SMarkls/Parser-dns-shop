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
        SettingsPage settings;
        public MainWindow()
        {
            InitializeComponent();
            ProductData data = new ProductData();
            dataViewModel = new DataViewModel(data);
            LinksTextBlock.DataContext = data;
            LinksTextBlock.TextChanged += (a, e) => StatusLabel.Text = "Главная страница";
        }

        private async void CloseBtnClicked(object sender, RoutedEventArgs e)
        {
            StatusLabel.Text = "Закрываем...";
            await System.Threading.Tasks.Task.Delay(100);
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

        private void SettingsBtnClicked(object sender, RoutedEventArgs e)
        {
            StatusLabel.Text = StatusLabel.Text == "Настройки" ? "Главная страница" : "Настройки";
            LinksTextBlock.Visibility = LinksTextBlock.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            settings = settings == null ? new SettingsPage() : settings;
            Frame.Content = Frame.Content == null ? settings : null;
            //if (settings != Properties.Settings.Default.PathToEdge)

        }
    }
}
