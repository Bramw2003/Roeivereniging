using System;
using System.Collections.Generic;
using System.Text;
using MicroMvvm;
using Model;
using Model.DAO;
namespace Viewmodel
{
    public class EventsViewModel : ObservableObject
    {
        public static EventsDAO EventsDAO = new EventsDAO();
        private List<Event> _events;

        public List<Event> events
        {
            get { return _events; }
            set { _events = value; RaisePropertyChanged("BoatList"); }
        }

        public bool Reserve(Member member, Boat boat, Event @event)
        {
            return EventsDAO.ReserveBoat(member, boat, @event);
        }

        public List<Event> GetAll()
        {
            return EventsDAO.GetAll();
        }
        public List<Event> GetAllByCreator(Member member)
        {
            return EventsDAO.GetByCreator(member);
        }
        public bool HasReservationForEvent(Member member, Event @event)
        {
            return EventsDAO.HasReservationForEvent(member, @event);
        }
        public EventsViewModel()
        {
            _events = GetAll();
        }
        public bool AddBoatsToEvent(List<Boat> boats, Event @event)
        {
            return EventsDAO.AddBoatsToEvent(boats, @event);
        }
        public List<Boat> GetAllBoatsByEventID(int id)
        {
            return EventsDAO.GetAllBoatsByEventID(id);
        }
        public bool AddMember(Event @event, Member member)
        {
            return EventsDAO.AddMember(@event, member);
        }
        public bool UpdateEvent(Event @event)
        {
            return EventsDAO.UpdateEvent(@event);
        }
    }
}
