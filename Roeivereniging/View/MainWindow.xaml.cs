using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Extensions.Configuration;
using Model;
using Viewmodel;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Member currentMember { get; set; }
        public static IMail Mail;
        private ReservePage ReservePage;
        private ViewReservationsPage ViewReservationsPage;
        private AdminPage AdminPage;
        private DefectsPage DefectsPage;
        private ExaminatorsPage ExaminatorsPage;
        private AccountPage AccountPage;
        public static Timer logOutTimer;
        IConfigurationRoot configuration;
        public MainWindow()
        {
            try
            {
                configuration = new ConfigurationBuilder()
                                        .AddUserSecrets<App>()
                                        .Build();
                Mail = new Model.DAO.Mail(configuration);

            }
            catch (Exception ex)
            {
                configuration = new ConfigurationBuilder().Build();
                Mail = new Model.DAO.NoMail();
                MessageBox.Show("Geen mail mogelijk... Stel user secrets in");
            }
            logOutTimer = new Timer(300000); //300000ms == 5m
            logOutTimer.Enabled = true;
            logOutTimer.Elapsed += LogOutTimer_Elapsed;
            InitializeComponent();
        }

        private void LogOutTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Logout();
            });
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
            currentMember = MemberViewModel.GetByUsername("admin");
#endif
            // More Anti-Cheese
            if (currentMember == null) { this.Close(); return; }
            ResetPages();

            var a = BoatViewmodel.GetAllBoats();
            Frame.Content = ReservePage;
            ReservePage.LbBoats.ItemsSource = a;
            AddHeaderBtns();
        }

        private void AddHeaderBtns()
        {
            // Add default header buttons like reserve and history
            Button ReserveBtn = new Button();
            ReserveBtn.Content = "Reserveer";
            ReserveBtn.MinWidth = 120;
            ReserveBtn.Margin = new Thickness(10, 10, 0, 0);
            ReserveBtn.Click += BtnReserveer_Click;
            ReserveBtn.FontSize = 16;
            this.Header.Children.Add(ReserveBtn);
            Button HistoryBtn = new Button();
            HistoryBtn.Content = "Geschiedenis";
            HistoryBtn.MinWidth = 120;
            HistoryBtn.Margin = new Thickness(10, 10, 0, 0);
            HistoryBtn.Click += BtnReserveringen_Click;
            HistoryBtn.FontSize = 16;
            this.Header.Children.Add(HistoryBtn);

            // Add user specific buttons
            if (currentMember.IsAdmin())
            {
                Button adminButton = new Button();
                adminButton.Content = "admin";
                adminButton.MinWidth = 120;
                adminButton.Margin = new Thickness(10, 10, 0, 0);
                adminButton.Click += AdminButton_Click;
                adminButton.FontSize = 16;
                this.Header.Children.Add(adminButton);
            }
            if (currentMember.IsRepair())
            {
                Button defectsBtn = new Button();
                defectsBtn.Content = "Defecte boten";
                defectsBtn.MinWidth = 120;
                defectsBtn.Margin = new Thickness(10, 10, 0, 0);
                defectsBtn.Click += DefectsButton_Click;
                defectsBtn.FontSize = 16;
                this.Header.Children.Add(defectsBtn);
            }
            if (currentMember.IsExaminator())
            {
                Button examBtn = new Button();
                examBtn.Content = "Examinator";
                examBtn.MinWidth = 120;
                examBtn.Margin = new Thickness(10, 10, 0, 0);
                examBtn.Click += ExaminatorButton_Click;
                examBtn.FontSize = 16;
                this.Header.Children.Add(examBtn);
            }

        }

        public void ResetPages()
        {
            ReservePage = new ReservePage();
            ViewReservationsPage = new ViewReservationsPage();
            AdminPage = new AdminPage();
            DefectsPage = new DefectsPage();
            ExaminatorsPage = new ExaminatorsPage();
            AccountPage = new AccountPage(this);
        }

        public void Logout()
        {
            logOutTimer.Stop();
            currentMember = null;
            LoginWindow loginWindow = new LoginWindow();
            // Anti-Cheese
            if (!(bool)loginWindow.ShowDialog())
            {
                this.Close();
            }
            // More Anti-Cheese
            if (currentMember == null) { this.Close(); return; }

            ResetPages();
            Frame.Content = ReservePage;

            // Clear the header
            Header.Children.Clear();

            AddHeaderBtns();
        }
        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = AdminPage;
        }

        private void BtnReserveringen_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = ViewReservationsPage;
        }

        private void BtnReserveer_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = ReservePage;
        }
        private void DefectsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = DefectsPage;
        }
        private void ExaminatorButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = ExaminatorsPage;
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            Logout();
        }

        private void BtnAccount_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = AccountPage;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            logOutTimer.Stop();
            logOutTimer.Dispose();
            logOutTimer = new Timer(300000);
            logOutTimer.Elapsed += LogOutTimer_Elapsed;
            logOutTimer.Start();
        }
    }
}
