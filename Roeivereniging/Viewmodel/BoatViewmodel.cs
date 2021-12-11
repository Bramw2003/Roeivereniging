﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using MicroMvvm;
using Model;
using Model.DAO;

namespace Viewmodel
{
    public class BoatViewmodel : ObservableObject{
        private static BoatDAO BoatDB = new BoatDAO();
        public List<Boat> _BoatList = GetAllBoats();

        public List<Boat> BoatList {
            get { return _BoatList; }
            set { _BoatList = value; RaisePropertyChanged("BoatList"); }
        }


        /// <summary>
        /// returns list of all boats in database
        /// </summary>
        public static List<Boat> GetAllBoats() {
            return BoatDB.GetAll();
        }

        /// <summary>
        /// Adds boat to database and if types doesnt exits, also makes type
        /// </summary>
        public static void AddBoat(string name, int type, int capacity, bool sculling, bool steer) {
            int? typeID = BoatDB.FindTypeIDByDetails(capacity, type, steer, sculling);
            if(typeID == null) {
                BoatDB.MakeTypeWithDetails(capacity, type, steer, sculling);
                typeID = BoatDB.FindTypeIDByDetails(capacity, type, steer, sculling);
            }
            BoatDB.AddBoat(name, (BoatType)typeID);
        }

        public void DeleteBoat(Boat boat) {
            BoatDB.Delete(boat);
            BoatList = GetAllBoats();
        }

    }
}
