using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Reservation
    {
        public DateTime startTime { get; }
        public DateTime endTime { get; }
        public Model.Boat boat { get; }
        public Model.Member member { get; }

        public Reservation(DateTime startTime, DateTime endTime, int boatId, int MemberId)
        {
            this.startTime = startTime;
            this.endTime = endTime;
            this.boat = Model.DAO.Boat.GetBoatByID(boatId);
            this.member = Model.DAO.Member.GetById(MemberId);
        }
    }
}
