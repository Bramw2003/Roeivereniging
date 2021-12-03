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

namespace View
{
    /// <summary>
    /// Interaction logic for DefectsPage.xaml
    /// </summary>
    public partial class DefectsPage : Page
    {
        public DefectsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (MainWindow.currentMember.IsRepair())
            {
                var column = new ColumnDefinition() { Width=new GridLength(1,GridUnitType.Star)};
                RepairMenu.ColumnDefinitions.Add(column);
                var button = new Button() { Content = "Boot Verwijderen", HorizontalAlignment=HorizontalAlignment.Stretch, VerticalAlignment=VerticalAlignment.Stretch };
                RemoveBtn.Children.Add(button);
            }
            LvDefects.ItemsSource = Model.DAO.Defect.GetAll();
        }
    }
}
