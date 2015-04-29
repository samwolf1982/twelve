﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media; 
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace twelve
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rand = new Random();
     
        Filler filler;
        int rank = 8;

        List<LittleShape2> curentList = new List<LittleShape2>();
        int curentindex = 0;
        public MainWindow()
        {
            // добавить красивые кнопки 495
            // красывый прогрес бар 505
            InitializeComponent();
        }
  
        /// <summary>
        ///  обработчик кнопки Начало 
        /// </summary>
        /// <param name="sender">sys</param>
        /// <param name="e">sys</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // поиск активного таб и заполнение


        //////////////////////////// компоновка
            filler = new Filler();
            filler.init();
  
                String str = "";
              //  str = "Время поиска: " + filler.sp.Seconds + "s.\n Найдкно: "+filler.figureColections.Count.ToString() + "\n area: " + filler.couuntPlosh.ToString();
                str = "Время поиска: " + filler.sp.Seconds + "s.\n Найдкно: " + filler.mainList.Count.ToString() + "\n количество вызовов area: " + filler.couuntPlosh.ToString();
                FlowDocument flowDoc = new FlowDocument(new Paragraph(new Run(str)));
                textik.Document = flowDoc;
                tabControl1.SelectedIndex = 0;

                if (filler == null || filler.mainList.Count == 0) return;
                curentList = selectfun(1);
                curentindex = 0;


        }

        /// <summary>
        ///     clear
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but2_Click(object sender, RoutedEventArgs e){
            Canvas curC = cuprentPicture();
            curC.Children.Clear();
        }
        /// <summary>
        ///  предыдущая фигура
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but3_Click(object sender, RoutedEventArgs e)
        {
        //    очистка екрана
            pic.Children.Clear();
            ///// одна фигура для рисования будет братmся из curentList
            LittleShape2 temp2 = new LittleShape2();
            // смотрим где указатель на текущюю фигуру из curentList
            if (curentindex == 0) {
        temp2=    curentList.Count != 0 ? curentList[curentindex].Clone() as LittleShape2 : null;
            }
            else { 
         temp2=       curentList.Count != 0 ? curentList[--curentindex].Clone() as LittleShape2 : null;
        }        
        if (temp2 == null)
            {
                String str2 = "Нету фигур для отображение";
                FlowDocument flowDoc2 = new FlowDocument(new Paragraph(new Run(str2)));
                textik.Document = flowDoc2;
                return;
            }

            //увеличиваю фигуру
            myMatrixTransformScale(ref temp2);
            // cмещаеть все линия по ху и добавлятеься в canvac
            // cледующий клик все чистит и утечки памяти нету, проверено.
            // дефолтное значение 
            // массив точок увеличеной  фигуры
            List<System.Windows.Point> arrpoint = new List<System.Windows.Point>();
            ////////////////////////
            foreach (var item in temp2.path)
            {
                LineGeometry blackLineGeometry = new LineGeometry();
               // смещение линии по ху
                Line l = moveLine(item);
                arrpoint.Add(new System.Windows.Point(l.X1, l.Y1));
                arrpoint.Add(new System.Windows.Point(l.X2, l.Y2));

            }

            StreamGeometryTriangleExample(arrpoint);
            // инфо про фигурку
            String str = "";
            str = "Maccа фигуры: " + temp2.Mass + "\nТекущый индекс: " + curentindex.ToString() + "\n Количество фигур: " + curentList.Count.ToString();
            FlowDocument flowDoc = new FlowDocument(new Paragraph(new Run(str)));
            textik.Document = flowDoc;
        }


        /// следующая фигура
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but4_Click(object sender, RoutedEventArgs e)
        {
         
           pic.Children.Clear();
            LittleShape2 temp2 = new LittleShape2();
               if (curentindex == curentList.Count)  { curentindex = 0;  }   
                 temp2  = curentList.Count!=0?curentList[curentindex++].Clone() as LittleShape2 :null;

         if (temp2 == null) {
            
            String str2 = "Нету фигур для отображение";
             FlowDocument flowDoc2 = new FlowDocument(new Paragraph(new Run(str2)));
             textik.Document = flowDoc2;
             
             return; }

               myMatrixTransformScale(ref temp2);
                        // cмещаеть все линия по ху и добавлятеься в canvac
                         // cледующий клик все чистит и утечки памяти нету, проверено.
  // дефолтное значение

                     List<System.Windows.Point> arrpoint = new List<System.Windows.Point>();
            ////////////////////////
                     foreach ( var item in temp2.path)
                     {
                         LineGeometry blackLineGeometry = new LineGeometry();
   
                         
                         Line l = moveLine(item);
                         arrpoint.Add(new System.Windows.Point(l.X1, l.Y1));
                         arrpoint.Add(new System.Windows.Point(l.X2, l.Y2));

                     }
                     StreamGeometryTriangleExample(arrpoint);



            // инфо про фигурку
                     String str = "";
                     str = "Maccа фигуры: " + temp2.Mass + "\nТекущый индекс: "+curentindex.ToString()+"\n Количество фигур: " + curentList.Count.ToString();
                     FlowDocument flowDoc = new FlowDocument(new Paragraph(new Run(str)));
                     textik.Document = flowDoc;
             

        }
 
        /// <summary>
        /// смещение линии по осям
        /// </summary>
        /// <param name="line"> передача по ссилке</param>
        /// <param name="y"></param>
        /// /// <param name="y"></param>
        /// <returns></returns>
        public Line moveLine(Line t, int resizeX = 350, int resizeY = 250)
        {
            Line item = t;
            item.X1 += resizeX;
            item.X2 += resizeX;
            item.Y2 += resizeY;
            item.Y1 += resizeY;
            return item;
        }
       /// <summary>
       /// фун для матричного преобразования в даном случае увеличения
       /// </summary>
       /// <param name="temp"></param>
        public void myMatrixTransformScale(ref LittleShape2 temp)
        {
            System.Drawing.Drawing2D.Matrix matrix = new System.Drawing.Drawing2D.Matrix();
            System.Drawing.Drawing2D.Matrix test = new System.Drawing.Drawing2D.Matrix(1, 0, 0, 1, 0, 0);
            // трансформация  из double[,] в pointF[]
            PointF[] arr = temp.getPointF(rank);
    // увеличение 
            matrix.Scale(50, 50);   
            // применение увеличения
            matrix.TransformPoints(arr);
            // уже увеличеная фигура (точки)
            temp.setPointF(arr);
        }
        /// <summary>
        /// виборка всех елементов с определенной масой
        /// доделать проверку на углы
        /// </summary>
        /// <param name="mass"></param>
        public List<LittleShape2> selectfun(int m)
        {
           
            List<LittleShape2> res2 = new List<LittleShape2>();

          //  var query = filler.figureColections.Where(x => x.Mass == mass).ToList();
            var query2 = filler.mainList.Where(x => x.Mass == m).ToList();
                       //     fun(){
                                         // sin<
           ///////////
        //}
        //    query2.Where();// > 
        //////////////todo
           
            return query2;

        }

        // cмена вкладки
 

      

        private void tabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl) //if this event fired from TabControl then enter
            {
                if (t1.IsSelected)
                {
                    if (filler==null|| filler.mainList.Count == 0) return;
                    curentList = selectfun(1);
                    curentindex = 0;
                    //Do your job here
                    System.Diagnostics.Debug.WriteLine("Tab,change T1");
                    // инфо про фигурку
                  
                   // curentList = selectfun(1);
                }
                if (t2.IsSelected)
                {
                    //Do your job here
                    System.Diagnostics.Debug.WriteLine("Tab,change T2");
                    if (filler == null || filler.mainList.Count == 0) return;
                    curentList = selectfun(2);
                    curentindex = 0;
                  //  drawCountShape(curentList.Count);
                }
                if (t3.IsSelected)
                {
                    //Do your job here
                    System.Diagnostics.Debug.WriteLine("Tab,change T3");
                    if (filler==null|| filler.mainList.Count == 0) return;
                    curentList = selectfun(3);
                    curentindex = 0;
                }
                if (t4.IsSelected)
                {
                    //Do your job here
                    System.Diagnostics.Debug.WriteLine("Tab,change T3");
                    if (filler==null|| filler.mainList.Count == 0) return;
                    curentList = selectfun(4);
                    curentindex = 0;
                }
                if (t5.IsSelected)
                {
                    //Do your job here
                    System.Diagnostics.Debug.WriteLine("Tab,change T3");
                    if (filler==null|| filler.mainList.Count == 0) return;
                    curentList = selectfun(5);
                    curentindex = 0;
                }
                if (t6.IsSelected)
                {
                    //Do your job here
                    System.Diagnostics.Debug.WriteLine("Tab,change T3");
                    if (filler==null|| filler.mainList.Count == 0) return;
                    curentList = selectfun(6);
                    curentindex = 0;
                }
                if (t7.IsSelected)
                {
                    //Do your job here
                    System.Diagnostics.Debug.WriteLine("Tab,change T3");
                    if (filler==null|| filler.mainList.Count == 0) return;
                    curentList = selectfun(7);
                    curentindex = 0;
                }
                if (t8.IsSelected)
                {
                    //Do your job here
                    System.Diagnostics.Debug.WriteLine("Tab,change T3");
                    if (filler==null|| filler.mainList.Count == 0) return;
                    curentList = selectfun(8);
                    curentindex = 0;
                }
                if (t9.IsSelected)
                {
                    //Do your job here
                    System.Diagnostics.Debug.WriteLine("Tab,change T3");
                    if (filler==null|| filler.mainList.Count == 0) return;
                    curentList = selectfun(9);
                    curentindex = 0;
                }
           
            }
             
          
        }
        public Canvas cuprentPicture()
        {
            Canvas test = new Canvas();
            if (t1.IsSelected)
            {
                pic.Children.Clear();
                return pic;
            }
            if (t2.IsSelected)
            {
                pic2.Children.Clear();
                return pic2;
            }
            if (t3.IsSelected)
            {
                pic3.Children.Clear();
                return pic3;
            }
            if (t4.IsSelected)
            {
                pic4.Children.Clear();
                return pic4;
            }
            if (t5.IsSelected)
            {
                pic5.Children.Clear();
                return pic5;
            }
            if (t6.IsSelected)
            {
                pic6.Children.Clear();
                return pic6;
            }
            if (t7.IsSelected)
            {
                pic7.Children.Clear();
                return pic7;

            }
            if (t8.IsSelected)
            {
                pic8.Children.Clear();
                return pic8;
            }
            if (t9.IsSelected)
            {
                pic9.Children.Clear();
                return pic9;
            }
            return test;

        }
  
 
     
        /// <summary>
        ///  вывод или в консоль + или в документ пока textik
        /// </summary>
        /// <param name="s"></param>
        /// <param name="x"></param>
        public void showinConsoleDebug(String s,bool x){
            if (x == true) System.Diagnostics.Debug.WriteLine(s);
            else
            {
             
                FlowDocument flowDoc = new FlowDocument(new Paragraph(new Run(s)));
                textik.Document = flowDoc;
            }
}
        /// <summary>
        /// обработчик ТЕСТ кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
          //  StreamGeometryTriangleExample();
          //  filler = new Filler();
            //filler.init();
            /////////////////////////////      заполнить mainList
          //  System.Diagnostics.Debug.WriteLine("Filler Ok testcolection: " + filler.testColection.Count.ToString());
            //showinConsoleDebug("Filler Ok testcolection: " + filler.testColection.Count.ToString(), false);
            //Filler2 obj=new Filler2();
            ////       
            //List<int> list=Enumerable.Range(1,4).Select(x=>x*0).ToList();
            //int range=5;
            //list[3] = range;
            //list[2] = 1;
            //list[1] = 1;
            //list[0] = 1;

            //obj.search(10 - 5,ref list);
            //showinConsoleDebug("Its OK \n", true);
            //foreach (var item in obj.result)
            //{
            //      string str= item[0].ToString()+" ";
            //      str += item[1].ToString() + " ";
            //      str += item[2].ToString() + " ";
            //      str += item[3].ToString() + " ";
            //      showinConsoleDebug(str + " \n", true);
             
            //}
            //showinConsoleDebug("Its OK \n", true);

            //foreach (var item in obj.testFun() )
            //{
            //    showinConsoleDebug("Num " + item.ToString()+"\n", true);
            //}
        //   showinConsoleDebug( "Fact: "+ Filler2.FactR(10).ToString(),true);
        }

 

        //}
        /// <summary>
        ///  рисовалка для фигуры
        /// </summary>
        /// <param name="arrPoints"></param>
          public void StreamGeometryTriangleExample(List<System.Windows.Point> arrPoints)
        {
            // Create a path to draw a geometry with.
            Path myPath = new Path();
            myPath.Stroke = System.Windows.Media.Brushes.Black;
            myPath.StrokeThickness = 1;
            System.Windows.Media.Color cl = new System.Windows.Media.Color();
              byte[] arr=new byte[4];
              rand.NextBytes(arr);
            //cl.A = 
              System.Windows.Media.Brush br = new SolidColorBrush(System.Windows.Media.Color.FromArgb(arr[0], arr[1], arr[2], arr[3]));
              myPath.Fill = br;
            // Create a StreamGeometry to use to specify myPath.
            StreamGeometry geometry = new StreamGeometry();
            geometry.FillRule = FillRule.EvenOdd;

            // Open a StreamGeometryContext that can be used to describe this StreamGeometry 
            // object's contents.

            using (StreamGeometryContext ctx = geometry.Open())
            {

                // Begin the triangle at the point specified. Notice that the shape is set to 
                // be closed so only two lines need to be specified below to make the triangle.
                ctx.BeginFigure(arrPoints[0], true /* is filled */, true /* is closed */);
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
            Canvas ss = cuprentPicture();
             ss.Children.Add(myPath);
        }
         
    }

}