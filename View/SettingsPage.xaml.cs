using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Parser_dns_shop.View
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
       private string PathEdge
        {
            get => Properties.Settings.Default.PathToEdge;
            set
            {
                Properties.Settings.Default.PathToEdge = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged("PathEdge");
            }
        }

        private string Frequency
        {
            get
            {
                return Properties.Settings.Default.Frequency;
            }
            set
            {
                if (!int.TryParse(value, out int a))
                {
                    MessageBox.Show("Неверно введено число.");
                    return;
                }
                Properties.Settings.Default.Frequency = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged("Frequency");
            }
        }

        public SettingsPage()
        {
            InitializeComponent();
            FrequencyUpdatingTextBox.DataContext = this;
            FrequencyUpdatingTextBox.Text = Frequency;
            PathEdgeTextBox.DataContext = this;
            PathEdgeTextBox.Text = PathEdge;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            FrequencyUpdatingTextBox.Text = Frequency;
            PathEdgeTextBox.Text = PathEdge;
        }
    }
}
