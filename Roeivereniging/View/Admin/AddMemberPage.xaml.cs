using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Viewmodel;

namespace View
{
    /// <summary>
    /// Interaction logic for AddMemberPage.xaml
    /// </summary>
    public partial class AddMemberPage : Page
    {

        private Timer timer = new Timer(3000);
        public AddMemberPage()
        {
            InitializeComponent();
            timer.Elapsed += OnTimedEvent;
        }

        private void Add_Member_Button_Click(object sender, RoutedEventArgs e) {
            if(MemberViewModel.MakeUser(tbName.Text,tbUserName.Text, (DateTime)dpBirthDay.SelectedDate, tbEMail.Text, tbPassWord.Text)) {
                Notification.Visibility = Visibility.Visible;
                tbName.Text = "";
                tbUserName.Text = "";
                dpBirthDay.SelectedDate = null;
                tbEMail.Text = "";
                tbPassWord.Text = "";
                timer.Start();
            }
        }

        public void OnTimedEvent(object o, EventArgs e) {
            this.Dispatcher.Invoke(() =>
            {
                Notification.Visibility = Visibility.Hidden;
            });
            timer.Stop();
        }
    }
}
