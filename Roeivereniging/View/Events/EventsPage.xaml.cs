using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    /// Interaction logic for EventsPage.xaml
    /// </summary>
    public partial class EventsPage : Page
    {
        public EventsPage()
        {
            InitializeComponent();
            Refresh();
        }

        private void ReserveBoat_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext.GetType() == typeof(Viewmodel.EventsViewModel))
            {
                var EventsViewModel = (Viewmodel.EventsViewModel)DataContext;
                EventsViewModel.Reserve(MainWindow.currentMember, (Model.Boat)LvBoats.SelectedItem, (Model.Event)LvEvents.SelectedItem);
                EventsViewModel.AddMember((Model.Event)LvEvents.SelectedItem, MainWindow.currentMember);
            }
            Refresh();
        }

        private void LvBoats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LvEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext.GetType() == typeof(Viewmodel.EventsViewModel))
            {
                var EventsViewModel = (Viewmodel.EventsViewModel)DataContext;
                var selectedEvent = (Model.Event)LvEvents.SelectedItem;
                if (ReserveColumn == null || BtnEdit == null || selectedEvent == null)
                {
                    return;
                }
                
                Map.Source = Convert(selectedEvent.map);
                BtnEdit.IsEnabled = selectedEvent.creator.id == MainWindow.currentMember.id;

                if (EventsViewModel.HasReservationForEvent(MainWindow.currentMember, selectedEvent)||selectedEvent.maxMembers == selectedEvent.members.Count)
                {
                    ReserveColumn.Visibility = Visibility.Hidden;
                }
                else
                {
                    ReserveColumn.Visibility = Visibility.Visible;
                }
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditEventWindow editEventDialog = new EditEventWindow((Model.Event)LvEvents.SelectedItem);
            editEventDialog.ShowDialog();
            if (DataContext.GetType() == typeof(Viewmodel.EventsViewModel))
            {
                var EventsViewModel = (Viewmodel.EventsViewModel)DataContext;
                EventsViewModel.UpdateEvent(editEventDialog.@event);
            }
            
            Refresh();
        }

        private void Refresh()
        {
            if (DataContext.GetType() == typeof(Viewmodel.EventsViewModel))
            {
                var EventsViewModel = (Viewmodel.EventsViewModel)DataContext;
                int currIndex = LvEvents.SelectedIndex;
                LvEvents.ItemsSource = EventsViewModel.GetAll();
                LvEvents.SelectedIndex = currIndex;
            } 
        }

        public BitmapImage Convert(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        private void Map_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show("faail");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditEventWindow NewEventDialog = new EditEventWindow(new Model.Event(MainWindow.currentMember));
            NewEventDialog.ShowDialog();
            Refresh();
        }
    }
}
