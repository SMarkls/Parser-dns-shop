﻿using System;
using System.Windows;
using System.Windows.Forms;
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
        NotifyIcon notify;
        WindowState state = WindowState.Normal;

        public MainWindow()
        {
            InitializeComponent();
            ProductData data = new ProductData();
            dataViewModel = new DataViewModel(data);
            LinksTextBlock.DataContext = data;
            LinksTextBlock.TextChanged += (a, e) => StatusLabel.Text = "Главная страница";


            notify = new NotifyIcon();
            notify.BalloonTipText = "Свёрнуто в трей. Для раскрытия нажмите на иконку.";
            notify.BalloonTipTitle = "Парсер DNS";
            notify.Text = "Парсер DNS";
            notify.Click += NotifyClicked;
            notify.Icon = Properties.Resources.NotifyLogo;
        }

        private void NotifyClicked(object sender, EventArgs e)
        {
            Show();
            WindowState = state;
        }

        private async void CloseBtnClicked(object sender, RoutedEventArgs e)
        {
            StatusLabel.Text = "Закрываем...";
            await System.Threading.Tasks.Task.Delay(100);
            Close();
        }

        private void BorderMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) DragMove();
        }

        private void AddBtnClick(object sender, RoutedEventArgs e)
        {
            string link = InputTextBox.Text;
            if (!link.Contains("https://") || !link.Contains("dns-shop.ru"))
            {
                System.Windows.MessageBox.Show("Неверная ссылка. Нужно вставлять вмемсте с https://");
                return;
            }
            if (dataViewModel.ProductsContains(link))
            {
                System.Windows.MessageBox.Show("Эта ссылка уже добавлена.");
                return;
            }
            InputTextBox.Clear();
            StatusLabel.Text = "Ищем цену продукта...";
            dataViewModel.AddProduct(link);
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dataViewModel.updater.parser.DisposeDriver();
            notify.Dispose();
        }

        private void DelBtnClick(object sender, RoutedEventArgs e)
        {
            string link = InputTextBox.Text;
            if (!link.Contains("https://") || !link.Contains("dns-shop.ru"))
            {
                System.Windows.MessageBox.Show("Неверная ссылка. Нужно вставлять вмемсте с https://");
                return;
            }
            if (!dataViewModel.ProductsContains(link))
            {
                System.Windows.MessageBox.Show("Данной ссылки нет");
                return;
            }
            dataViewModel.DelProduct(link);
        }

        private void SettingsBtnClicked(object sender, RoutedEventArgs e)
        {
            StatusLabel.Text = StatusLabel.Text == "Настройки" ? "Главная страница" : "Настройки";
            LinksTextBlock.Visibility = LinksTextBlock.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            settings = settings ?? new SettingsPage();
            Frame.Content = Frame.Content == null ? settings : null;
        }

        private void OnStateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
                if (notify != null)
                    notify.ShowBalloonTip(1000);
            }
            else
                state = WindowState;
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (notify != null)
                    notify.Visible = !IsVisible;
            }
            catch (NullReferenceException)
            {

            }
        }

        private void MinimizeBtnClicked(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
