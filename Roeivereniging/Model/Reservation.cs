using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Reservation
    {
        public DateTime date { get;}
        public DateTime startTime { get; }
        public DateTime endTime { get; }
        public Model.Boat boat { get; }
        public Model.Member member { get; }

        public Reservation(DateTime startTime, DateTime endTime, int boatId, int MemberId)
        {
            this.date = startTime.Date;
            this.startTime = startTime;
            this.endTime = endTime;
            this.boat = Model.DAO.Boat.GetBoatByID(boatId);
            this.member = Model.DAO.Member.GetById(MemberId);
        }
        public string StartTime { 
            get {  return this.startTime.ToString("HH:mm"); }
        }

        public string EndTime
        {
            get { return this.endTime.ToString("HH:mm"); }
        }

        public string Date
        {
            get { return this.date.ToString("dd-MM-yyyy"); }
        }
    }
}
