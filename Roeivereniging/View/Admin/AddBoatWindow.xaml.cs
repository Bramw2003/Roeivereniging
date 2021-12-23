using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Viewmodel;

namespace View.Admin
{
    /// <summary>
    /// Interaction logic for AddBoatWindow.xaml
    /// </summary>
    public partial class AddBoatWindow : Window
    {
        public AddBoatWindow()
        {
            InitializeComponent();
        }

        private void Add_Boat_Button_Click(object sender, RoutedEventArgs e)
        {
            int Capacity = int.Parse(tbCapacity.Textbox.Text);
            bool sculing = false;
            bool steer = false;
            string location = tbShed.Textbox.Text + "-" + tbRow.Textbox.Text + "-" + tbColumn.Textbox.Text + "-" + tbHeight.Textbox.Text;
            switch (cbSculling.SelectedIndex)
            {
                case -1:
                    return;
                case 0:
                    sculing = true;
                    break;
                case 1:
                    sculing = false;
                    break;
            }

            switch (cbSteering.SelectedIndex)
            {
                case -1:
                    return;
                case 0:
                    steer = true;
                    break;
                case 1:
                    steer = false;
                    break;
            }

            BoatViewmodel.AddBoat(tbName.Textbox.Text, cbType.SelectedIndex, Capacity, sculing, steer, location);
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
