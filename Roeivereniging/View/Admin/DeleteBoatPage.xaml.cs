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

namespace View {
    /// <summary>
    /// Interaction logic for DeleteBoatPage.xaml
    /// </summary>
    public partial class DeleteBoatPage : Page {

        BoatViewmodel _BoatViewModel;
        public DeleteBoatPage() {
            InitializeComponent();
            _BoatViewModel = (BoatViewmodel)base.DataContext;
        }

        private void Delete_Boat_Button_Click(object sender, RoutedEventArgs e) {
            _BoatViewModel.DeleteBoat((Model.Boat)dataGrid.SelectedItem);
        }

        private void Edit_Boat_Button_Click(Object sender, RoutedEventArgs e) {
            var EditBoatWindow= new Admin.EditBoatWindow(_BoatViewModel,(Model.Boat)dataGrid.SelectedItem);
            EditBoatWindow.ShowDialog();
        }
    }
}
