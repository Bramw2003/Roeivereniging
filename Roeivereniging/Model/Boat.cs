using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Boat
    {
        public int id { get; }
        public string name { get; }
        public string type { get; }
        public int capacity { get; }
        public bool steer { get; }
        public bool sculling { get; }
        public BoatType category { get; }
        public bool defect { get; }
        public bool deleted { get; }
        //location info
        public int shed { get; }
        public int row { get; }
        public int Column { get; }
        public int Height { get; }
        public string location {
            get { return $"{shed}-{row}-{Column}-{Height}"; } 
        }

        public Boat(int id, string name, int capacity, int category, bool steer, bool sculling, string location = "", bool defect = false, bool deleted = false)
        {
            this.id = id;
            this.name = name;
            type = "";
            this.category = (BoatType)category;
            this.capacity = capacity;
            this.steer = steer;
            this.sculling = sculling;

            switch (this.category)
            {
                case BoatType.A:
                    break;
                case BoatType.B:
                    type += "B";
                    break;
                case BoatType.C:
                    type += "C";
                    break;
                case BoatType.W:
                    type = "Wherry";
                    break;
                default:
                    break;
            }
            if (category != (int)BoatType.W)
            {
                type += this.capacity;
                type += sculling ? "x" : "";
                type += steer ? "+" : "";
            }
            string[] locationInfo = location.Split("-");
            this.shed = int.Parse(locationInfo[0]);
            this.row = int.Parse(locationInfo[1]);
            this.Column = int.Parse(locationInfo[2]);
            this.Height = int.Parse(locationInfo[3]); 
            this.defect = defect;
            this.deleted = deleted;
        }
        public override string ToString()
        {
            return $"id:\t{id}\t name:\t{name}\tcapacity:\t{capacity}\tsteer:\t{steer}\tsculling:\t{sculling}";
        }

    }
}
