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
using Viewmodel;
using System.Text.RegularExpressions;
using System.Timers;

namespace View {
    /// <summary>
    /// Interaction logic for AddBoatPage.xaml
    /// </summary>
    public partial class AddBoatPage : Page {
        private Timer timer = new Timer(3000);

        public AddBoatPage() {
            InitializeComponent();
            timer.Elapsed += OnTimedEvent;
        }

        /// <summary>
        /// when the add button get clicked checks values and calls view model
        /// </summary>
        private void Add_Boat_Button_Click(object sender, RoutedEventArgs e) {
            int Capacity = int.Parse(tbCapacity.Textbox.Text);
            bool sculing = false;
            bool steer = false;
            string location = tbShed.Textbox.Text + "-" + tbRow.Textbox.Text + "-" + tbColumn.Textbox.Text + "-" + tbHeight.Textbox.Text;
            switch (cbSculling.SelectedIndex) {
                case -1:
                    return;
                case 0:
                    sculing = true;
                    break;
                case 1:
                    sculing = false;
                    break;
            }

            switch (cbSteering.SelectedIndex) {
                case -1:
                    return;
                case 0:
                    steer = true;
                    break;
                case 1:
                    steer = false;
                    break;
            }

            if(BoatViewmodel.AddBoat(tbName.Textbox.Text, cbType.SelectedIndex, Capacity, sculing, steer, location))
            {
                timer.Start();
                Notification.Visibility = Visibility.Visible;
                tbName.Textbox.Text = "";
                cbType.SelectedIndex = 0;
                tbCapacity.Textbox.Text = "";
                cbSculling.SelectedIndex = 0;
                cbSteering.SelectedIndex = 1;
                tbShed.Textbox.Text = "";
                tbRow.Textbox.Text = "";
                tbColumn.Textbox.Text = "";
                tbHeight.Textbox.Text = "";
            }
        }

        /// <summary>
        /// makes sure capacity textbox can only be numbers
        /// </summary>
        private void PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public void OnTimedEvent(object o, EventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                Notification.Visibility = Visibility.Hidden;
            });
            timer.Stop();
        }
    }
}
