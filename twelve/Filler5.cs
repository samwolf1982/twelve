using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace twelve
{
    class Filler5
    {
        int rank;
        int curentN; // текущая глубина отсчет с нуля; 0== первая точка
        /// <summary>
        /// все точки для дерева
        /// </summary>
        Point[] mainPoins2;
        Canvas cv = new Canvas();
        Random rand = new Random();
        double[] iii = new double[] {0, 90, 180, 270,90};
      //  List<Point> curentWay = new List<Point>();
        double degree; // default 
        bool down = false;
      public  List<Point[]> mainColections = new List<Point[]>();



        /// <summary>
        /// смещение все точек на -х и -у 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="p"></param>
        public void shiftArray(ref Point[] arr, Point p)
        {
          
            for (int i = 0; i <=curentN; i++)
            {
                arr[i].X -= p.X; ;// p.X;
                arr[i].Y -= p.Y; ;
            }
           // arr[curentN].X = 0; // p.X;
            //arr[curentN].Y =0 ;
        }
        /// <summary>
        /// смещение все точек на -х и -у 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="p"></param>
        public void shiftArray2(ref Point[] arr, Point p)
        {

            for (int i = 0; i < curentN; i++)
            {
                arr[i].X -= p.X; ;// p.X;
                arr[i].Y -= p.Y; ;
            }
            // arr[curentN].X = 0; // p.X;
            //arr[curentN].Y =0 ;
        }

        public void start()
        {
            mainPoins2[curentN] = new Point(0, 0);
            if (search() == true)
            {
                var t = 0;
                // catch !!!!!!!!!
            }
   
        }
        public bool search()
        {
            //if (search() == true)
            //{
            //    // попался ктото снизу
            //    var t = 0;
            //    // catch !!!!!!!!!
            //}
            //else
            //{
         
                // если в самом низу но еще надо одну линию тогда todo 
                if (curentN == rank-1)
                {
                    //         at down !!
                    var d = 0;
                    // перебор всех линий поиск совпадение на mainPoins2[0]
                    // test draw
                    //     проверить 
                    double degree2=((360/degree)-2)* (degree);
                    //curentN++;  // опускаемся на уровень ниже для поиска последней точки
                    for (double i = degree; i <degree2; i+=degree)
                    {
                        
                        Point fPoint = mainPoins2[0];
                        Point lPoint = newPoint(i);

                        mainPoins2[curentN] = lPoint;
                      //  shiftArray(ref mainPoins2, mainPoins2[curentN]);

                        mainColections.Add(mainPoins2.ToArray());
                        if (mainColections.Count == 10000)
                        {

                        }
                        if (fPoint == lPoint)
                        {
                            // bingo !!!
                            var r = 0;
                            return true;
                        }                       
                    }
                         // совпадений нету вверх
                    curentN--;
                    down = true;
                    return true;
                    //mainPoins2[curentN]=newPoint(180);
                    //shiftArray(ref mainPoins2, mainPoins2[curentN]);
          
     //  draw();
                    //scaleCurentFigure();
                    //shiftArray(ref mainPoins2, new Point(-200, -200));
                    //StreamGeometryTriangleExample(mainPoins2.ToList());
                }
                else
                {
                    // опускаемся на уровень ниже
                    if(down==false) curentN++; //to  down 1 Level
                    // cоздаем 3 состояния и запоминаем их
                    List<Point> lp = new List<Point>();
                    for (double i = 30; i <= 60; i += 30)
                    {
                        lp.Add(newPoint(i));
                    }
                    // проход по всем состояниям
                    foreach (var item in lp)
                    {
                        mainPoins2[curentN] = new Point(item.X, item.Y);
                        shiftArray(ref mainPoins2, mainPoins2[curentN]);
                        if (search() == true && down==true)
                        {
                            // если есть совпадения добалвлять фигуру идти више
                            var r = 99; //bingo!!!!
                        }
                    }

                    // после всего наверх
                    curentN--;
                    return true;
                   


            }
                curentN--;
            return true;
        }

        public Point newPoint(double degree)
        {

            double angle2 = Math.PI * degree / 180.0;
            double sinAngleX = Math.Sin(angle2);
            double cosAngleY = Math.Cos(angle2);
            Point p = new Point(sinAngleX, cosAngleY);
            return p;
        }

        public Filler5(Canvas can,int rank = 5,double deg=30)
        {
            this.rank = rank;
            this.degree = deg;
            curentN = 0;
            cv = can;
           mainPoins2  = new Point[rank];
        }


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
                arr[i] = new System.Drawing.PointF((float)mainPoins2[i].X,(float) mainPoins2[i].Y);
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

    }
}
