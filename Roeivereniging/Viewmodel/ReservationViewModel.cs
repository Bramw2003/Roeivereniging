using System;
using Model.DAO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Viewmodel {
    public static class ReservationViewModel {
        private static ReservationDAO ReservationDB = new ReservationDAO();

        public static List<Reservation> GetAllReservations() {
            return ReservationDB.GetAll();
        }

        public static bool MakeReservation(Boat boat, DateTime date, DateTime endTime, Member member) {
            if (GetAllByMember(member).Count(x => x.date > DateTime.Now) >= 2)
            {
                return false;
            }
            return ReservationDB.ReserveBoat(boat, date, endTime, member);
        }

        public static List<Reservation> GetAllByMember(Member member) {
            return ReservationDB.GetAllByMember(member);
        }

        public static void DeleteReservation(Reservation reservation)
        {
            ReservationDB.DeleteReservation(reservation.boat,reservation.date,reservation.startTime,reservation.endTime,reservation.member);
        }

    }
}
