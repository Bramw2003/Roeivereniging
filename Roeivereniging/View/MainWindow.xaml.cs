using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model;
namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var a = Database.GetBoatAll();
            var ReservePage = new ReservePage();
            Frame.Content = ReservePage;
            ReservePage.LbBoats.ItemsSource = a;
            LoginWindow loginWindow = new LoginWindow();

            loginWindow.ShowDialog();
        }
    }
}
