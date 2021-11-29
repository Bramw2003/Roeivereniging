using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (TbUsername.Text != "" && PbPassword.Text != "" && PbPassword.Text != null)
            {
                if (Database.UserLogin(TbUsername.Text, PbPassword.Text))
                {
                    MainWindow.currentMember = Model.DAO.Member.GetByUsername(TbUsername.Text);
                    this.DialogResult = true;
                    this.Close();
                }
                else {
                    Melding.Visibility = Visibility.Visible;
                }
            }
        }

        private void TbUsername_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TbUsername_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (TbUsername.IsKeyboardFocused)
            {
                TbUsernamePlaceholder.Visibility = Visibility.Hidden;
            }
            else
            {
                if(TbUsername.Text == "")
                {
                    TbUsernamePlaceholder.Visibility = Visibility.Visible;
                }
            }
        }

        private void PbPassword_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (PbPassword.IsKeyboardFocused)
            {
                PbPasswordPlaceholder.Visibility = Visibility.Hidden;
            }
            else
            {
                if (PbPassword.Text == "")
                {
                    PbPasswordPlaceholder.Visibility = Visibility.Visible;
                }
            }
        }

        private void PbPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                BtnLogin_Click(null,null);
            }
        }
    }
}
