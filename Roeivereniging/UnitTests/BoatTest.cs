using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Viewmodel;

namespace UnitTests {
    
    class BoatTest {

        [SetUp]
        public void Setup() {
            Database.catalog = "Roeivereniging_Test";
        }

        [Test]
        public void Boat_AddBoat_MakeBoat() {
            BoatViewmodel.AddBoat("test", 2, 4, true, false);
            List<Boat> result = BoatViewmodel.GetAllBoats();
            Assert.AreEqual(4, result[0].capacity);
        }
    }
}
