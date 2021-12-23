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
using System.Windows.Shapes;
using Model;
using Viewmodel;

namespace View.Admin
{
    /// <summary>
    /// Interaction logic for EditMemberWindow.xaml
    /// </summary>
    public partial class EditMemberWindow : Window
    {

        MemberViewModel model;
        Member member;
        public EditMemberWindow(MemberViewModel memberMV, Member member)
        {
            InitializeComponent();
            this.model = memberMV;
            this.member = member;
            tbName.Textbox.Text = member.name;
            tbUsername.Textbox.Text = member.username;
            tbEmail.Textbox.Text = member.email;
            dpBirthDay.SelectedDate = member.date;
            cbAdmin.IsChecked = member.admin;
            cbExaminator.IsChecked = member.examinator;
            cbRepair.IsChecked = member.repair;
        }

        private void Edit_Member_Button_Click(object sender, RoutedEventArgs e)
        {
            model.EditMember(member,tbName.Textbox.Text,tbUsername.Textbox.Text,tbEmail.Textbox.Text,(DateTime) dpBirthDay.SelectedDate,(bool) cbAdmin.IsChecked,(bool) cbExaminator.IsChecked,(bool) cbRepair.IsChecked);
            this.Close();
        }
    }
}
