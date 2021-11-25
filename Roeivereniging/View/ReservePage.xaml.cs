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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;

namespace View
{
    /// <summary>
    /// Interaction logic for ReservePage.xaml
    /// </summary>
    public partial class ReservePage : Page
    {
        public ReservePage()
        {
            InitializeComponent();
            EndTime.IsEnabled = false;
            Date.IsEnabled = false;
        }

        private void StartTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            EndTime.IsEnabled = true;
            EndTime.Minimum = (DateTime)e.NewValue;
            EndTime.Maximum = ((DateTime)e.NewValue).AddHours(6);
            EndTime.ClipValueToMinMax = true;
            UpdateLbBoats();
        }

        private void EndTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Date.IsEnabled = true;
            Date.DisplayDateEnd = DateTime.Now.AddDays(5);
            UpdateLbBoats();
        }


        private void Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateLbBoats();
        }
        public void UpdateLbBoats()
        {
            if (Date.SelectedDate != null && StartTime.Value != null && EndTime.Value != null)
            {
                var date = (DateTime)Date.SelectedDate;
                LbBoats.ItemsSource = Database.GetAvailableBoats(date, (DateTime)StartTime.Value, (DateTime)EndTime.Value);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Date.SelectedDate != null && StartTime.Value != null && EndTime.Value != null && LbBoats.SelectedItem != null)
            {
                DateTime startTime = (DateTime)StartTime.Value.Value;
                DateTime endTime = (DateTime)EndTime.Value.Value;

                TimeSpan a = new TimeSpan(startTime.Hour,startTime.Minute, startTime.Second);
                DateTime date = (DateTime)Date.SelectedDate.Value.Date + a;
                TimeSpan b = new TimeSpan(endTime.Hour, endTime.Minute, endTime.Second);
                DateTime end = (DateTime)Date.SelectedDate.Value.Date + b;
                MessageBox.Show(Model.DAO.Reservation.ReserveBoat((Boat)LbBoats.SelectedItem, date, end, MainWindow.currentMember).ToString());
            }
        }
    }
}
