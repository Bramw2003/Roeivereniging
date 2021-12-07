using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Repairs
    {
        public Defect defect { get; }
        public Member repairer { get; }
        public DateTime repairDate { get; }

        public Repairs(Defect defect, Member repairer, DateTime repairDate)
        {
            this.defect = defect;
            this.repairer = repairer;
            this.repairDate = repairDate;
        }

        public override string ToString()
        {
            return $"{defect.boat.name} is repaired by {repairer.GetName()} on {repairDate.ToString("HH:mm")}";
        }
    }
}
