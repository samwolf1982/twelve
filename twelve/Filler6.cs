using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace twelve
{
    class Filler6
    {
        #region Values
        int rank;
        int curentN; // текущая глубина отсчет с нуля; 0== первая точка
        /// <summary>
        /// все точки для дерева
        /// </summary>
        Point[] mainPoins2;
        Canvas cv = new Canvas();
        Random rand = new Random();
        double[] iii = new double[] { 0, 90, 180, 270, 90 };
        //  List<Point> curentWay = new List<Point>();
        double degree; // default 
        bool down = false;
        public List<Point[]> mainColections = new List<Point[]>();
        public List<Point> tempPopintList = new List<Point>();
        public List<Point> colectPointForZero = new List<Point>();
        #endregion

        public void start()
        {
            alif(90);

        }

        public bool alif(double degree)
        {
            if (tempPopintList.Count < rank)
            {
                Point p = newPoint(degree);
                tempPopintList.Add(p);
                shiftList(ref tempPopintList, p);     // cмещение по осям на ноль послед точку
                alif(degree);
            }

            if(tempPopintList.Count==rank)     // проверка 0 и последнего елемента
            {
                    //double degree2=((360/30)-2)* (30); // количество град/угол  30 to 330
            if( colectPointForZero.Exists(x=>x==tempPopintList[0])) // проверка на попадание
            {
                var ff = 99;
            }
                    
                
             
            }
            return true;

        }
        //public Point[] alif1(double degree)
        //{
        //    Point p = newPoint(degree);
        // //   Point[] arr = new Point[rank];

                
        //}
        public Point search(ref Point[] arr,Point p, double angle, int level)
        {
            if (level == rank) // first check - at down
            {
                Point res1 = newPoint(angle);// последняя точка должна совпадать с первой если фигура ОК
                // если совпадение тогда фигура закрыта и возврат точки
                if (arr[0] == res1) return res1;
                else return new Point();  // иначе пустая точка (0 0)
            }
            if(level<rank)
            { // копаем глубже
                  Point res = newPoint(angle);
                  Point other=search(ref arr, res, angle, level++);  // точка с низу
                if (other != new Point()) // возврат не пустой точки ок
            {
                // 
            }
            }
          

            return new Point() ;
        }


        /// <summary>
        /// первая проверка в самом низу на 
        /// </summary>
        /// <returns>+ it's ok point is</returns>
        public bool firstCheck(   Point[] mainPoins2,Point p)
        {
            foreach (var item in mainPoins2)
            {
                if (item == p) return true;
            }
            return false;

        }
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

        #region Shift
        /// <summary>
        /// смещение все точек на -х и -у 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="p"></param>
        public void shiftArray(ref Point[] arr, Point p)
        {

            for (int i = 0; i <= curentN; i++)
            {
                arr[i].X -= p.X; ;// p.X;
                arr[i].Y -= p.Y; ;
            }
            // arr[curentN].X = 0; // p.X;
            //arr[curentN].Y =0 ;
        }

        public void shiftList(ref List<Point> arr, Point p)
        {

            for (int i = 0; i < arr.Count; i++)
            {
                var x=  arr[i].X - p.X; ;// p.X;
                var y= arr[i].Y - p.Y; ;
               arr[i] = new Point(x, y);
            }
        }
        ///// <summary>
        ///// смещение все точек на -х и -у 
        ///// </summary>
        ///// <param name="arr"></param>
        ///// <param name="p"></param>
        //public void shiftArray2(ref Point[] arr, Point p)
        //{

        //    for (int i = 0; i < curentN; i++)
        //    {
        //        arr[i].X -= p.X; ;// p.X;
        //        arr[i].Y -= p.Y; ;
        //    }
        //    // arr[curentN].X = 0; // p.X;
        //    //arr[curentN].Y =0 ;
        //}
        #endregion
        #region для рисования

        public void drawOfIndex(int index)
        {
            scaleCurentFigure();
            Point[] temp = new Point[mainColections[index].Length];
            temp = mainColections[index].ToArray();
            scaleCurentFigure2(ref temp);
            shiftArray(ref temp, new Point(-200, -200));
            //   shiftArray2(ref mainPoins2, new Point(-200, -200));
            StreamGeometryTriangleExample(temp.ToList());
        }
        public void draw()
        {
            scaleCurentFigure();
            shiftArray(ref mainPoins2, new Point(-200, -200));
            //   shiftArray2(ref mainPoins2, new Point(-200, -200));
            StreamGeometryTriangleExample(mainPoins2.ToList());
        }

        public void scaleCurentFigure2(ref Point[] p)
        {
            System.Drawing.Drawing2D.Matrix matrix = new System.Drawing.Drawing2D.Matrix();
            System.Drawing.Drawing2D.Matrix test = new System.Drawing.Drawing2D.Matrix(1, 0, 0, 1, 0, 0);
            // трансформация  из double[,] в pointF[]
            System.Drawing.PointF[] arr = new System.Drawing.PointF[rank];

            for (int i = 0; i < p.Length; i++)
            {
                arr[i] = new System.Drawing.PointF((float)p[i].X, (float)p[i].Y);
            }
            // увеличение 
            matrix.Scale(50, 50);
            // применение увеличения
            // matrix.Shear(-2, -2);
            matrix.TransformPoints(arr);
            // уже увеличеная фигура (точки)
            for (int i = 0; i < p.Length; i++)
            {
                // arr[i] = new System.Drawing.PointF((float)mainPoins2[i].X, (float)mainPoins2[i].Y);
                p[i].X = arr[i].X;
                p[i].Y = arr[i].Y;
            }
            // setPointFToLinearr(arr);

        }
        public void scaleCurentFigure()
        {
            System.Drawing.Drawing2D.Matrix matrix = new System.Drawing.Drawing2D.Matrix();
            System.Drawing.Drawing2D.Matrix test = new System.Drawing.Drawing2D.Matrix(1, 0, 0, 1, 0, 0);
            // трансформация  из double[,] в pointF[]
            System.Drawing.PointF[] arr = new System.Drawing.PointF[rank];

            for (int i = 0; i < mainPoins2.Length; i++)
            {
                arr[i] = new System.Drawing.PointF((float)mainPoins2[i].X, (float)mainPoins2[i].Y);
            }
            // увеличение 
            matrix.Scale(50, 50);
            // применение увеличения
            // matrix.Shear(-2, -2);
            matrix.TransformPoints(arr);
            // уже увеличеная фигура (точки)
            for (int i = 0; i < mainPoins2.Length; i++)
            {
                // arr[i] = new System.Drawing.PointF((float)mainPoins2[i].X, (float)mainPoins2[i].Y);
                mainPoins2[i].X = arr[i].X;
                mainPoins2[i].Y = arr[i].Y;
            }
            // setPointFToLinearr(arr);

        }


        public void StreamGeometryTriangleExample(List<System.Windows.Point> arrPoints)
        {
            // Create a path to draw a geometry with.
            System.Windows.Shapes.Path myPath = new System.Windows.Shapes.Path();
            myPath.Stroke = System.Windows.Media.Brushes.Black;
            myPath.StrokeThickness = 1;
            System.Windows.Media.Color cl = new System.Windows.Media.Color();
            byte[] arr = new byte[4];
            rand.NextBytes(arr);
            //cl.A = 
            System.Windows.Media.Brush br = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(arr[0], arr[1], arr[2], arr[3]));
            #region для заполнение разкоментировать
            //  myPath.Fill = br;
            #endregion

            // Create a StreamGeometry to use to specify myPath.
            System.Windows.Media.StreamGeometry geometry = new System.Windows.Media.StreamGeometry();
            //   geometry.FillRule = System.Windows.Media.FillRule.EvenOdd;

            // Open a StreamGeometryContext that can be used to describe this StreamGeometry 
            // object's contents.

            using (System.Windows.Media.StreamGeometryContext ctx = geometry.Open())
            {

                // Begin the triangle at the point specified. Notice that the shape is set to 
                // be closed so only two lines need to be specified below to make the triangle.
                //ctx.BeginFigure(arrPoints[0], true /* is filled */, true /* is closed */);
                ctx.BeginFigure(arrPoints[0], true /* is filled */, false /* is closed */);
                for (int i = 1; i < arrPoints.Count; i++)
                {
                    ctx.LineTo(arrPoints[i], true /* is stroked */, false /* is smooth join */);
                }
                // Draw a line to the next specified point.
                //   ctx.LineTo(new System.Windows.Point(100, 100), true /* is stroked */, false /* is smooth join */);

                // Draw another line to the next specified point.
                //  ctx.LineTo(new System.Windows.Point(100, 50), true /* is stroked */, false /* is smooth join */);
            }

            // Freeze the geometry (make it unmodifiable)
            // for additional performance benefits.
            geometry.Freeze();

            // Specify the shape (triangle) of the Path using the StreamGeometry.
            myPath.Data = geometry;

            // Add path shape to the UI.
            StackPanel mainPanel = new StackPanel();
            Canvas ss = cv;
            ss.Children.Clear();
            ss.Children.Add(myPath);
        }
        #endregion
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="can"></param>
        /// <param name="rank"> для квадрата 4 точка</param>
        /// <param name="deg"></param>
              public Filler6(Canvas can,int rank = 4,double deg=30)
        {
            this.rank = rank;
            this.degree = deg;
            curentN = 0;
            cv = can;
           mainPoins2  = new Point[rank];
           for (double i = 0; i < 360; i++)
           {
               colectPointForZero.Add(newPoint(i));

           }

        }
        #endregion



    }
}
