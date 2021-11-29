﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        public static Member currentMember { get; set; }
        private ReservePage ReservePage = new ReservePage();
        private History HistoryPage = new History();
        private ViewReservationsPage ViewReservationsPage = new ViewReservationsPage();
        private AddUserPage ViewAddUserPage = new AddUserPage();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
#if !DEBUG
            LoginWindow loginWindow = new LoginWindow();
            // Anti-Cheese
            if (!(bool)loginWindow.ShowDialog())
            {
                this.Close();
            }
#else
            currentMember = Model.DAO.Member.GetByUsername("admin");
#endif
            // More Anti-Cheese
            if(currentMember == null){this.Close();}

            var a = Model.DAO.Boat.GetBoatAll();
            Frame.Content = ReservePage;
            ReservePage.LbBoats.ItemsSource = a;

            if (currentMember.IsAdmin())
            {
                Button adminButton = new Button();
                adminButton.Content = "admin";
                adminButton.MinWidth = 100;
                adminButton.Margin = new Thickness(10, 0, 0, 0);
                adminButton.Click += AdminButton_Click;
                this.Header.Children.Add(adminButton);
            }
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = ViewAddUserPage;
        }

        private void BtnReserveringen_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = ViewReservationsPage;
            //ViewReservationsPage.LvRervations.ItemsSource = Model.DAO.Reservation.GetAllByMember(MainWindow.currentMember).OrderByDescending(x => x.date);

        }

        private void BtnReserveer_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = ReservePage;
        }
    }
}
