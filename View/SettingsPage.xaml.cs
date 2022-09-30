using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Parser_dns_shop.View
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
       private string PathEdge
        {
            get 
            {
                return Properties.Settings.Default.PathToEdge;
            }
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
