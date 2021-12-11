using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DAO
{
    public class NoMail : IMail
    {
        public NoMail()
        {

        }
        public bool SendMail(string subject, string body, string email)
        {
            return true;
        }

        public bool SendReservationCancelation(Reservation reservation, Member member)
        {
            return SendMail("","","");
        }

        public bool SendReservationCancelation(Reservation reservation)
        {
            return SendMail("", "", "");

        }

        public bool SendReservationConfirmation(Reservation reservation)
        {
            return SendMail("", "", "");

        }

        public bool SendTempPassword(Member member, string password)
        {
            return SendMail("", "", "");

        }
    }
}
