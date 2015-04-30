using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace twelve
{
    class Filler3
    {
        int rank;
        double angle;
        int startN = 0;
        Point p;
        /// <summary>
        ///  список количества совпадение потом координаты
        /// </summary>
        public List<bool> mainboolList = new List<bool>();
        public List<Point> mainPointList = new List<Point>();
        public List<Point> redyPoints = new List<Point>();

        // для отладкиж
        int plus= 0, minus=0;
        List<double> test = new List<double> {0, 30, 90,210};
        public bool search()
        {
            // узнаем глубину если (4-1 rank ) тогда делаем первую проверку на совпадение с (0 0)
            startN++;
            if(startN==rank){

                foreach (Point item in redyPoints)
                {
                    if (item == mainPointList.Last())
                    {
                        var t = 0;
                    }    
                }
                //for (int i = 0; i < 360; i+=30)
                //{
                //    Point p = newPoint(i);
                //}
               
                plus++;
                return true;
            }
            else
            {
                // для начала строим путь
                double degree = test[startN];
                p = newPoint(ref mainPointList,degree);
                search();
                minus++;
                return false;
            }
        }
        public Point newPoint(ref List<Point> arr,double degree)
        {
           
            double angle2 = Math.PI * degree / 180.0;
            double sinAngleX = Math.Sin(angle2);
            double cosAngleY = Math.Cos(angle2);
            Point p = new Point(sinAngleX, cosAngleY);
            arr.Add(p);
            //double xx = Math.Sin(a * Math.PI / 180);
            //listSinCos.Add(a, xx);
            //double yy = Math.Cos(a * Math.PI / 180);
            //listSinCos.Add(a + 1, yy);

            //double x = curentPoint.X + Math.Sin(angle * Math.PI / 180);
            //double y = curentPoint.Y - Math.Cos(angle * Math.PI / 180);
         //   Point p = new Point(x, y);
            //double x = curentPoint.X + Math.Sin(angle * Math.PI / 180);
            //double y = curentPoint.Y - Math.Cos(angle * Math.PI / 180);
            return p;
        }
        public Point newPoint( double degree)
        {

            double angle2 = Math.PI * degree / 180.0;
            double sinAngleX = Math.Sin(angle2);
            double cosAngleY = Math.Cos(angle2);
            Point p = new Point(sinAngleX, cosAngleY);
            //double xx = Math.Sin(a * Math.PI / 180);
            //listSinCos.Add(a, xx);
            //double yy = Math.Cos(a * Math.PI / 180);
            //listSinCos.Add(a + 1, yy);

            //double x = curentPoint.X + Math.Sin(angle * Math.PI / 180);
            //double y = curentPoint.Y - Math.Cos(angle * Math.PI / 180);
            //   Point p = new Point(x, y);
            //double x = curentPoint.X + Math.Sin(angle * Math.PI / 180);
            //double y = curentPoint.Y - Math.Cos(angle * Math.PI / 180);
            return p;
        }

        public int quartern()
        {
            int a=0;

            return a;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rank">количество поколений сейчас 4 для квадрата</param>
        /// <param name="angle">статичный градус сейчас 90</param>
        public Filler3(int rank=4,double angle=90)
        {
            this.rank = rank;
            this.angle = angle;
            p = new Point(0, 0);
            mainPointList.Add(p);

            // заполнение готовых точек для проверки в самом низу дерева
            for (double i = 0; i < 360; i+=0.5)
            {
                newPoint(ref redyPoints, i);
            }

        }
    }
}
