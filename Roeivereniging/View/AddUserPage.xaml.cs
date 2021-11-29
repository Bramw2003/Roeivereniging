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
using Model.DAO;

namespace View {
    /// <summary>
    /// Interaction logic for AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page {
        public AddUserPage() {
            InitializeComponent();
        }

        private void Add_User_Button_Click(object sender, RoutedEventArgs e) {//adds new member
            Member.AddUser(TbUserrname.Text, TbPassword.Text, TbEmail.Text, TbName.Text, (DateTime)DpBirthDate.SelectedDate);

        } 
    }
}
