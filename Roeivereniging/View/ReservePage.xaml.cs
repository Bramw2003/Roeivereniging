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
            if (Date?.SelectedDate != null && StartTime.Value != null && EndTime.Value != null)
            {
                var date = (DateTime)Date.SelectedDate;
                List<Boat> boats = Database.GetAvailableBoats(date, (DateTime)StartTime.Value, (DateTime)EndTime.Value, MainWindow.currentMember).Where(x => x.defect == false).ToList();
                if (TbPersons.Text != "")
                {
                    boats = boats.Where(x => x.capacity == int.Parse(TbPersons.Text)).ToList();
                }
                if (CbType.SelectedIndex != 4)
                {
                    boats = boats.Where(x => (int)x.category == CbType.SelectedIndex).ToList();
                }
                if ((bool)ChbSteer.IsChecked)
                {
                    boats = boats.Where(x=>x.steer).ToList();
                }
                if ((bool)ChbScull.IsChecked)
                {
                    boats = boats.Where(x => x.sculling).ToList();
                }
                LbBoats.ItemsSource = boats.Where(x => x.deleted != true);
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
                LbDuur.Content = "Duur: " + endTime.Subtract(startTime).ToString();
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
                if(ReservationViewModel.MakeReservation((Boat)LbBoats.SelectedItem, date, end, MainWindow.currentMember)!=-1)
                {
                    MainWindow.Mail.SendReservationConfirmation(new Reservation(date, end, (Boat)LbBoats.SelectedItem, MainWindow.currentMember));
                }
                else
                {
                    MessageBox.Show("U heeft al 2 reserveringen");
                }
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
