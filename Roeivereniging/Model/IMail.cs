using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface IMail
    {
        //public IMail Mail(IConfigurationRoot configuration);
        public bool SendReservationCancelation(Reservation reservation, Member member);
        public bool SendReservationCancelation(Reservation reservation);
        public bool SendReservationConfirmation(Reservation reservation);
        public bool SendTempPassword(Member member, string password);
        public bool SendMail(string subject, string body, string email);
    }
}
