using System;
using System.Collections.Generic;
using System.Text;

namespace Model {
    interface IBoat {
        bool insert(Boat boat);
        void Delete(Boat boat);
        Boat GetBoatByID(int id);
        List<Boat> GetAll();
    }
}
