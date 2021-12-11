using System;
using System.Collections.Generic;
using System.Text;
using FluentEmail.Mailgun;
using FluentEmail.Core;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Configuration;
namespace Model.DAO
{
    public class Mail : IMail
    {
        string mailDomain { get; set; }
        MailgunSender MailgunSender { get; set; }
        public Mail(IConfigurationRoot configuration)
        {
            // get usersecret mailDomain from usersecrets.json
            mailDomain = configuration["mailDomain"];
            var mailKey = configuration["mailKey"];
            MailgunSender = new MailgunSender(mailDomain, mailKey);
        }
        public bool SendReservationCancelation(Reservation reservation, Member member)
        {
            string body;
            if (member.name != reservation.member.name)
            {
                body = "Je reservering is geannuleerd door" + member.name;
            }
            else
            {
                body = "Je reservering is geannuleerd";
            }
            return SendMail("Annulering reservering " + reservation.Date + " " + reservation.StartTime,
                body,
                reservation.member.email);
        }

        public bool SendReservationCancelation(Reservation reservation)
        {
            return SendReservationCancelation(reservation, reservation.member);
        }
        public bool SendReservationConfirmation(Reservation reservation)
        {
            return SendMail($"Reservering {reservation.Date} {reservation.StartTime}",
                $"Reservering op {reservation.Date} tussen {reservation.StartTime} en {reservation.EndTime}",
                reservation.member.email);
        }
        public bool SendTempPassword(Member member, string password)
        {
            return SendMail("Tijdelijk wachtwoord Roeivereniging", "Hier is je tijdelijke wachtwoord: " + password, member.email);
        }
        public bool SendMail(string subject, string body, string email)
        {
            Email.DefaultSender = MailgunSender;
            var mail = Email.From("roei@" + mailDomain)
                                .To(email)
                                .Subject(subject)
                                .Body(body);
            var s = mail.Send();
            return s.Successful;
        }
    }
}
