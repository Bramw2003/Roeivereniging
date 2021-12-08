using System;
using System.Collections.Generic;
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
    }
}
