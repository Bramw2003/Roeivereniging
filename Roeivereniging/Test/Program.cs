using System;
using System.Collections.Generic;
using Model;
namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Boat> b = Database.GetBoatAll();
            foreach (Boat boat in b)
            {
                Console.WriteLine(boat.ToString());
            }
            //Database.Select("", "");
        }
    }
}
