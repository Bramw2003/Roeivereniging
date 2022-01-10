using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;
using Model;
namespace View
{
    /// <summary>
    /// Interaction logic for EditMapWindow.xaml
    /// </summary>
    public partial class EditMapWindow : Window
    {
        private Event @event;
        public EditMapWindow(Event @event)
        {
            InitializeComponent();
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("Events/map.png", UriKind.Relative));
            InkCanvas.Background = imageBrush;
            this.@event = @event;
        }

        private void Trash_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas.Strokes.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double width = InkCanvas.ActualWidth;
            double height = InkCanvas.ActualHeight;
            RenderTargetBitmap bmpCopied = new RenderTargetBitmap((int)Math.Round(width), (int)Math.Round(height), 96, 96, PixelFormats.Default);
            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(InkCanvas);
                dc.DrawRectangle(vb, null, new Rect(new System.Windows.Point(), new System.Windows.Size(width, height)));
            }
            bmpCopied.Render(dv);
            Bitmap bitmap;
            MemoryStream outStream = new MemoryStream();

            // from System.Media.BitmapImage to System.Drawing.Bitmap 
            BitmapEncoder enc = new BmpBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(bmpCopied));
            enc.Save(outStream);
            bitmap = new System.Drawing.Bitmap(outStream);


            EncoderParameter qualityParam =
         new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 85L);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            Bitmap btm = new Bitmap(bitmap);
            bitmap.Dispose();

            Model.DAO.EventsDAO eventDao = new Model.DAO.EventsDAO();
            //eventDao.UpdateMap(ImageToByte(btm), @event);
            outStream.Dispose();
            //btm.Save("C:\\Users\\bwdes\\Desktop\\test.jpg", jpegCodec, encoderParams);
            MemoryStream stream = new MemoryStream();
            btm.Save(stream, jpegCodec, encoderParams);
            btm.Save("C:\\Users\\bwdes\\Desktop\\test.jpg", jpegCodec, encoderParams);
            if (!eventDao.UpdateMap(stream.ToArray(), @event))
            {
                MessageBox.Show("Watt");
            }
            btm.Dispose();

        }
        public static byte[] ImageToByte(Bitmap img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                return stream.ToArray();
            }
        }
        private ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }
    }
}
