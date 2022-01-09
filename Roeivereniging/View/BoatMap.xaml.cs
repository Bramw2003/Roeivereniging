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
        private double shedStart = 38; //40
        private double columnStart = 66;

        private double shedSpacing = 502;
        private double rowSpacing = 280;
        private double columnSpacing = 465;

        private Boat selectedBoat = new Boat(102,"test",4,1,false,false,"4-3-2-2");

        private double imgSizeX = 4028;
        //private double imgSizeY = 1235;

        private double imgBoatSizeX = 134;
        //private double imgBoatSizeY = 426;

        public double desiredWidth = 1000;

        private double scale = 1.0;

        private BitmapImage boatSingleImage = new BitmapImage(new Uri(@"Resources/boatSingle.png", UriKind.Relative));
        private BitmapImage boatDoubleImage = new BitmapImage(new Uri(@"Resources/boatDouble.png", UriKind.Relative));
        public BoatMap()
        {
            
            InitializeComponent();

            SetScale();
            SetSelectionPosition();


        }
        
        public void SetSelectedBoat(Boat boat)
        {
            selectedBoat = boat;
            if (boat.capacity == 8) Selected.Source = boatDoubleImage;
            else Selected.Source = boatSingleImage;
            SetSelectionPosition();

        }

        private void SetSelectionPosition()
        {
            double posX = 0;
            posX += (selectedBoat.shed-1) * shedSpacing + shedStart;
            if (selectedBoat.row == 2) posX += rowSpacing;
            double posY = 0;
            posY += ((selectedBoat.Column - 1) * columnSpacing) + columnStart;

            Canvas.SetLeft(Selected, posX * scale);
            Canvas.SetTop(Selected, posY *scale);

        }

        private void SetScale()
        {
            scale = desiredWidth / imgSizeX;
            Background.Width = desiredWidth;
            Selected.Width = (imgBoatSizeX / imgSizeX) * desiredWidth;
        }

    }
}
