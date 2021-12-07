using System;
using Model.DAO;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Viewmodel {
    public static class ReservationViewModel {
        private static ReservationDAO ReservationDB = new ReservationDAO();

        public static List<Reservation> GetAllReservations() {
            return ReservationDB.GetAll();
        }

        public static bool MakeReservation(Boat boat, DateTime date, DateTime endTime, Member member) {
            return ReservationDB.ReserveBoat(boat, date, endTime, member);
        }

        public static List<Reservation> GetAllByMember(Member member) {
            return ReservationDB.GetAllByMember(member);
        }

    }
}
