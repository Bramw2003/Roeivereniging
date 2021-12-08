using System;
using System.Collections.Generic;
using System.Windows.Input;
using MicroMvvm;
using Model;
using Model.DAO;

namespace Viewmodel
{
    public static class BoatViewmodel{
        private static BoatDAO BoatDB = new BoatDAO();

        public static List<Boat> GetAllBoats() {
            return BoatDB.GetAll();
        }
        
        public static void AddBoat(string name, int type, int capacity, bool sculling, bool steer) {
            int? typeID = BoatDB.FindTypeIDByDetails(capacity, type, steer, sculling);
            if(typeID == null) {
                BoatDB.MakeTypeWithDetails(capacity, type, steer, sculling);
                typeID = BoatDB.FindTypeIDByDetails(capacity, type, steer, sculling);
            }
            BoatDB.AddBoat(name, (BoatType)typeID);
        }

    }
}
