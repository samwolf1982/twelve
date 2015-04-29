using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace twelve
{
    class Tester
    {
        public void draw(Canvas canvas)
        { }
        public void RenderTargetBitmapExample()
        {

            System.Windows.Controls.Image myImage = new System.Windows.Controls.Image();
            //FormattedText text = new FormattedText("ABC",
            //        new CultureInfo("en-us"),
            //        FlowDirection.LeftToRight,
            //        new Typeface(this.FontFamily, FontStyles.Normal, FontWeights.Normal, new FontStretch()),
            //        this.FontSize,
            //        this.Foreground);

            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
         //   drawingContext.
     //  drawingContext.DrawText(text, new Point(2, 2));
            //  drawingContext.Close();

            RenderTargetBitmap bmp = new RenderTargetBitmap(180, 180, 120, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);
            myImage.Source = bmp;

            // Add Image to the UI
            StackPanel myStackPanel = new StackPanel();
            myStackPanel.Children.Add(myImage);
           //his.Content = myStackPanel;
        }
        //  canvas.Background=

        //  Bitmap  bitmap1 = new Bitmap(200, 200, pictureBox1.CreateGraphics());
        //   DrawingContext g = can  
    
    }
}
