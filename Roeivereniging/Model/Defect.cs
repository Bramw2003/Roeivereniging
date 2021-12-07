using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Defect
    {
        public string title { get; set; }
        public string description { get; set; }
        public Member reporter { get; set; }
        public Boat boat { get; set; }

        public Defect(string title, string description, Member reporter, Boat boat)
        {
            this.title = title;
            this.description = description;
            this.reporter = reporter;
            this.boat = boat;
        }
        
        public override string ToString()
        {
            return $"{title} - {description} - {reporter.GetName()} - {boat.name}";
        }
    }
}
