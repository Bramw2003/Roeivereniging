using System;
using MicroMvvm;
using Model;
namespace Viewmodel
{
    public class BoatViewmodel : ObservableObject
    {
        private Boat _boat;
        public BoatViewmodel()
        {
            _boat = new Boat(0,"Unknown",1,1,false,true);
        }

        public Boat Boat
        {
            get { return _boat; }
            set { _boat = value; }
        }

        public string BoatName
        {
            get { return _boat.name; }
        }
    }
}
