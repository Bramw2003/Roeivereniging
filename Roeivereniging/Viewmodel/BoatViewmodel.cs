using System;
using System.Collections.Generic;
using System.Windows.Input;
using MicroMvvm;
using Model;
using Model.DAO;
using System.Linq;

namespace Viewmodel
{
    public class BoatViewmodel : ObservableObject{
        private static BoatDAO BoatDB = new BoatDAO();
        private static ReservationDAO ReservationDB = new ReservationDAO();
        public List<Boat> _BoatList = GetAllBoats();

        public List<Boat> BoatList {
            get { return _BoatList; }
            set { _BoatList = value; RaisePropertyChanged("BoatList"); }
        }


        /// <summary>
        /// returns list of all boats in database
        /// </summary>
        public static List<Boat> GetAllBoats() {
            return BoatDB.GetAll().Where(x => x.deleted != true).ToList();
        }

        /// <summary>
        /// Adds boat to database and if types doesnt exits, also makes type
        /// </summary>
        public static bool AddBoat(string name, int type, int capacity, bool sculling, bool steer, string location) {
            int? typeID = BoatDB.FindTypeIDByDetails(capacity, type, steer, sculling);
            if(typeID == null) {
                BoatDB.MakeTypeWithDetails(capacity, type, steer, sculling);
                typeID = BoatDB.FindTypeIDByDetails(capacity, type, steer, sculling);
            }
            BoatDB.AddBoat(name, (BoatType)typeID, location);
            return true;
        }

        public void EditBoat(Boat boat, string name, int type,int Capacity,bool sculing, bool steer, string location) {
            BoatDB.EditBoat(boat, name, type, Capacity, sculing, steer, location);
            BoatList = GetAllBoats();
        }

        public void DeleteBoat(Boat boat) {
            BoatDB.Delete(boat);
            BoatList = GetAllBoats();
            ReservationDB.DeleteAllFutureReservationsByBoat(boat);
        }

        

    }
}
