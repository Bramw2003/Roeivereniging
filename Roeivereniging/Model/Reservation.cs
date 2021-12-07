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
        public Boat boat { get; }
        public Member member { get; }

        public Reservation(DateTime startTime, DateTime endTime, Boat boat, Member member)
        {
            this.date = startTime.Date;
            this.startTime = startTime;
            this.endTime = endTime;
            this.boat = boat;
            this.member = member;
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

        public override string ToString()
        {
            return $"{this.date.ToString("dd-MM-yyyy")} {this.startTime.ToString("HH:mm")} - {this.endTime.ToString("HH:mm")} {this.boat.name} {this.member.GetName()} {this.member.GetId()}";
        }
    }
}
