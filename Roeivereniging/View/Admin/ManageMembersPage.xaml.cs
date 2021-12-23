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

namespace View.Admin
{
    /// <summary>
    /// Interaction logic for ManageMembersPage.xaml
    /// </summary>
    public partial class ManageMembersPage : Page
    {
        MemberViewModel _MemberViewModel;
        public ManageMembersPage()
        {
            InitializeComponent();
            _MemberViewModel = (MemberViewModel)base.DataContext;
        }

        private void Add_Member_Button_Click(object sender, RoutedEventArgs e)
        {
            var AddMemberWindow = new AddMemberWindow(_MemberViewModel);
            AddMemberWindow.ShowDialog();
        }
    }
}
