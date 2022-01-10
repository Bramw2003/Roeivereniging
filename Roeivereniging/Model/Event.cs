using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace Model
{
    public class Event
    {
        public int Id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int maxMembers { get; set; }
        public Member creator { get; set; }
        public List<Boat> availableBoats { get; set; }
        public List<Reservation> reservations { get; set; }
        public List<Member> members { get; set; }
        public Bitmap map { get; set; }

        public Event(int maxMembers, Member creator, DateTime start, DateTime end, string name, string type, int id, byte[] map)
        {
            if (map != null)
            {
                this.map = ByteToImage(map);
            }
            else
            {
                this.map = new Bitmap("Events\\map.bmp");
            }
            this.maxMembers = maxMembers;
            this.start = start;
            this.end = end;
            availableBoats = new List<Boat>();
            reservations = new List<Reservation>();
            members = new List<Member>();
            this.name = name;
            this.type = type;
            this.Id = id;
            this.creator = creator;
        }
        public Event(Member creator):this(50,creator,DateTime.Now,DateTime.Now,"","",-1,null)
        {
            
        }

        private static Bitmap ByteToImage(byte[] blob)
        {
            using (MemoryStream mStream = new MemoryStream())
            {
                mStream.Write(blob, 0, blob.Length);
                mStream.Seek(0, SeekOrigin.Begin);

                Bitmap bm = new Bitmap(mStream);
                return bm;
            }
        }
    }
}
