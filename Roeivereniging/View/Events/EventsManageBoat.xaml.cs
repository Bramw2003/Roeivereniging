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
using System.Windows.Shapes;
using Model;
using System.Linq;

namespace View
{
    /// <summary>
    /// Interaction logic for EventsManageBoat.xaml
    /// </summary>
    public partial class EventsManageBoat : Window
    {
#nullable enable
        public List<Boat>? boats = null;
        public EventsManageBoat(List<Boat> availableBoat, List<Boat> eventBoats)
        {
            InitializeComponent();
            
            foreach (var item in eventBoats)
            {
                EventsBoats.Items.Add(item);
            }
            foreach (var item in availableBoat)
            {
                AvailableBoats.Items.Add(item);
            }
        }

        private void MoveToSelection_Click(object sender, RoutedEventArgs e)
        {
            MoveItem(AvailableBoats, EventsBoats, false);
        }

        private void MoveFromSelection_Click(object sender, RoutedEventArgs e)
        {
            MoveItem(EventsBoats, AvailableBoats, false);
        }

        private void MoveItem(ListView source, ListView dest, bool all)
        {
            var items = all ? source.Items.OfType<Boat>(): source.SelectedItems.OfType<Boat>();
            foreach (Boat item in items.ToList()) 
            {
                source.Items.Remove(item);
                dest.Items.Add(item);
            }
            boats = EventsBoats.Items.OfType<Boat>().ToList();
        }

        private void MoveAllToSelection_Click(object sender, RoutedEventArgs e)
        {
            MoveItem(AvailableBoats, EventsBoats, true);
        }

        private void MoveAllFromSelection_Click(object sender, RoutedEventArgs e)
        {
            MoveItem(EventsBoats, AvailableBoats, true);
        }
    }
}
