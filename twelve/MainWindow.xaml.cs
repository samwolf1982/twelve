using System;
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
using MoreLinq;
using System.Threading;


namespace twelve
{
   
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<MyDataGridItem> items = new List<MyDataGridItem>();
        int miniindex = -1;
        int sec = 0;
        System.Windows.Threading.DispatcherTimer timer1 = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer timer2 = new System.Windows.Threading.DispatcherTimer();
       //  public event StatusChangedEventHandler StatusChanged;
        Random rand = new Random(); // для генерации цветов
        // Filler    Filler;  // главный объэкт
           Filler  FillerX;  // главный объэкт
        Thread thredValues = null;

        int rank = 6;  // количество граней

        List<LittleShape2> curentList = new List<LittleShape2>();  // сборка временых фигур
        int curentindex = 0; // указатель на фигуру что в даный момент отображаеться
        public MainWindow()
        {
            // добавить красивые кнопки 495
            // красывый прогрес бар 505

            InitializeComponent();
            lb.Content = rank;
            timer1.Tick += new EventHandler(dispatcherTimer_Tick);
            timer1.Interval = new TimeSpan(0, 0, 1);
            timer2.Tick += new EventHandler(dispatcherTimer2_Tick);
            timer2.Interval = TimeSpan.FromMilliseconds(0.2);

            //var items = new List<MyDataGridItem> {
            //    new MyDataGridItem {
            //        Image = new BitmapImage(new Uri("my_image.png", UriKind.Relative)),
            //        Description = "It's image"
            //    },
            //    new MyDataGridItem {
            //        Image = new BitmapImage(new Uri("my_image2.png", UriKind.Relative)),
            //        Description = "It's another image!"
            //    },
            //    new MyDataGridItem {
            //        Image = new BitmapImage(new Uri("my_image3.png", UriKind.Relative)),
            //        Description = "annoying images!"
            //    },
            //};

           // MyDataGridItem it = new MyDataGridItem();

       
       
           // it.Image = myCanvas;
           // grid.ItemsSource = items;
            //items.Add(new MyDataGridItem {
            //        Image = new BitmapImage(new Uri("my_image3.png", UriKind.Relative)),
            //        Description = "annoying22 images!"
            //    }
            //);
       //     timer1.Start();

         
            //Thread.CurrentThread.Name = "Main";
        }

        private void dispatcherTimer2_Tick(object sender, EventArgs e)
        {

            int total = 0;
            lock (Filler.locker)
            {
                total = Filler.mainListFigures.Count;
            }
            if (total == 0 || total == miniindex + 1) return;

            LittleShape2 lp = selectOneShape(++miniindex);
            var a = lp.anglesArr;

            List<System.Windows.Point> arrpoint = new List<System.Windows.Point>();
            ////////////////////////
            foreach (var item in lp.path)
            {
                LineGeometry blackLineGeometry = new LineGeometry();
                //    cмещение по канвасу
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                Line l = moveLine(item, 40, 50);
                arrpoint.Add(new System.Windows.Point(l.X1, l.Y1));
                arrpoint.Add(new System.Windows.Point(l.X2, l.Y2));

            }

            Canvas myCanvas = StreamGeometryTriangleLitle(arrpoint);
     //       myCanvas.Background = System.Windows.Media.Brushes.LightBlue;
            myCanvas.Width = 170;
            myCanvas.Height = 120;
            wrapP.Children.Add(myCanvas);   
        }

        /// <summary>
        ///  обработчик кнопки Начало 
        /// </summary>
        /// <param name="sender">sys</param>
        /// <param name="e">sys</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (timer2.IsEnabled) timer2.Stop();
            miniindex = -1;
            wrapP.Children.Clear();
            if (thredValues!=null &&thredValues.ThreadState == ThreadState.Running ) { thredValues.Abort(); }
            curentList.Clear();
            Filler.mainListFigures.Clear();
            curentindex = 0;
          
            thredValues = new Thread(  Filler.initialisation);
     
            //   Filler = null;
            int a = Int16.Parse(lb.Content.ToString());
               FillerX = new    Filler(a);
   //-      Filler.initialisation();
            thredValues.Start();

            sec = 0;
            timer1.Start();
            timer2.Start();

           
            tabControl1.SelectedIndex = 0;

