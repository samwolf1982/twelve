using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace twelve
{
 public   class Filler
    {
        #region Values
            static int count = 0;
         public   int rank ;
            public  double[,] mas ;//количество точек (сейчас 8)
            static UInt64[] intplosh = new UInt64[9];//количество для значений площади(максимальная площадь 6) //bitfield
        public int xcouunt=0;
     public   int couuntPlosh = 0;
     public List<List<double>> testColection = new List<List<double>>();

     // все фигури  
  public List<LittleShape2> mainList = new List<LittleShape2>();


  public List<double[,]> mainList2 = new List<double[,]>();
 //public List<LittleShape> figureColections=new List<LittleShape>();
 public TimeSpan sp;
     // 
 Dictionary<double, double> listSinCos = new Dictionary<double, double>();  // набор синусов и косинусо для определеных углов
        #endregion


 #region Debug Values
 public  double[,] debug1 ;//количество точек (сейчас 8)
 public  double[,] debug2 ;//количество точек (сейчас 8)
 #endregion
// отладка


 public Filler(int rank=8)
 {
     this.rank = rank;
    mas = new double[2, rank];
    debug1 = new double[2, rank];//количество точек (сейчас 8)
  debug2 = new double[2, rank];//количество точек (сейчас 8)
 }

        /// <summary>
        /// начальна инициализация
        /// </summary>
        public  void init ()
            {
                for (int i = 0; i < intplosh.Length; i++)// переделать!!! битовое поле
                    intplosh[i] = 0;
                //////елемент динамического програмирование
                for (double a = 0; a <= 330; a += 30) // бежим в 11 сторон
                {
                    double xx = Math.Sin(a * Math.PI / 180);
                    listSinCos.Add(a , xx);
                    double yy = Math.Cos(a * Math.PI / 180);
                    listSinCos.Add(a+1, yy);
                }
                // старт
                //* ч\н     номер линии изначально 0 0 0 1
                mas[0, 0] = 0;//начальные точки
                mas[0, 1] = 0;

                //2точка с х=0            
                mas[1, 0] = 0;

                //2точка с у=1  
                mas[1, 1] = 1;
                DateTime start = DateTime.Now;            
                //  int k;
                             search(2);        //вызов рекурсивной функции
            //    time work
                   sp = DateTime.Now -start;
            
            }
     /// <summary>
     ///  рекурсивная функция
     /// </summary>
     /// <param name="k"></param>
          public void search(int k)
            {
                //    перврый раз
                if (k == 2) // две начальные точки мы поставили, от второй точки, строим третья, для второго отрезка многоугольника, бежим в 6! сторон
                {
                    //int i = 0;
                   // for (double a = 30; a <= 180; a += 30)
                 //   double a = 30;
                      for (double a = 30; a <= 180; a += 30)
                    {
                        // окружность круга
                        double x = Math.Sin(a * Math.PI / 180); // нахождение новых координат
                        double y = 1 - Math.Cos(a * Math.PI / 180);
                        //////////////////
                        mas[0, k] = x; // постановка координат
                        mas[1, k] = y;
                        //t2 = mas;
                        search(k + 1); //  вызов рекурсивной функции
                       // ///
    
                    }
                }
                else
                {
                    /// к=3
                    for (double a = 0; a <= 330; a += 30) // бежим в 11 сторон
                    {
                        //                   берем значения син и кос из готового массива
                        double x = mas[0, k - 1] + listSinCos[a];
                        double y = mas[1, k - 1] -listSinCos[a+1] ;

        
                        // проверка на касание
                        bool Flag = false;

                        for (int i = 0; i < k - 1; i++)
                        {
                            if (Math.Abs(rasst(mas[0, i], mas[1, i], x, y)) < 0.001){
                                Flag = true; break;}
                            if (i > 0 && equal(mas[0, i - 1], mas[1, i - 1], mas[0, i], mas[1, i], mas[0, k - 1], mas[1, k - 1], x, y))
                            {
                                Flag = true; break;
                            }
                        }
                        #region предыдущий код
                                    //for (int i = 0; i < k - 1; i++)
                        //{
                        //     double pp = rasst(mas[0, i], mas[1, i], x, y);
                        //    double absPP = Math.Abs(pp);
                        //    if (absPP < 0.001)
                        //    {
                        //        Flag = true;
                        //        break;
                        //    }
                        //    ///////      делал только с проверкой не пересечение результат нехватка памяти
                        //    if (i > 0 && equal(mas[0, i - 1], mas[1, i - 1], mas[0, i], mas[1, i], mas[0, k - 1], mas[1, k - 1], x, y))
                        //    {
                        //        Flag = true;
                        //        break;
                        //    }
                        //    // если (касаеться +)   делал только с проверкой не пересечение результат нехватка памяти надо включать этот кусок
                        //    // будут отсекаться фигуры с меньшей пл. но будут лищние проверки 
                        //    //    пробую менять местами сначала на пересечение линий break потом на площадь тоже break
                         
                        //   // return;
                        //}
                        // если нету касания го 
                        #endregion
            
                        if (!Flag)
                        {
                            mas[0, k] = x;
                            mas[1, k] = y;
                         //   var test = mas.GetLength(1) - 1;
                            if (k < mas.GetLength(1) - 1)
            
                                search(k + 1);
                            else
                                if (Math.Abs(rasst(mas[0, k], mas[1, k], mas[0, 0], mas[1, 0]) - 1) < 0.01)
                                {
                                    //????????
                                    Flag = true;
                                    for (int i = 1; i < k - 1; i++)
                                    {
                                        debug1 = mas;
                                        if (equal(mas[0, i], mas[1, i], mas[0, i + 1], mas[1, i + 1], mas[0, k], mas[1, k], mas[0, 0], mas[1, 0]))
                                        {
                                            Flag = false; 
                                            break;
                                        }
                                    }
                                    if (Flag)
                                    {
                                        //****************************
          
                                        double intarea = area(mas);
                                         var abs=Math.Abs(Math.Round(intarea) - intarea);
                             couuntPlosh++;
                                        //////************

              

                                        if (abs < 0.00001)
                                        {
   
                                                    xcouunt++;
                                                    intplosh[(int)Math.Round(intarea)]++;
                                          
                                            ////*********////////////////////////////////
                                            double[] tempAppay =new double[rank];
               

                                            List<double> temp = new List<double>();
                                           

                                            double [,]tempArr=new double[2,rank];
                                            // копирование в новый масс для сохранения
                                            for (int i = 0; i < rank; i++)
                                            {
                                                  tempArr[0,i] = mas[0, i];
                                                  tempArr[1,i] = mas[1, i];
                                            }

                                            // сохранение массива 
                                            LittleShape2 ready = new LittleShape2();
                                            ready.add(tempArr,rank);
                                            //   установка масси
                                            //double areaF = area(mas);
                                        
                                   
                                       
                                            ready.Mass =  (int)Math.Round(intarea);;
                                            mainList.Add(ready);
                                            foreach (var item in mas)
                                            {
                                                temp.Add(item);
                                            }
                                       //  mas.CopyTo(tempAppay, 0);
                                            // здесь храняться все результаты поиска
                                            // для использования забить в в какойто PATH и расовать  проверки на массу нету
                                            // делать отдельно в другой части кода
                                            // если делать здесь то время поиска будет увеличиваться 
                                            // как вариатн можно результат поиска 12 и 10 записать в файл а потом считать для демонстрации 
                                            // работоспособности программы
                                            // хотя площадь фигуры все равно находиться по причине избежания переполнения 
                                            // значить сразу можно делать метку на массу как и было сделано в начале 
                                            // делать колекцию фигур с меткой масса
                                            testColection.Add(temp);

                                        }
                                        count++;

                                    }

                                }
                        }
        

                    }

                }


            }
          #region ЗАКОМЕНТИРОВАНО работающая search()  
          //public void search(int k)
          //{
          //    //    перврый раз
          //    if (k == 2) // две начальные точки мы поставили, от второй точки, строим третья, для второго отрезка многоугольника, бежим в 6! сторон
          //    {
          //        //int i = 0;
          //        // for (double a = 30; a <= 180; a += 30)
          //        //   double a = 30;
          //        for (double a = 30; a <= 180; a += 30)
          //        {
          //            // окружность круга
          //            double x = Math.Sin(a * Math.PI / 180); // нахождение новых координат
          //            double y = 1 - Math.Cos(a * Math.PI / 180);
          //            //  System.Diagnostics.Debug.WriteLine("X:Y:  "+x.ToString()+"  "+y.ToString()+"\n");
          //            // mas[2+i, 0] = x;
          //            // mas[2+i, 1] = y;
          //            // для дебага
          //            double[,] t1 = new double[2, rank];
          //            double[,] t2 = new double[2, rank];
          //            t1 = mas;
          //            //////////////////
          //            mas[0, k] = x; // постановка координат
          //            mas[1, k] = y;
          //            t2 = mas;
          //            search(k + 1); //3 4 5 - 6
          //            // ///


          //            ////
          //            // i++;
          //            // Console.ReadLine();
          //        }
          //    }
          //    else
          //    {
          //        /// к=3
          //        for (double a = 0; a <= 330; a += 30) // бежим в 11 сторон
          //        {
          //            //double xx=Math.Sin(a * Math.PI / 180);
          //            //  double yy = Math.Cos(a * Math.PI / 180);
          //            // оптимизировано создае  елемент для динамического програмирования  listSinCos


          //            double x = mas[0, k - 1] + listSinCos[a];
          //            double y = mas[1, k - 1] - listSinCos[a + 1];

          //            //   System.Diagnostics.Debug.WriteLine("X:Y:  " + xx.ToString() + "  " + yy.ToString() + "\n");
          //            // проверка на касание
          //            bool Flag = false;
          //            for (int i = 0; i < k - 1; i++)
          //            {
          //                double pp = rasst(mas[0, i], mas[1, i], x, y);
          //                double absPP = Math.Abs(pp);
          //                if (absPP < 0.001)
          //                {
          //                    Flag = true;
          //                    break;
          //                }
          //                ///////      делал только с проверкой не пересечение результат нехватка памяти
          //                if (i > 0 && equal(mas[0, i - 1], mas[1, i - 1], mas[0, i], mas[1, i], mas[0, k - 1], mas[1, k - 1], x, y))
          //                {
          //                    Flag = true;
          //                    break;
          //                }
          //                // если (касаеться +)   делал только с проверкой не пересечение результат нехватка памяти надо включать этот кусок
          //                // будут отсекаться фигуры с меньшей пл. но будут лищние проверки 
          //                //    пробую менять местами сначала на пересечение линий break потом на площадь тоже break

          //                // return;
          //            }
          //            // если нету касания го 
          //            if (!Flag)
          //            {
          //                mas[0, k] = x;
          //                mas[1, k] = y;
          //                var test = mas.GetLength(1) - 1;
          //                if (k < mas.GetLength(1) - 1)
          //                    //// ?????????? почему к другое
          //                    search(k + 1);
          //                else
          //                    if (Math.Abs(rasst(mas[0, k], mas[1, k], mas[0, 0], mas[1, 0]) - 1) < 0.01)
          //                    {
          //                        //????????
          //                        Flag = true;
          //                        for (int i = 1; i < k - 1; i++)
          //                        {
          //                            debug1 = mas;
          //                            if (equal(mas[0, i], mas[1, i], mas[0, i + 1], mas[1, i + 1], mas[0, k], mas[1, k], mas[0, 0], mas[1, 0]))
          //                                Flag = false;
          //                        }
          //                        if (Flag)
          //                        {
          //                            //****************************
          //                            //   double intarea = area(mas);
          //                            double intarea = 0;

          //                            couuntPlosh++;
          //                            //////************

          //                            //if (Math.Abs(intarea) < 0.01)

          //                            if (Math.Abs(Math.Round(intarea) - intarea) < 0.00001)
          //                            {

          //                                xcouunt++;


          //                                ////*********////////////////////////////////
          //                                double[] tempAppay = new double[rank];


          //                                List<double> temp = new List<double>();


          //                                double[,] tempArr = new double[2, rank];
          //                                // копирование в новый масс для сохранения
          //                                for (int i = 0; i < rank; i++)
          //                                {
          //                                    tempArr[0, i] = mas[0, i];
          //                                    tempArr[1, i] = mas[1, i];
          //                                }

          //                                // сохранение массива 
          //                                LittleShape2 ready = new LittleShape2();
          //                                ready.add(tempArr, rank);
          //                                //   установка масси
          //                                double areaF = area(mas);
          //                                int s = (int)Math.Round(areaF);

          //                                ready.Mass = s;
          //                                mainList.Add(ready);
          //                                foreach (var item in mas)
          //                                {
          //                                    temp.Add(item);
          //                                }
          //                                //  mas.CopyTo(tempAppay, 0);
          //                                // здесь храняться все результаты поиска
          //                                // для использования забить в в какойто PATH и расовать  проверки на массу нету
          //                                // делать отдельно в другой части кода
          //                                // если делать здесь то время поиска будет увеличиваться 
          //                                // как вариатн можно результат поиска 12 и 10 записать в файл а потом считать для демонстрации 
          //                                // работоспособности программы
          //                                // хотя площадь фигуры все равно находиться по причине избежания переполнения 
          //                                // значить сразу можно делать метку на массу как и было сделано в начале 
          //                                // делать колекцию фигур с меткой масса
          //                                testColection.Add(temp);

          //                            }
          //                            count++;

          //                        }

          //                    }
          //            }
          //            //i++;

          //        }

          //    }


          //}
          #endregion
  
        #region Пересечение площадь растояние
     /// <summary>
     /// растояние между двумя точками ?
     /// </summary>
     /// <param name="x1"></param>
     /// <param name="y1"></param>
     /// <param name="x2"></param>
     /// <param name="y2"></param>
     /// <returns></returns>
             double rasst(double x1, double y1, double x2, double y2)
            {
                return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));  // расстояние между 2-мя точками
            }

            // нетрогать
             double area(double[,] mas1) // функция для отыскания площади фигуры
            {
               //  xcouunt++;
              
                double[,] mas = (double[,])mas1.Clone();
                double min = mas[1, 0];
                for (int i = 0; i < mas.GetLength(1); i++)
                    if (mas[1, i] < min) min = mas[1, i];
                for (int i = 0; i < mas.GetLength(1); i++)
                    mas[1, i] += -min;
                double plosh = 0;
                for (int i = 0; i < mas.GetLength(1) - 1; i++)
                    plosh += (mas[1, i] + mas[1, i + 1]) / 2 * (mas[0, i + 1] - mas[0, i]);

                plosh += (mas[1, mas.GetLength(1) - 1] + mas[1, 0]) / 2 * (mas[0, 0] - mas[0, mas.GetLength(1) - 1]);
                return Math.Abs(plosh);
            }
 
     /// <summary>
     ///  функция пересечение двух отрезков ?
     /// </summary>
     /// <param name="ax1"></param>
     /// <param name="ay1"></param>
     /// <param name="ax2"></param>
     /// <param name="ay2"></param>
     /// <param name="bx1"></param>
     /// <param name="by1"></param>
     /// <param name="bx2"></param>
     /// <param name="by2"></param>
     /// <returns></returns>
             bool equal(double ax1, double ay1, double ax2, double ay2, double bx1, double by1, double bx2, double by2) // функция проверки, что 2 отрезка не пересекаются
            {
                double v1; double v2; double v3; double v4;
                //        // (bx2 - bx1)
                var b1 = bx2 - bx1;
                //         (by2 - by1)
                var b2 = by2 - by1;
                var b3 = ax2 - ax1;
                var b4 = ay2 - ay1;

                v1 = (b1) * (ay1 - by1) - (b2) * (ax1 - bx1);
                v2 = (b1) * (ay2 - by1) - (b2) * (ax2 - bx1);
                v3 = (b3) * (by1 - ay1) - (b4) * (bx1 - ax1);
                v4 = (b3) * (by2 - ay1) - (b4) * (bx2 - ax1);
                //var test1 = v1 * v2;
               // var test2 = v4 * v4;
              ;
                return  (v1 * v2 < 0) && (v3 * v4 < 0);
                //v1 = (bx2 - bx1) * (ay1 - by1) - (by2 - by1) * (ax1 - bx1);
                //v2 = (bx2 - bx1) * (ay2 - by1) - (by2 - by1) * (ax2 - bx1);
                //v3 = (ax2 - ax1) * (by1 - ay1) - (ay2 - ay1) * (bx1 - ax1);
                //v4 = (ax2 - ax1) * (by2 - ay1) - (ay2 - ay1) * (bx2 - ax1);
                //var test1 = v1 * v2;
                //var test2 = v4 * v4;
                //var res = (v1 * v2 < 0) && (v3 * v4 < 0);
                //return res  ;

            }

        #endregion




     

        

    }
}
