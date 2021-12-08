using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Certificate
    {
        public DateTime date { get; set; }
        public string name { get; }
        public Certificate(string name, DateTime? date = null)
        {
            this.name = name;
            if(date == null)
            {
                date = DateTime.Now;
            }
            this.date = date.Value;
        }
    }
}
