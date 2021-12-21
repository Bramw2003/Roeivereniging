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
        private Timer timer = new Timer(3000);
        public DeleteBoatPage() {
            InitializeComponent();
            _BoatViewModel = (BoatViewmodel)base.DataContext;
            timer.Elapsed += OnTimedEvent;
        }

        private void Delete_Boat_Button_Click(object sender, RoutedEventArgs e) {
            _BoatViewModel.DeleteBoat((Model.Boat)dataGrid.SelectedItem);
            Notification.Visibility = Visibility.Visible;
            timer.Start();
        }

        public void OnTimedEvent(object o, EventArgs e) {
            this.Dispatcher.Invoke(() => {
                Notification.Visibility = Visibility.Hidden;
            });
            timer.Stop();
        }
    }
}
