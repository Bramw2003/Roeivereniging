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
using Model;
using Model.DAO;
namespace View
{
    /// <summary>
    /// Interaction logic for ChangePasswordPage.xaml
    /// </summary>
    public partial class ChangePasswordPage : Page
    {
        MainWindow mainWindow;
        public ChangePasswordPage(MainWindow main)
        {
            mainWindow = main;
            InitializeComponent();
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            if (Database.UserLogin(MainWindow.currentMember.username, TbCurrent.Textbox.Text))
            {
                MemberDAO mDao = new MemberDAO();
                if (TbNew.Textbox.Text == TbNewRepeat.Textbox.Text)
                {
                    mDao.UpdatePassword(MainWindow.currentMember, TbNew.Textbox.Text);
                    mainWindow.Logout();
                }
                else
                {
                    LbWarning.Content = "Nieuwe wachtwoorden zijn niet gelijk!";
                    LbWarning.Visibility = Visibility.Visible;
                }
            }
            else
            {
                LbWarning.Content = "Huidig wachtwoord is verkeerd";
                LbWarning.Visibility = Visibility.Visible;
            }
        }
    }
}
