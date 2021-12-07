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

namespace View
{
    /// <summary>
    /// Interaction logic for RepairBoatDialog.xaml
    /// </summary>
    public partial class RepairBoatDialog : Window
    {
        Defect defect;
        public RepairBoatDialog(Model.Defect defect)
        {
            this.defect = defect;
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            grid.DataContext = defect;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = Model.DAO.Repairs.AddRepair(defect, TbNote.Text,MainWindow.currentMember);
            this.Close();
        }
    }
}
