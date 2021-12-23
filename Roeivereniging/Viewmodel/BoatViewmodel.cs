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
        public void AddBoat(string name, int type, int capacity, bool sculling, bool steer, string location) {
            int? typeID = BoatDB.FindTypeIDByDetails(capacity, type, steer, sculling);
            if(typeID == null) {
                BoatDB.MakeTypeWithDetails(capacity, type, steer, sculling);
                typeID = BoatDB.FindTypeIDByDetails(capacity, type, steer, sculling);
            }
            BoatDB.AddBoat(name, (BoatType)typeID, location);
            BoatList = GetAllBoats();
        }

        public void EditBoat(Boat boat, string name, int type,int Capacity,bool sculing, bool steer, string location) {
            BoatDB.EditBoat(boat, name, type, Capacity, sculing, steer, location);
            BoatList = GetAllBoats();
        }
        //public void AddBoat(Boat boat)
        //{
        //    AddBoat(boat.name,(int)boat.category,boat.capacity,boat.sculling,boat.steer);
        //}
        public void DeleteBoat(Boat boat) {
            BoatDB.Delete(boat);
            BoatList = GetAllBoats();
            ReservationDB.DeleteAllFutureReservationsByBoat(boat);
        }

        public void CheckShedSpace(string location, int boatID)
        {
            string[] locationInfo = location.Split("-");
            int shed = int.Parse(locationInfo[0]);
            int row = int.Parse(locationInfo[1]);
            int Column = int.Parse(locationInfo[2]);
            int Height = int.Parse(locationInfo[3]);

            if (shed > 8 || shed < 1)
            {
                //error: Shed doesnt exist
                throw new Exception("Deze loods bestaat niet. Waarde moet tussen de 1 en 8 zijn");
            }
            if (shed == 1 && row == 1 || row > 2 || row < 1)
            {
                //error: Row doesnt exist
                throw new Exception("Deze Rij bestaat niet. Waarde moet tussen de 1 en 2 zijn");
            }
            if(Column > 2 || Column < 1)
            {
                //error: Column doesnt exist
                throw new Exception("Deze Kolom bestaat niet. Waarde moet tussen de 1 en 2 zijn");
            }
            if(Height > 4 || Height < 0)
            {
                //error: height doesnt exist
                throw new Exception("Deze Hoogte bestaat niet. Waarde moet tussen de 0 en 4 zijn");
            }

           var boatRow = BoatList.Where(x => x.shed == shed && x.row == row && x.Height == Height);
            if(boatRow.Count() == 0)
            {
                //space found
            } else if(boatRow.First().capacity >= 8 && boatRow.First().id != boatID)
            {
                //error: space ocupied
                throw new Exception("Plek bezet");
            } else if(boatRow.First().Column == Column && boatRow.First().id != boatID)
            {
                //error space ocupied
                throw new Exception("Plek bezet");
            }
            else
            {
                //space found 
            }
        }
    }
}
