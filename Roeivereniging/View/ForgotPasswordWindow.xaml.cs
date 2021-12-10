﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model.DAO;
using Model;
using System.Linq;
using FluentEmail.Mailgun;
using FluentEmail.Core;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Configuration;

namespace View
{
    /// <summary>
    /// Interaction logic for ForgotPasswordWindow.xaml
    /// </summary>
    public partial class ForgotPasswordWindow : Window
    {
        public MemberDAO memberDAO = new MemberDAO();
        private Random random = new Random();
        public ForgotPasswordWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var member = memberDAO.GetByUsername(TbUsername.Text);
            if (member.email == TbEmail.Text)
            {
                var newpass = RandomString(8);
                if (memberDAO.UpdatePassword(member, newpass))
                {
                    if (SendTempPassword(member, newpass)) MessageBox.Show("Tijdelijk wachtwoord succesvol verzonden!");
                }
            }
        }


        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void TbEmail_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (TbEmail.IsKeyboardFocused)
            {
                PbEmailPlaceholder.Visibility = Visibility.Hidden;
            }
            else if (TbEmail.Text == "")
            {
                PbEmailPlaceholder.Visibility = Visibility.Visible;

            }

        }
        private void TbUsername_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (TbUsername.IsKeyboardFocused)
            {
                PbUsernamePlaceholder.Visibility = Visibility.Hidden;
            }
            else if (TbUsername.Text == "")
            {
                PbUsernamePlaceholder.Visibility = Visibility.Visible;

            }
        }
        public bool SendTempPassword(Member member, string password)
        {
            var configuration = new ConfigurationBuilder()
                                    .AddUserSecrets<App>()
                                    .Build();

            // get usersecret mailDomain from usersecrets.json
            //var mailDomain = Environment.GetEnvironmentVariable("mailDomain");
            //var mailKey = Environment.GetEnvironmentVariable("mailKey");
            var mailDomain = configuration["mailDomain"];
            var mailKey = configuration["mailKey"];

            var sender = new MailgunSender(mailDomain, // Mailgun Domain
                                             mailKey); // Mailgun API Key
            Email.DefaultSender = sender;

            /*
                You can optionally set the sender per instance like so:

                email.Sender = new MailgunSender(...);
            */
            var email = Email.From("roei@"+mailDomain)
                                .To(member.email)
                                .Subject("Tijdelijk wachtwoord Roeivereniging")
                                .Body("Hier is je tijdelijke wachtwoord: " + password);

            return email.Send().Successful;
        }
    }
}