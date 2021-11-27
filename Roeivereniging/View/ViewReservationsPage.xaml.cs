using Model;
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

namespace View
{
    /// <summary>
    /// Interaction logic for ViewReservationsPage.xaml
    /// </summary>
    public partial class ViewReservationsPage : Page
    {
        public Model.Reservation reservation { get; set; }
        public ViewReservationsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.LvRervations.ItemsSource = Model.DAO.Reservation.GetAllByMember(MainWindow.currentMember);
        }

        private void LvRervations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentSelectedPerson = (Reservation)e.AddedItems[0];
        }

        private Reservation currentSelectedPerson;
        public Reservation CurrentSelectedPerson
        {
            get { return currentSelectedPerson; }
            set
            {
                currentSelectedPerson = value;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentSelectedPerson"));
            }
        }
    }
}
