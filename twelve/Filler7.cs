using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace twelve
{
    class Filler7
    {
        List<Point> mainPointList = new List<Point>();
      public  int couner = 0;
        public void start()
        {
            for (int a = 0; a < mainPointList.Count; a++)
            {
                   for (int b = 0; b < mainPointList.Count; b++)
                            {
                                for (int c = 0; c < mainPointList.Count; c++)
                                {
                                    for (int d = 0; d < mainPointList.Count; d++)
                                    {

                                       couner++;
                                        var x = 0 - mainPointList[a].X - mainPointList[b].X - mainPointList[c].X-mainPointList[d].X;
                                        var y = 0 - mainPointList[a].Y - mainPointList[b].Y - mainPointList[c].Y-mainPointList[d].Y;

                                        foreach (var item in mainPointList)
                                        {
                                            int v = 0;
                                            var itX=Math.Round( item.X,v);
                                            var nx=Math.Round( x,v);
                                            var itY= Math.Round( item.Y,v);
                                            var ny=Math.Round( y,v);
                                            if ( itX== nx && itY == ny)
                                            {
                                                var t = 0;
                                            }
                                        }
                                    }
                                    //-------

                                }

                            }
            }
        }

        #region Initial
        /// <summary>
        /// новая точка
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public Point newPoint(double degree)
        {

            double angle2 = Math.PI * degree / 180.0;
            double sinAngleX = Math.Sin(angle2);
            double cosAngleY = Math.Cos(angle2);
            Point p = new Point(sinAngleX, cosAngleY);
            return p;
        }

        public Filler7()
        {
            for (double i = 10; i < 360; i+=10)
            {
                mainPointList.Add(newPoint(i));

            }
        }
        #endregion
  

    }
}
