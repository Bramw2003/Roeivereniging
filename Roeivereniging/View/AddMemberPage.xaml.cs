using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Viewmodel;

namespace View
{
    /// <summary>
    /// Interaction logic for AddMemberPage.xaml
    /// </summary>
    public partial class AddMemberPage : Page
    {
        public AddMemberPage()
        {
            InitializeComponent();
        }

        private void Add_Member_Button_Click(object sender, RoutedEventArgs e) {
            MemberViewModel.MakeUser(tbName.Text,tbUserName.Text, (DateTime)dpBirthDay.SelectedDate,tbEMail.Text,tbPassWord.Text); 
        }
    }
}
