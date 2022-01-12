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
using Viewmodel;

namespace View.Admin
{
    /// <summary>
    /// Interaction logic for AddMemberWindow.xaml
    /// </summary>
    public partial class AddMemberWindow : Window
    {
        MemberViewModel model;
        public AddMemberWindow(MemberViewModel MemberVM)
        {
            InitializeComponent();
            model = MemberVM;
        }

        private void Add_Member_Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbEMail.Text != "" && tbName.Text != "" && tbPassWord.Text != "" && tbUserName.Text != "")
            {
                model.MakeUser(tbName.Text, tbUserName.Text, (DateTime)dpBirthDay.SelectedDate, tbEMail.Text, tbPassWord.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("Niet alle gegevens zijn ingevuld!");
            }
        }
    }
}
