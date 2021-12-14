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
using Viewmodel;

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
            // Add a "remove boat" butten if the current user is a admin
            if (MainWindow.currentMember.IsAdmin()) {
                var column = new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) };
                RepairMenu.ColumnDefinitions.Add(column);
                var button = new Button() { Content = "Boot Verwijderen", HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };
                RemoveBtn.Children.Add(button);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Get all defects and display them in the listview
            LvDefects.ItemsSource = DefectViewModel.AllDefects();
        }

        private void RepairBtn_Click(object sender, RoutedEventArgs e)
        {
            var RepairDialog = new RepairBoatDialog((Defect)LvDefects.SelectedItem);
            if ((bool)RepairDialog.ShowDialog())
            {
                LvDefects.ItemsSource = DefectViewModel.AllDefects();
            }
        }
    }
}
