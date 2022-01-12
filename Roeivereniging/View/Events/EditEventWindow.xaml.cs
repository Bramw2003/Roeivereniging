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
using Model;
using System.Linq;

namespace View
{
    /// <summary>
    /// Interaction logic for EditEventWindow.xaml
    /// </summary>
    public partial class EditEventWindow : Window
    {
        public Event @event;
        private bool @new;
        public EditEventWindow(Event @event, bool @new = false)
        {
            InitializeComponent();
            this.@event = @event;
            this.DataContext = @event;
            this.@new = @new;
            if (!@new)
            {
                StartDatePicker.SelectedDate = @event.start;
                EndDatePicker.SelectedDate = @event.end;
                if (@event.type.ToLower().Contains("wed"))
                {
                    TypeBox.SelectedIndex = 0;
                }
                else
                {
                    TypeBox.SelectedIndex = 1;
                }
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Model.DAO.BoatDAO boatDao = new Model.DAO.BoatDAO();
            Model.DAO.EventsDAO eventDao = new Model.DAO.EventsDAO();
            var editBoatWindow = new EventsManageBoat(boatDao.GetAll().Where(x => @event.availableBoats.All(x2 => x2.id != x.id) && eventDao.GetAllBoatsByEventID(@event.Id).All(x2 => x2.id != x.id)).ToList(), @event.availableBoats);
            editBoatWindow.ShowDialog();

            if (editBoatWindow.boats != null && !@new)
            {
                var newItems = editBoatWindow.boats.Where(x => @event.availableBoats.All(x2 => x2.id != x.id)).ToList();
                eventDao.AddBoatsToEvent(newItems, @event);

                var removedItems = @event.availableBoats.Where(x => editBoatWindow.EventsBoats.Items.OfType<Boat>().All(x2 => x2.id == x.id)).ToList();
                eventDao.RemoveBoatsFromEvent(removedItems, @event);
            }
            else
            {
                @event.availableBoats = editBoatWindow.boats;
            }
        }

        private void EditMap_Click(object sender, RoutedEventArgs e)
        {
            var a = new EditMapWindow(@event);
            a.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            @event.description = DescriptionBox.Text;
            @event.name = Title.Text;
            if (TypeBox.SelectedIndex == 1)
            {
                @event.type = "Tourtocht";
            }
            else
            {
                @event.type = "Wedstrijd";
            }
        }
    }
}
