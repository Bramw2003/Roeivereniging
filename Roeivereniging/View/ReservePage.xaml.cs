using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Viewmodel;

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
            StartTime.Minimum = DateTime.Now;
            EndTime.Minimum = DateTime.Now;
            Date.DisplayDateStart = DateTime.Now;
            Date.DisplayDateEnd = DateTime.Now.AddDays(5);
        }

        /// <summary>
        /// Update the listbox showing all available boats in the given time frame
        /// </summary>
        public void UpdateLbBoats()
        {
            if (Date?.SelectedDate != null && StartTime.Value != null && EndTime.Value != null && TbPersons.Text != "")
            {
                var date = (DateTime)Date.SelectedDate;
                LbBoats.ItemsSource = Database.GetAvailableBoats(date, (DateTime)StartTime.Value, (DateTime)EndTime.Value, (BoatType)CbType.SelectedIndex, int.Parse(TbPersons.Text), (bool)ChbSteer.IsChecked, (bool)ChbScull.IsChecked).Where(x => x.defect == false);
            }
        }

        /// <summary>
        /// Update the duration label
        /// </summary>
        private void UpdateDuration()
        {
            if (StartTime.Value != null && EndTime.Value != null)
            {
                DateTime startTime = (DateTime)StartTime.Value.Value;
                DateTime endTime = (DateTime)EndTime.Value.Value;
                LbDuur.Content = "Duur: " + (endTime - startTime).Hours + ":" + (endTime - startTime).Minutes;
            }
        }

        private void StartTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            EndTime.Minimum = (DateTime)e.NewValue;
            EndTime.Maximum = ((DateTime)e.NewValue).AddHours(6);
            EndTime.ClipValueToMinMax = true;
            UpdateDuration();
            UpdateLbBoats();
        }

        private void EndTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            UpdateDuration();
            UpdateLbBoats();
        }


        private void Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateTime.Now.Date != Date.SelectedDate.Value.Date)
            {
                StartTime.Minimum = DateTime.MinValue;
                EndTime.Maximum = DateTime.MaxValue;
            }
            else
            {
                StartTime.Minimum = DateTime.Now;
                EndTime.Minimum = DateTime.Now;
            }
            UpdateLbBoats();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Date.SelectedDate != null && StartTime.Value != null && EndTime.Value != null && LbBoats.SelectedItem != null)
            {
                DateTime startTime = (DateTime)StartTime.Value.Value;
                DateTime endTime = (DateTime)EndTime.Value.Value;

                TimeSpan a = new TimeSpan(startTime.Hour, startTime.Minute, startTime.Second);
                DateTime date = (DateTime)Date.SelectedDate.Value.Date + a;
                TimeSpan b = new TimeSpan(endTime.Hour, endTime.Minute, endTime.Second);
                DateTime end = (DateTime)Date.SelectedDate.Value.Date + b;
                ReservationViewModel.MakeReservation((Boat)LbBoats.SelectedItem, date, end, MainWindow.currentMember).ToString();
            }
        }

        /// <summary>
        /// Validate the input of a textbox to only allow numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateLbBoats();
        }

        private void TbPersons_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateLbBoats();
        }

        private void ChbSteer_Click(object sender, RoutedEventArgs e)
        {
            UpdateLbBoats();

        }

        private void ChbScull_Click(object sender, RoutedEventArgs e)
        {
            UpdateLbBoats();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
