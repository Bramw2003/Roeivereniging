using System;
using System.Collections.Generic;
using Model;
namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Boat> b = Model.DAO.Boat.GetBoatAll();
            foreach (Boat boat in b)
            {
                Console.WriteLine(boat.ToString());
                Console.WriteLine(boat.defect);
            }
            //Database.Select("", "");
        }
    }
}
