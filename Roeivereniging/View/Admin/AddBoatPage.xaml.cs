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

namespace View {
    /// <summary>
    /// Interaction logic for AddBoatPage.xaml
    /// </summary>
    public partial class AddBoatPage : Page {
        public AddBoatPage() {
            InitializeComponent();
        }

        /// <summary>
        /// when the add button get clicked checks values and calls view model
        /// </summary>
        private void Add_Boat_Button_Click(object sender, RoutedEventArgs e) {
            int Capacity = int.Parse(tbCapacity.Text);
            bool sculing = false;
            bool steer = false;
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

            BoatViewmodel.AddBoat(tbName.Text, cbType.SelectedIndex , Capacity, sculing, steer);
        }

        /// <summary>
        /// makes sure capacity textbox can only be numbers
        /// </summary>
        private void PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
