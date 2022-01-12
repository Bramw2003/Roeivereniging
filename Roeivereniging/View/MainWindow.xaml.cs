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
using Renci.SshNet;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Member currentMember { get; set; }
        public static IMail Mail;
        public static ReservePage ReservePage;
        private ViewReservationsPage ViewReservationsPage;
        private AdminPage AdminPage;
        private DefectsPage DefectsPage;
        private ExaminatorsPage ExaminatorsPage;
        private AccountPage AccountPage;
        private EventsPage EventsPage;
        public static Timer logOutTimer;
        IConfigurationRoot configuration;
        SshClient sshClient;
        ForwardedPortLocal portFwld;
        public MainWindow()
        {
            PasswordConnectionInfo connectionInfo = new PasswordConnectionInfo("145.44.233.142", "student", "#7mBzd*EN");
            connectionInfo.Timeout = TimeSpan.FromSeconds(30);
            sshClient = new SshClient(connectionInfo);
            sshClient.Connect();
            portFwld = new ForwardedPortLocal("127.0.0.1", Convert.ToUInt32(1433), "localhost", Convert.ToUInt32(1433)); sshClient.AddForwardedPort(portFwld);
            portFwld.Start();
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
            //var b = new EventsManageBoat(a,new List<Boat>());
            //b.ShowDialog();
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
            HistoryBtn.Content = "Reserveringen";
            HistoryBtn.MinWidth = 120;
            HistoryBtn.Margin = new Thickness(10, 10, 0, 0);
            HistoryBtn.Click += BtnReserveringen_Click;
            HistoryBtn.FontSize = 16;
            this.Header.Children.Add(HistoryBtn);
            Button EventsBtn = new Button();
            EventsBtn.Content = "Evenementen";
            EventsBtn.MinWidth = 120;
            EventsBtn.Margin = new Thickness(10, 10, 0, 0);
            EventsBtn.FontSize = 16;
            EventsBtn.Click += BtnEvents_Click;
            this.Header.Children.Add(EventsBtn);

            // Add user specific buttons
            if (currentMember.IsAdmin())
            {
                Button adminButton = new Button();
                adminButton.Content = "Admin";
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
            EventsPage = new EventsPage();
        }

        public void Logout()
        {
            logOutTimer.Stop();
            currentMember = null;
            LoginWindow loginWindow = new LoginWindow();
            // Anti-Cheese
            Frame.Content = ReservePage;
            if (!(bool)loginWindow.ShowDialog())
            {
                this.Close();
            }
            // More Anti-Cheese
            if (currentMember == null) { this.Close(); return; }

            ResetPages();

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

        public void BtnReserveer_Click(object sender, RoutedEventArgs e)
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

        private void BtnEvents_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = EventsPage;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            sshClient.Disconnect();
            sshClient.Dispose();
        }
    }
}
