using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace twelve
{
    class Filler4
    {
      //  List<Point> mainPoins = new List<Point>();
        Point[] mainPoins2 = new Point[12];
        public void test()
        {
            for (int j = 0; j < mainPoins2.Length; j++)
            {
            //     for (int i = 30; i <=330; i+=30)
            //{
                mainPoins2[j]=(newPoint(j*30));
           // }
            }
           
            System.Diagnostics.Debug.WriteLine("Before:");
            foreach (var item in mainPoins2)
            {
                System.Diagnostics.Debug.WriteLine(item.ToString());
            }
            System.Diagnostics.Debug.WriteLine("After:");

            shiftArray(ref mainPoins2, new Point(1, 1));
            foreach (var item in mainPoins2)
            {
                System.Diagnostics.Debug.WriteLine(item.ToString());
            }

        }

        /// <summary>
        /// смещение все точек на -х и -у 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="p"></param>
        public void shiftArray(ref Point[] arr , Point p)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].X -= p.X; ;// p.X;
                arr[i].Y -= p.Y; ;
            }           
        }
        // 
        public Point newPoint(double degree)
        {

            try
            {
             double angle2 = Math.PI * degree / 180.0;
            double sinAngleX = Math.Sin(angle2);
            double cosAngleY = Math.Cos(angle2);
            Point p = new Point(sinAngleX, cosAngleY);
            return p;
            }
            catch (Exception ex)
            {
                
                throw;
            }
     
        }
    }
}
