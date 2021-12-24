using Model;
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
    /// Interaction logic for BoatMap.xaml
    /// </summary>
    public partial class BoatMap : Page
    {
        private int shedStart = 35;
        private int columnStart = 67;

        private int shedSpacing = 502;
        private int rowSpacing = 279;
        private int columnSpacing = 468;

        private Boat selectedBoat = new Boat(102,"test",4,1,false,false,"2-1-1-1");

        private int imgSizeX = 4028;
        private int imgSizeY = 1235;

        private int imgBoatSizeX = 134;
        private int imgBoatSizeY = 426;

        private double scale = 1.0;
        public BoatMap()
        {
            
            InitializeComponent();
            Background.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            Background.Arrange(new Rect(0, 0, Background.DesiredSize.Width, Background.DesiredSize.Height));

            SetImagesSizes();
            SetScale();
            SetSelectionPosition();
           
        }
        
        private void SetImagesSizes()
        {
            //Background.Height = page.ActualHeight;
            //Background.Width = page.ActualWidth;
        }

        private void SetSelectionPosition()
        {
            int posX = 100;
            posX += (selectedBoat.shed * shedSpacing) + shedStart;
            if (selectedBoat.row == 2) posX += rowSpacing;
            int posY = 100;
            posY += ((selectedBoat.Column - 1) * columnSpacing) + columnStart;
            //Selected.Width = scale * imgBoatSizeX;
            //Selected.Height = scale * imgBoatSizeY;
            //Selected.Margin = new Thickness(posX * scale, posY * scale, 0, 0);
            Selected.Margin = new Thickness(-500,-500 , 0, 0);

        }

        private void SetScale()
        {
            scale = Background.ActualWidth / (imgSizeX+0.0);
            //testingLabel.Content = page.Width;
        }

    }
}
