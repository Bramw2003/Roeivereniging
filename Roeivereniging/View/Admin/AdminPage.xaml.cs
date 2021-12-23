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
using View.Admin;

namespace View {
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page {

        private AddMemberPage AddMemberPage = new AddMemberPage();
        private AddBoatPage AddBoatPage = new AddBoatPage();
        private ManageBoatPage DeleteBoatPage = new ManageBoatPage();
        private ManageMembersPage manageMembersPage = new ManageMembersPage();
        public AdminPage() {
            InitializeComponent();
        }

        private void Button_Member_Add_Click(object sender, RoutedEventArgs e) {
            AdminFrame.Content = AddMemberPage;
        }

        private void button2_Click(object sender, RoutedEventArgs e) {
            AdminFrame.Content = AddBoatPage;
        }

        private void Button_Boat_Delete_Click(object sender, RoutedEventArgs e) {
            AdminFrame.Content = DeleteBoatPage;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Content = manageMembersPage;
        }
    }
}