            if ( FillerX == null || Filler.mainListFigures.Count == 0) return;
            curentList = selectfun(1);
            curentindex = 1;

        

        }

        /// <summary>
        ///     clear
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but2_Click(object sender, RoutedEventArgs e)
        {
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
       
            // смотрим где указатель на текущюю фигуру из curentList

            if (curentindex <= 0 || curentList.Count == 0) { return; }
            //    очистка екрана
            pic.Children.Clear();
            ///// одна фигура для рисования будет братmся из curentList
            LittleShape2 temp2 = new LittleShape2();

            temp2 = curentList[--curentindex].Clone() as LittleShape2;
               
 
           


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
            figureInfo(temp2);
        }


        /// следующая фигура
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but4_Click(object sender, RoutedEventArgs e)
        {

         
        
            if (curentindex == curentList.Count - 1 || curentindex > curentList.Count || curentindex<0) return;
            
            LittleShape2 temp2 = new LittleShape2();
            pic.Children.Clear();
            temp2 = curentList[++curentindex].Clone() as LittleShape2;
            //{
              //  curentindex = 0; curentList.Count != 0 ? curentList[curentindex].Clone() as LittleShape2 : null;
            //}
            //else
            //temp2 = curentList.Count != 0 ? curentList[++curentindex].Clone() as LittleShape2 : null;


            //LittleShape2 temp2 = new LittleShape2();
            //if (curentindex == curentList.Count-1 || curentindex > curentList.Count) {
            //    curentindex = 0; temp2 = curentList.Count != 0 ? curentList[curentindex].Clone() as LittleShape2 : null; }
            //else  temp2 = curentList.Count != 0 ? curentList[++curentindex].Clone() as LittleShape2 : null;

            if (temp2 == null)
            {

                String str2 = "Нету фигур для отображение";
                FlowDocument flowDoc2 = new FlowDocument(new Paragraph(new Run(str2)));
                textik.Document = flowDoc2;

                return;
            }

            myMatrixTransformScale(ref temp2);
            // cмещаеть все линия по ху и добавлятеься в canvac
            // cледующий клик все чистит и утечки памяти нету, проверено.
            // дефолтное значение

            List<System.Windows.Point> arrpoint = new List<System.Windows.Point>();
            ////////////////////////
            foreach (var item in temp2.path)
            {
                LineGeometry blackLineGeometry = new LineGeometry();


                Line l = moveLine(item);
                arrpoint.Add(new System.Windows.Point(l.X1, l.Y1));
                arrpoint.Add(new System.Windows.Point(l.X2, l.Y2));

            }
            StreamGeometryTriangleExample(arrpoint);



            // инфо про фигурку
            figureInfo(temp2);


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
        /// фун для матричного преобразования в даном случае увеличения
        /// </summary>
        /// <param name="temp"></param>
        public void myMatrixTransformScaleMini(ref LittleShape2 temp)
        {
            System.Drawing.Drawing2D.Matrix matrix = new System.Drawing.Drawing2D.Matrix();
            System.Drawing.Drawing2D.Matrix test = new System.Drawing.Drawing2D.Matrix(1, 0, 0, 1, 0, 0);
            // трансформация  из double[,] в pointF[]
            PointF[] arr = temp.getPointF(rank);
            // увеличение 
            matrix.Scale(25, 25);
            // применение увеличения
     
            matrix.TransformPoints(arr);
           //  matrix.Shear(1, 1);
          //   matrix.TransformPoints(arr);
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

            //  var query =    Filler.figureColections.Where(x => x.Mass == mass).ToList();
           List<LittleShape2> query2=null ;
            lock ( Filler.locker)
            {
              query2  =    Filler.mainListFigures.Where(x => x.Mass == m).ToList();
            }
            List<LittleShape2> request = new List<LittleShape2>();
            List<LittleShape2> tempRequest = new List<LittleShape2>();
            //проверка всех углов 
            // checkbox
            //Две геометрические фигуры называются равными, если их можно совместить наложением.
            // как вариант будем одну фигуру вращать 
            // надо длину, угол и порядок следования 
            // сейчас есть 3 квадрата что позволяет думать что мы вроде делаем 3 лишних действия
            // ---------------------------------------------сразу искать не координаты а вектора фигур но  12в степени 12 еще никто не отменял
            if (cb.IsChecked == true && query2.Count > 0)
            {
                //фигуры из запроса надо теперь их обработать
                // для всех выставить  градусы
                foreach (var item in query2)
                {
                    LittleShape2 fig2 = item.Clone() as LittleShape2;
                    myMatrixTransformScale(ref fig2);
                    fig2.setAngleList(3);
                    request.Add(fig2);
                }

                List<int> index2 = new List<int>();
                // проход по всем фигурам кроме последней - будет сверяться две фигуры
                for (int i = 0; i < request.Count - 1; i++)
                {
                    if (request[i] == null) continue;

                    var x1 = request[i];   //фигура а


                    for (int j = i + 1; j < request.Count; j++) //беру все остальные фиг поочереди поворачивая и проверяю на совпадения
                    {
                        if (request[j] == null) continue;
                        var x2 = request[j];  // фигура следующая // временая фигура она будет или null или не будет
                        double[] temp = x2.anglesArr.ToArray();


                        for (int k = 0; k < x2.anglesArr.Count(); k++) //  здесь сравнение и поворот фигуры второй
                        {
                            //var a1 = x2.anglesArr.ToArray();
                            bool resEual = equalFun(x1.anglesArr, temp);  //проверка

                            if (resEual == true)//ok  okokokokokokokokokokokok
                            {
                                request[j] = null; //                      1111111111111111111111111
                                break;
                            }
                            else
                            {
                                temp = x2.nextAngle(k);
                            }


                        }

                    }

                    query2 = request.Where(x => x != null).ToList();

                }



            }

            return query2;

        }
        /// <summary>
        /// виборка одной фигуры по индексу
        /// </summary>
        /// <param name="mass"></param>
        public LittleShape2 selectOneShape(int m)
        {

              LittleShape2 query2 = null;
            lock (Filler.locker)
            {
                query2 = Filler.mainListFigures[m].Clone() as LittleShape2 ;
            }

            myMatrixTransformScaleMini(ref query2);

            query2.setAngleList(3);


            //List<LittleShape2> request = new List<LittleShape2>();
            //List<LittleShape2> tempRequest = new List<LittleShape2>();
            //проверка всех углов 
            // checkbox
            //Две геометрические фигуры называются равными, если их можно совместить наложением.
            // как вариант будем одну фигуру вращать 
            // надо длину, угол и порядок следования 
            // сейчас есть 3 квадрата что позволяет думать что мы вроде делаем 3 лишних действия
            // ---------------------------------------------сразу искать не координаты а вектора фигур но  12в степени 12 еще никто не отменял
            //if (cb.IsChecked == true && query2.Count > 0)
            //{
            //    //фигуры из запроса надо теперь их обработать
            //    // для всех выставить  градусы
            //    foreach (var item in query2)
            //    {
            //        LittleShape2 fig2 = item.Clone() as LittleShape2;
            //        myMatrixTransformScale(ref fig2);
            //        fig2.setAngleList(3);
            //        request.Add(fig2);
            //    }

            //    List<int> index2 = new List<int>();
            //    // проход по всем фигурам кроме последней - будет сверяться две фигуры
            //    for (int i = 0; i < request.Count - 1; i++)
            //    {
            //        if (request[i] == null) continue;

            //        var x1 = request[i];   //фигура а


            //        for (int j = i + 1; j < request.Count; j++) //беру все остальные фиг поочереди поворачивая и проверяю на совпадения
            //        {
            //            if (request[j] == null) continue;
            //            var x2 = request[j];  // фигура следующая // временая фигура она будет или null или не будет
            //            double[] temp = x2.anglesArr.ToArray();


            //            for (int k = 0; k < x2.anglesArr.Count(); k++) //  здесь сравнение и поворот фигуры второй
            //            {
            //                //var a1 = x2.anglesArr.ToArray();
            //                bool resEual = equalFun(x1.anglesArr, temp);  //проверка

            //                if (resEual == true)//ok  okokokokokokokokokokokok
            //                {
            //                    request[j] = null; //                      1111111111111111111111111
            //                    break;
            //                }
            //                else
            //                {
            //                    temp = x2.nextAngle(k);
            //                }


            //            }

            //        }

            //        query2 = request.Where(x => x != null).ToList();

            //    }



            //}

            return query2;

        }


        public bool equalFun(double[] a, double[] b)
        {

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i]) return false;
            }
            return true;
        }


        private void tabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (e.Source is TabControl) //if this event fired from TabControl then enter
            {
                if (t1.IsSelected)
                {
                    if ( FillerX == null ||    Filler.mainListFigures.Count == 0) return;
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
                    if ( FillerX == null ||    Filler.mainListFigures.Count == 0) return;
                    curentList = selectfun(2);
                    curentindex = 0;
                    //  drawCountShape(curentList.Count);
                }
                if (t3.IsSelected)
                {
                    //Do your job here
                    System.Diagnostics.Debug.WriteLine("Tab,change T3");
                    if ( FillerX == null ||    Filler.mainListFigures.Count == 0) return;
                    curentList = selectfun(3);
                    curentindex = 0;
                }
                if (t4.IsSelected)
                {
                    //Do your job here
                    System.Diagnostics.Debug.WriteLine("Tab,change T3");
                    if ( FillerX == null ||    Filler.mainListFigures.Count == 0) return;
                    curentList = selectfun(4);
                    curentindex = 0;
                }
                if (t5.IsSelected)
                {
                    //Do your job here
                    System.Diagnostics.Debug.WriteLine("Tab,change T3");
                    if ( FillerX == null ||    Filler.mainListFigures.Count == 0) return;
                    curentList = selectfun(5);
                    curentindex = 0;
                }
                if (t6.IsSelected)
                {
                    //Do your job here
                    System.Diagnostics.Debug.WriteLine("Tab,change T3");
                    if ( FillerX == null ||    Filler.mainListFigures.Count == 0) return;
                    curentList = selectfun(6);
                    curentindex = 0;
                }
                if (t7.IsSelected)
                {
                    //Do your job here
                    System.Diagnostics.Debug.WriteLine("Tab,change T3");
                    if ( FillerX == null ||    Filler.mainListFigures.Count == 0) return;
                    curentList = selectfun(7);
                    curentindex = 0;
                }
                if (t8.IsSelected)
                {
                    //Do your job here
                    System.Diagnostics.Debug.WriteLine("Tab,change T3");
                    if ( FillerX == null ||    Filler.mainListFigures.Count == 0) return;
                    curentList = selectfun(8);
                    curentindex = 0;
                }
                if (t9.IsSelected)
                {
                    //Do your job here
                    System.Diagnostics.Debug.WriteLine("Tab,change T3");
                    if ( FillerX == null ||    Filler.mainListFigures.Count == 0) return;
                    curentList = selectfun(9);
                    curentindex = 0;
                }

            }

            pic.Children.Clear();
            LittleShape2 temp2 = new LittleShape2();
            if (curentindex == curentList.Count) { curentindex = 0; }
            temp2 = curentList.Count != 0 ? curentList[curentindex].Clone() as LittleShape2 : null;

            if (temp2 == null)
            {

                String str2 = "Нету фигур для отображение";
                FlowDocument flowDoc2 = new FlowDocument(new Paragraph(new Run(str2)));
                textik.Document = flowDoc2;

                return;
            }

            myMatrixTransformScale(ref temp2);
            // cмещаеть все линия по ху и добавлятеься в canvac
            // cледующий клик все чистит и утечки памяти нету, проверено.
            // дефолтное значение

            List<System.Windows.Point> arrpoint = new List<System.Windows.Point>();
            ////////////////////////
            foreach (var item in temp2.path)
            {
                LineGeometry blackLineGeometry = new LineGeometry();


                Line l = moveLine(item);
                arrpoint.Add(new System.Windows.Point(l.X1, l.Y1));
                arrpoint.Add(new System.Windows.Point(l.X2, l.Y2));

            }
            StreamGeometryTriangleExample(arrpoint);



            // инфо про фигурку
            figureInfo(temp2);



        }

        public void figureInfo(LittleShape2 temp2)
        {
            String str = "";
            String str3 = "";
            temp2.setAngleList();
            if (temp2.anglesArr != null)
            {
                foreach (var item in temp2.anglesArr)
                {
                    str3 += item.ToString() + "° ";
                }
            }

            str = "Maccа фигуры: " + temp2.Mass + "\nТекущый индекс: " + curentindex.ToString() + "\n Количество фигур: " + curentList.Count.ToString() +
                "\nУглы: " + str3+"Status :"+ thredValues.ThreadState.ToString() ;
            ;

            FlowDocument flowDoc = new FlowDocument(new Paragraph(new Run(str)));
            textik.Document = flowDoc;
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


        #region Debug
         /// <summary>
        ///  вывод или в консоль + или в документ пока textik
        /// </summary>
        /// <param name="s"></param>
        /// <param name="x"></param>
        public void showinConsoleDebug(String s, bool x)
        {
            if (x == true) System.Diagnostics.Debug.WriteLine(s);
            else
            {
                string richText = new TextRange(textik.Document.ContentStart, textik.Document.ContentEnd).Text;

                richText += s;
                textik.Document.Blocks.Clear();
                FlowDocument flowDoc = new FlowDocument(new Paragraph(new Run(richText)));
                textik.Document = flowDoc;
            }
        }

        #endregion
       
        #region Все закоментированое
        /// <summary>
        /// обработчик ТЕСТ кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void Button_Click_2(object sender, RoutedEventArgs e)
        //{

        // pic.Children.Clear();
        //    FillerX8 obj = new    FillerX8(10);
        //// obj.addNewVector(4);
        // obj.addPointToVectorMainList2();
        // showinConsoleDebug("Vector Start", false);
        // //showinConsoleDebug(obj.showAllLength(obj.mainList2),false);

        // showinConsoleDebug(obj.test(), false);

        // var po = Math.Pow(12, 6);
        // var maxval = long.MaxValue;

        // for (UInt64 i = 0; i < po; i++)
        // {
        //     var res = obj.alif4(0);
        // }

        //           for (long i = 0; i < po; i++)
        //           {
        //Thread tr = new Thread(obj.startThread);
        //tr.Start();
        ////tr.Join();

        //           }



        //    #region ДЛя рисования разкоментировать
        ////         curentList=   obj.draw2();
        //// curentindex = 0;
        //// if (curentList.Count <= 0) { return; }
        ////var temp2 = curentList[curentindex];
        //// myMatrixTransformScale(ref temp2);


        //// List<System.Windows.Point> arrpoint = new List<System.Windows.Point>();
        //// ////////////////////////
        //// foreach (var item in temp2.path)
        //// {
        ////     LineGeometry blackLineGeometry = new LineGeometry();


        ////     Line l = moveLine(item);
        ////     arrpoint.Add(new System.Windows.Point(l.X1, l.Y1));
        ////     arrpoint.Add(new System.Windows.Point(l.X2, l.Y2));

        //// }
        //// StreamGeometryTriangleExample(arrpoint);
        //// showinConsoleDebug("Vector Finish", false);      
        ////}
        //    #endregion

        #endregion



        //}

        /// <summary>
        ///  рисовалка для фигуры
        /// </summary>
        /// <param name="arrPoints"></param>
        public Canvas StreamGeometryTriangleLitle(List<System.Windows.Point> arrPoints)
        {
            // Create a path to draw a geometry with.
            Path myPath = new Path();
            myPath.Stroke = System.Windows.Media.Brushes.Black;
            myPath.StrokeThickness = 1;
            
            byte[] arr = new byte[4];
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
                    ctx.LineTo(arrPoints[i], true /* is stroked */, true /* is smooth join */);
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
          //  StackPanel mainPanel = new StackPanel();
           Canvas c = new Canvas();
           
        //    myPath.RenderTransform
            c.Children.Add(myPath);
            return c;
        }
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
     
            byte[] arr = new byte[4];
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
                    ctx.LineTo(arrPoints[i], true /* is stroked */, true /* is smooth join */);
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
           // StackPanel mainPanel = new StackPanel();
            Canvas ss = cuprentPicture();
            ss.Children.Add(myPath);
        }
        /// <summary>
        /// обработчкик для кнопки Next
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void b1Next_Click(object sender, RoutedEventArgs e)
        {
            if (rank == 8) { rank = 10; lb.Content = rank; }
            else if (rank == 10) { rank = 12; lb.Content = rank; }
            else if (rank == 12) { rank = 8; lb.Content = rank; }

        }
        /// <summary>
        /// обработчкик для кнопки NextDraw
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        /// <summary>
        ///  обработчик снятия чека
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_Unchecked(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// установка чека
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_Checked(object sender, RoutedEventArgs e)
        {

            //#region rA
            //pic.Children.Clear();
            //LittleShape2 temp2 = new LittleShape2();
            //if (curentindex == curentList.Count) { curentindex = 0; }
            //temp2 = curentList.Count != 0 ? curentList[curentindex++].Clone() as LittleShape2 : null;

            //if (temp2 == null)
            //{

            //    String str2 = "Нету фигур для отображение";
            //    FlowDocument flowDoc2 = new FlowDocument(new Paragraph(new Run(str2)));
            //    textik.Document = flowDoc2;

            //    return;
            //}

            //myMatrixTransformScale(ref temp2);
            //// cмещаеть все линия по ху и добавлятеься в canvac
            //// cледующий клик все чистит и утечки памяти нету, проверено.
            //// дефолтное значение

            //List<System.Windows.Point> arrpoint = new List<System.Windows.Point>();
            //////////////////////////
            //foreach (var item in temp2.path)
            //{
            //    LineGeometry blackLineGeometry = new LineGeometry();


            //    Line l = moveLine(item);
            //    arrpoint.Add(new System.Windows.Point(l.X1, l.Y1));
            //    arrpoint.Add(new System.Windows.Point(l.X2, l.Y2));

            //}
            //StreamGeometryTriangleExample(arrpoint);



            //// инфо про фигурку
            //figureInfo(temp2);
            //#endregion

            //var r = tabControl1.SelectedItem;
            //var ind = tabControl1.SelectedIndex;

            //if ( Filler == null ||    Filler.mainListFigures.Count == 0) return;
            //curentList = selectfun(ind);
            //curentindex = 0;
            ////Do your job here
            //System.Diagnostics.Debug.WriteLine("Tab,change T1");
            // инфо про фигурку
        }

        private void num_Click(object sender, RoutedEventArgs e)
        {
            int a = Int16.Parse( lb.Content.ToString());
           a=a+2;
         //   a ++;
            int maxnum = 12;
            if (a > maxnum) { a = 6; }

            lb.Content = a;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (thredValues != null) { thredValues.Abort(); }
        }
   
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            sec++;
          String  str = "State : " + thredValues.ThreadState.ToString() +" Прошло: "+sec.ToString()+"s."  ;
  
            
       
            workerState.Content = str;
            if (thredValues.ThreadState == ThreadState.Running)
            {

            }
            if (thredValues.ThreadState == ThreadState.Stopped)
            {
                timer1.Stop();
            }

            //for (int i = 0; i < 50; i++)
            //{
            //    Line line = new Line();
            //    line.Stroke = System.Windows.Media.Brushes.Black;

            //    line.X1 = 0;
            //    line.X2 = 20;
            //    line.Y1 = 0;
            //    line.Y2 = 20;

            //    line.StrokeThickness = 5;
            //    Canvas myCanvas = new Canvas();
            //    myCanvas.Background = System.Windows.Media.Brushes.Red;

            //    myCanvas.Children.Add(line);
            //    wrapP.Children.Add(myCanvas);
            //}
        }

        // test
        private void test_Click(object sender, RoutedEventArgs e)
        {
            int total=0;
            lock (Filler.locker)
            {
             total=Filler.mainListFigures.Count;  
            }
            if(total==0 ||total==miniindex+1 ) return;
            
                LittleShape2 lp = selectOneShape(++miniindex);
            var a = lp.anglesArr;

            List<System.Windows.Point> arrpoint = new List<System.Windows.Point>();
            ////////////////////////
            foreach (var item in lp.path)
            {
                LineGeometry blackLineGeometry = new LineGeometry();
      //    cмещение по канвасу
      //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                Line l = moveLine(item,40,50);
                arrpoint.Add(new System.Windows.Point(l.X1, l.Y1));
                arrpoint.Add(new System.Windows.Point(l.X2, l.Y2));

            }
           
          Canvas myCanvas =     StreamGeometryTriangleLitle(arrpoint );
          myCanvas.Background = System.Windows.Media.Brushes.LightBlue;
          myCanvas.Width = 170;
          myCanvas.Height = 120;
          wrapP.Children.Add(myCanvas);   
        } 
    }

}
