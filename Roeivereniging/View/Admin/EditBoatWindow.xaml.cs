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
using Model;

namespace View.Admin
{
    /// <summary>
    /// Interaction logic for EditBoatWindow.xaml
    /// </summary>
    public partial class EditBoatWindow : Window
    {
        BoatViewmodel model;
        Boat Boat;
        public EditBoatWindow(BoatViewmodel boatVM, Boat boat)
        {
            InitializeComponent();
            model = boatVM;
            Boat = boat;
            tbName.Textbox.Text = boat.name;
            cbType.SelectedIndex = (int)boat.category;
            tbCapacity.Textbox.Text = boat.capacity.ToString();
            if (boat.sculling)
            {
                cbSculling.SelectedIndex = 0;
            }
            else
            {
                cbSculling.SelectedIndex = 1;
            }
            if (boat.steer)
            {
                cbSteering.SelectedIndex = 0;
            }
            else
            {
                cbSteering.SelectedIndex = 1;
            }
            tbShed.Textbox.Text = boat.shed.ToString();
            tbRow.Textbox.Text = boat.row.ToString();
            tbColumn.Textbox.Text = boat.Column.ToString();
            tbHeight.Textbox.Text = boat.Height.ToString();

        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Edit_Boat_Button_Click(object sender, RoutedEventArgs e)
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
            try
            {
                model.CheckShedSpace(location, Boat.id);
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            model.EditBoat(Boat, tbName.Textbox.Text, cbType.SelectedIndex, Capacity, sculing, steer, location);
            this.Close();
        }
    }
}
