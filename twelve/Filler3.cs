using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace twelve
{
    class Filler3
    {
        int rank;
        double angle;
        int startN = 0;
        Point p;

        public List<List<Point>> mainColections = new List<List<Point>>();
        public List<Point> mainPointList = new List<Point>();
        public List<Point> redyPoints = new List<Point>();

        // для отладкиж
        int plus= 0, minus=0;
        public List<double> arrDegree = new List<double> ();
     public   List<double> test = new List<double> {0, 30, 90,210};
        public bool search()
        {
            // узнаем глубину если (4-1 rank ) тогда делаем первую проверку на совпадение с (0 0)
            // имееться в виду что на самом низу
            startN++;
             //if(startN==rank-1){  // третья или ппред последняя
             if(startN==rank){
                      // здесь первое попадание может быть а может и не быть :)
                    // если последняя точка совпадает с точками круга
                      // нулевой точки тогда она достает до (0:0) и имеет размер 1-ца и возврат для последующей обработки
                // ??? делать ли проверку на пересечение для етой линни  (x y) (0: 0) c остальными ??
                // ?? возможно что последняя == первой ?? я думаю что с большими углами и нескоькими поколениями 
                // это маловероятно (если будет время проверить или нужда )))
                  if( redyPoints.Exists(x=>x== mainPointList.Last())){
                                   var t = 0;
                       
                        // следующей обработки ветки на уровень выше
                        // уменьшаем уровень вложенности
                        startN--;
                        return true;

                  }
               
                plus++;
                return false;
            }
            else
            {
                // для начала строим путь
                double degree = test[startN];
                // здесь заполняемся пока не дойдем то нижнего уровня сейчас 4 уровня
                // хорошо продумать правило заполнения  можно дать отдельную фун.
                p = newPoint(ref mainPointList,degree);
                if (search() == true) {
                    //  ветка что глубже дала положительный отзыв значить надо проверять 
                    // то что ...??
                    //++ 1) смотреть на каком уровне находимся   //  startN == 3 значит я на точке 3 вместе с (0 0) 
                    // 2) делать проверку на что-то ?? ( 1) на пересечение ?? с какими линиями (надо ли со всеми ??)  2) что еще??
                    // проверять с следующей и предыдущей ненадо Причина: эта точка есть базисом для построения угла для
                    // текущей линни и предыдущей ( я на точке 3 значить что линия 2и3 составляют угол - проверка не нужна только для через одну выше... и ниже...)
                    //++ 3)  слать сигнал выше что все  ок (return true;) или что все плохо. return false;
                    var t = 0;
                    // Level
                    int level = startN;
                    // текущая точка и следующая  они создают линию для проверки
                    Point pCurent = mainPointList[startN-1];
                    Point pNext = mainPointList[startN ];
                    // заходим в цикл и проверяем на пересечение с теми что идут после или до ??
                    // думаю проверять только с теми что прошли валидацию )) ведь с теми что еще не прошли
                    // нету смысла иметь дело значить проверка тех что внизу
                    
                    // предпоследняя точка сверять с последней нету надобности они создают угол  ABC  сейчас на B (из 4точек  я на 3 точке)
                    // проверка только начиная с  mainPointList.Count-2 !!
                    // думаю можно удалить а первый раз сразу спригнуть на 2 поколения вверх  
                    // потом 
                     if(startN==mainPointList.Count-1)
                    {
                        startN--;
                        return true;

                    }
                    // теперь проверка на пересечение только тех линий что идут ниже.
                     // level -2 можно проверять ту самую последню где начало mainPointList[+2] а конец mainPointList[0]
                     if (startN <= mainPointList.Count -2)
                     {
                         // ето точки той линии что надо проверить не текущей  в даном случае последняя
                         // пока что обход 
                         // дублирую пока что перменные (точка и количество елементов) потом оптимиз 
                         int startN2 = startN;
                         int maxElement = mainPointList.Count;
                         //    количество проверок для цикла проследить   !!!!!!!!!!!!!!!!  0 1 ok
                        // int counProverk = maxElement - 2 - startN;
                         int counProverk = maxElement - 1 - startN;
                            // вроде все ок начинаме проверять на пересечение все что внизу -2 линии 
                         //  для  квадрата на 2-ом лев. только одна проверка на пересечение с последней точкой
                         // и нулевой точкой суть линия( xy - 00 )
                         // но аналогично получаеться что и для первой точки приходится делать проверку 
                         // пускай будет возможно будет убираться тот случай когда первая и последняя линия будут с одинаковими координатами 
                         // возможно потом от этой проверки избавлюсь 
                         bool flag = false;
                         for (int i = 1; i <= counProverk; i++)
                         {
                             // и поехали точка(вторая начало линии)
                             Point pCurent2 = mainPointList[startN + i];
                             // считать так чтобы последней индекс был или первым или следующим через один
                             // чтото не так но вроде работает ???? проверить ставлю метку !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                             int zeroOrNext = ((startN + i+1) < mainPointList.Count) ? (startN + i+1) : 0;
                             // точка вторая конец линии
                             Point pNext2 = mainPointList[zeroOrNext];
                             // если не пересекаються тогда флаг + иначе - и брейк
                             if (intersection(pCurent.X, pCurent.Y, pNext.X, pNext.Y, pCurent2.X, pCurent2.Y, pNext2.X, pNext2.Y) == false)
                             {
                                 flag = true;
                             }
                             // проверка для 4 ок
                         }
                         // после цикла смотрим на флаг + -  если+ значить не пересекаються тогда 
                         // сигналим вверх  ок  незабывая менять уровень вложености  
                         if (flag == true) 
                         {
                             var r = 0;
                             // ok уменьшаем уровень и true 
                             startN--;
                             // здесь можно наверно перехватить готовую линию
                             // если левел ==0
                             if (startN == 0)
                             {
                                 // ну если уже здесь значить чтото можно уже словилось собираю в главную колекцию
                                 System.Diagnostics.Debug.WriteLine("catch !!!!! : " + startN.ToString());
                                 // фигуру добавили 
                                 mainColections.Add(mainPointList.ToList());
                                 //  и теперь мы все равно на 0 уровне
                                 // значить здесь удобно давать цикл для второй фигуры - наверно
                                 // можно чистить mainPointList и стартовать с новыми даными поже 
                                 // незабыть оставить первую нулевую точку или добавить новую
                                 var t2 = 0;
                             }
                             return true;
                         }
                         else
                         {
                             return false;
                         }

                         // 
                         //Point pCurent2 = mainPointList[startN + 1];
                         //// считать так чтобы последней индекс был или первым или следующим через один
                         //// чтото не так но вроде работает ???? проверить ставлю метку !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                         //int zeroOrNext = ((startN + 2) < mainPointList.Count) ? (startN + 2) : 0;
                         //Point pNext2 = mainPointList[zeroOrNext];
           


                    //     // если не пересекаються тогда идем дальше
                    //if (intersection(pCurent.X, pCurent.Y, pNext.X, pNext.Y, pCurent2.X, pCurent2.Y, pNext2.X, pNext2.Y) == false)
                    //{
                    //    var r = 0;
                    //    // ok уменьшаем уровень и true 
                    //    startN--;
                    //    return true;
                    //}
                    //     for (int i = 0; i < 10; i++)
                    //     {
                    //         if (true)
                    //         {

                    //             // следующей обработки ветки на уровень выше
                    //             // уменьшаем уровень вложенности
                    //             startN--;
                    //             return true;
                    //         }
                    //     }

                    //     //if (intersection() == false)
                    //     //{
                    //     //    // подумать как очищать результаты или куда дальше пускать веть 
                    //     //    //???????????
                    //     //    return false;
                    //     //}
                    //     //else
                    //     //{
                    //     //      startN--;
                    //     //   return true;
                    //     //}
                       

                     }

           
                    System.Diagnostics.Debug.WriteLine("Level : " + startN.ToString());
                }
                
                minus++;
                return false;
            }
        }
        public Point newPoint(ref List<Point> arr,double degree)
        {
           
            double angle2 = Math.PI * degree / 180.0;
            double sinAngleX = Math.Sin(angle2);
            double cosAngleY = Math.Cos(angle2);
            // новая точка со смещением
            //double cosAngleY =1- Math.Cos(angle2);
       
            
            //   double y = 1 - Math.Cos(a * Math.PI / 180);
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
        /// <summary>
        /// функция проверки, что 2 отрезка не пересекаются
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
        bool intersection(double ax1, double ay1, double ax2, double ay2, double bx1, double by1, double bx2, double by2) // функция проверки, что 2 отрезка не пересекаются
        {

            double v1; double v2; double v3; double v4;

            v1 = (bx2 - bx1) * (ay1 - by1) - (by2 - by1) * (ax1 - bx1);
            v2 = (bx2 - bx1) * (ay2 - by1) - (by2 - by1) * (ax2 - bx1);
            v3 = (ax2 - ax1) * (by1 - ay1) - (ay2 - ay1) * (bx1 - ax1);
            v4 = (ax2 - ax1) * (by2 - ay1) - (ay2 - ay1) * (bx2 - ax1);
            var test1 = v1 * v2;
            var test2 = v4 * v4;
            var res = (v1 * v2 < 0) && (v3 * v4 < 0);
            return res;

        }
    }
}
