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
    /// Interaction logic for ExaminatorsPage.xaml
    /// </summary>
    public partial class ExaminatorsPage : Page
    {
        public MemberDAO memberD = new MemberDAO();
        public CerticateDAO certDao = new CerticateDAO();
        public ExaminatorsPage()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            LvMember.ItemsSource = memberD.GetAll();
        }

        private void LvMember_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxDiplomas.Items.Clear();
            certDao.GetAvailableByMember((Member)LvMember.SelectedItem).ForEach(x => ComboBoxDiplomas.Items.Add(x.name));
            LvCerts.ItemsSource = certDao.GetByMember((Member)LvMember.SelectedItem);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //ComboBoxDiplomas.Items.Clear();
            //certDao.GetAvailableByMember(MainWindow.currentMember).ForEach(x => ComboBoxDiplomas.Items.Add(x.name));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxDiplomas.SelectedValue == null || LvMember.SelectedItem == null) return;
            certDao.AddToMember((Member)LvMember.SelectedItem, new Certificate(ComboBoxDiplomas.SelectedValue.ToString()));
            ComboBoxDiplomas.Items.Clear();
            certDao.GetAvailableByMember((Member)LvMember.SelectedItem).ForEach(x => ComboBoxDiplomas.Items.Add(x.name));
            LvCerts.ItemsSource = certDao.GetByMember((Member)LvMember.SelectedItem);
        }
    }
}