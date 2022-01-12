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
using System.Linq;
using Viewmodel;

namespace View
{
    /// <summary>
    /// Interaction logic for ViewReservationsPage.xaml
    /// </summary>
    public partial class ViewReservationsPage : Page
    {
        private BoatMap map;
        public Model.Reservation reservation { get; set; }
        public ViewReservationsPage()
        {
            InitializeComponent();
            map = new BoatMap();
            Frame.Content = map;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLvRervations();
        }

        private void LoadLvRervations()
        {
            if (showAll) this.LvRervations.ItemsSource = ReservationViewModel.GetAllReservations().OrderByDescending(x => x.date);
            else this.LvRervations.ItemsSource = ReservationViewModel.GetAllByMember(MainWindow.currentMember).OrderByDescending(x => x.date);
        }

        private void LvRervations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentSelectedReservation = (Reservation)LvRervations.SelectedItem;
            if (currentSelectedReservation != null)
            {
                map.SetSelectedBoat(currentSelectedReservation.boat);
                CancelReservation.IsEnabled = currentSelectedReservation.date > DateTime.Now;

            }

        }

        private Reservation currentSelectedReservation;
        public Reservation CurrentSelectedPerson
        {
            get { return currentSelectedReservation; }
            set
            {
                currentSelectedReservation = value;
            }
        }

        private void BtnReportDefect_click(object sender, RoutedEventArgs e)
        {
            DefectPopup defectPopup = new DefectPopup();
            defectPopup.ShowDialog();
            if ((bool)defectPopup.DialogResult && defectPopup.TbTitle.Text != "" && defectPopup.TbDescription.Text != "")
            {
                DefectViewModel.AddDefect(new Defect(defectPopup.TbTitle.Text, defectPopup.TbDescription.Text, MainWindow.currentMember, currentSelectedReservation.boat, (bool)!defectPopup.CBseaworthy.IsChecked));
                var j = LvRervations.SelectedIndex;
                LoadLvRervations();
                LvRervations.SelectedIndex = j;
            }
        }

        private void BtnCancelReservation_click(object sender, RoutedEventArgs e)
        {
            var selectedReservation = (Reservation)LvRervations.SelectedItem;
            if (selectedReservation != null)
            {
                if ((selectedReservation.member.id == MainWindow.currentMember.id) ||
                MessageBox.Show($"U staat op het punt de reservering van {selectedReservation.member.name} te annuleren, weet u het zeker dat u deze wilt annuleren?", "Zeker weten", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    MainWindow.Mail.SendReservationCancelation(selectedReservation, MainWindow.currentMember);
                    ReservationViewModel.DeleteReservation(selectedReservation);
                    LoadLvRervations();
                }
            }
        }

        private bool showAll = false; //used to deterime which method should be used to get the reservation in the LoadLvRervations() method

        private void CbxShowAllUnchecked(object sender, RoutedEventArgs e)
        {
            showAll = false;
            LoadLvRervations();
        }

        private void CbxShowAllChecked(object sender, RoutedEventArgs e)
        {
            showAll = true;
            LoadLvRervations();
        }
    }
}
