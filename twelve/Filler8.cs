using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

namespace twelve
{
    class Filler8
    {
        #region Values
                //List<Vector> mainList = new List<Vector>();
    public   List<Vector> mainList2 = new List<Vector>(); // колекция векторов
      public List<Vector> temp = new List<Vector>();
    //  int[] testlist = new int[] { 6, 6, 9,  0, 0 };
      int index = 1;
       int rank;
      int countAlif = 0;
     public  List<List<Vector>> mainLL = new List<List<Vector>>();
     public Vector[,] dinamicmainLL;
     //Переменная "локер", которая служит для блокировки value
     private object valueLocker = new object();

      int[] indexerToLevel;
        #endregion


     #region Debug
    public List<int[]> testint = new List<int[]>();
     #endregion


    public void startThread()
    {
        lock (valueLocker)
        {
            //Выводим значение value
            var res = alif4(0);
            //Увеличиваем его на единицу
                 }
    }
      public void start()
      {
          temp.Clear();
          #region Hide
           //temp.Add(mainList2[6]);
          //temp.Add(mainList2[6]);
          //temp.Add(mainList2[9]);
          //temp.Add(mainList2[0]);
          //temp.Add(mainList2[0]);
          /////////*************
          //temp.Add(mainList2[3]);
          //temp.Add(mainList2[6]);
          //temp.Add(mainList2[9]);
          //Vector resTemp = new Vector();
          //foreach (var item in temp)
          //{
          //    resTemp +=item;
          //}
          //for (int i = 0; i < mainList2.Count; i++)    // для каждого елемента из векторов свой запуск
          //{
          //List<Vector>vv=new List<Vector>();
          //vv.Add(temp[0]);
          #endregion
         
          var po = Math.Pow(12, 4);
          var maxval = long.MaxValue;

          for ( long i = 0; i < po; i++)
          {

              if (i == 500)
              {

              }
              //Thread tread = new Thread(alif4);


              var res = alif4(0);
          }
                    //var res=alif3(1,temp[0],vv);   // для каждого елемента из векторов свой запуск
        // для каждого елемента из векторов свой запуск

                    int result = 9999;
  
      
      }

      public  Vector alif4(int level)   
      {
         // countAlif++;
          if (level == rank - 1)// опустился вниз и взял след вектор и вернул
          {
            #region Нижний уровень
                          //return  nextVector4End(level);  // возврат самого нижнего вектор + смещение индекс      
              return nextVector4End(level);  // возврат самого нижнего вектор + смещение индекс      
              }
           #endregion

          var lev = level+1;
              return  alif4(lev)+ nextVector4(level);
      

      }

      #region Not Use
            public bool alif3(int level, Vector vector, List<Vector> curentList)
      {
          countAlif++;
          if (level == rank - 1)   // опустился вниз и взял след вектор и вернул
          {
              #region Нижний уровень
              foreach (var item in mainList2)
              {
                  Vector res = item + vector;
                  if (res.Length > 0.90 && res.Length < 1.1)
                  {
                      int ok = 999;
                      // проверить на пересечение и собирать
                      curentList.Add(item);
                      mainLL.Add(curentList.ToList());
                      curentList.RemoveAt(curentList.Count - 1);
                      return true;
                  }
                  else
                  {
                      int bad = 999;
                  }
              }
              return false;
              #endregion
              // проход по циклу последнего совпадения

          }

          int num = level + 1;
          foreach (var item in mainList2)
          {
              var vv = item + vector;
              var result = alif3(num, vv, curentList);
          }

          // сочетаю двв вектора

          return false;

      }

        public List< Vector>  alif2(int level, Vector vector)
      {
          countAlif++;
          if (level == rank-1)
          {
              #region Нижний уровень
                       foreach (var item in mainList2)
              {
                  Vector res=item+vector;
                  if (res.Length > 0.90 && res.Length < 1.1)
                  {
                      int ok = 999;
                      // проверить на пересечение и собирать

                  }
                  else
                  {
                      int bad = 999;
                  }
              }
              return new List<Vector>();
              #endregion
              // проход по циклу последнего совпадения
     
          }
          int num = level + 1;
          var vv = nextVector()+vector;  // сочетаю двв вектора
          var result = alif2(num, vv);
          return result;

      }
        private  Vector nextVector4(int level)
        {
            Vector v = new Vector();
            int x = (indexerToLevel[level]);
            v = mainList2[x];
          //  nextValueChange(level, mainList2.Count);
            return v;
        }
      #endregion

        private  Vector nextVector4End(int level)
        {
            Vector v = new Vector();
            int x = (indexerToLevel[level]);
            v = mainList2[x];
            ++indexerToLevel[level];
            nextValueChange(level, mainList2.Count);
            return v;
            //Vector v = new Vector();
            //int n = indexerToLevel[level]; // уровень - значение до 12
            //if (n >= mainList2.Count)
            //{   // когда n =12 тогда увеличиваеться предыдущий уровень и установка значения в 0
            //  //  ++indexerToLevel[level - 1];
            //    ++indexerToLevel[level-1] ;
            //    indexerToLevel[level] = 0;
            //    //return new Vector(); // нулевой вектор
            //    int x1 = (indexerToLevel[level]);
            //    v = mainList2[x1];

            //    return v;
            //}
            //int x = (indexerToLevel[level]++);
            //v = mainList2[x];
            //return v;
        }


        public  void nextValueChange(int level,int maxVal)
        {
           // indexerToLevel[level]++;
            for (int i = indexerToLevel.Length-1; i >0; i--)
            {
                if (indexerToLevel[i] >= maxVal) { indexerToLevel[i] = 0; indexerToLevel[i - 1]++; }

            }


        }
        
        private Vector nextVector()
      {
          Vector v=new Vector();
          v = temp[index++];
          return v;
      }

      public Filler8(int rank=6)
      {
          this.rank = rank ;
          indexerToLevel = new int[rank];//~~~~~~~~~~~~~~~~~~~

          dinamicmainLL = new Vector[rank, 4096];
      }

      #region Test Draw alif
      public Vector alif(int level, Vector vector)
      {
          countAlif++;
          if (level == rank)
          {
              return nextVector();
          }
          int num = level + 1;
          var vv = nextVector();
          var result = alif(num, vv) + vv;
          return result;

      }
      public string test()
      {

          return "";
      }
      public List<LittleShape2> draw2()
      {
          List<LittleShape2> f = new List<LittleShape2>();
          int ccc = 0;
          foreach (var item in mainLL)
          {
              if (ccc++ == 1000) return f;
              List<Point> p = new List<Point>();
          int c = 1;
          p.Add(new Point());
              foreach (var i in item)
              {
                  Point pr = new Point(i.X * c, i.Y * c);
                  p.Add(pr);
                  shiftList(ref p, pr);
                  //  shiftList(ref p, new Point(0, 0));
                  List<Point> lp = new List<Point>();

                  LittleShape2 ls = new LittleShape2();
                  ls.setPoint(p.ToArray());
                  f.Add(ls);
              }


          }
          //foreach (var item in temp)
          //{

          //}



          return f;
      }
      
      public List<LittleShape2> draw()
      {
          List<LittleShape2> f = new List<LittleShape2>();
          List<Point> p = new List<Point>();
          int c = 1;
          p.Add(new Point());
          foreach (var item in temp)
          {
              Point pr = new Point(item.X * c, item.Y * c);
              p.Add(pr);
              shiftList(ref p, pr);
          }

          //  shiftList(ref p, new Point(0, 0));
          List<Point> lp = new List<Point>();

          LittleShape2 ls = new LittleShape2();
          ls.setPoint(p.ToArray());
          f.Add(ls);

          return f;
      }
        
      #endregion

        
        
        #region функции из Filler
        public void addPointToVectorMainList2()
        {
            // add to       mainlist2

            //for (double a = 0; a <= 360; a += 30) // бежим в 11 сторон
            for (double a = 0; a <= 360; a += 30) 
            {
                double xx = Math.Sin(a * Math.PI / 180);
                double yy = Math.Cos(a * Math.PI / 180);
                Vector v = new Vector(xx - 0, yy - 0);
                mainList2.Add(v);
            }
        //    indexerToLevel = Enumerable.Range(0, mainList2.Count).ToArray();

        }

        public String showAllLength(List<Vector> s)
        {
            String str = "";
            int x = -30;
            int indexer = 0;
            foreach (var item in s)
            {
                str += indexer++ + ") Vec: " + (x += 30).ToString() + "   (" + item.X + " : " + item.Y + ")  " + "Length: " + item.Length + "\n";
            }
            return str;
        }


        /// <summary>
        /// смещение фигуры на ноль
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="p"></param>
        public void shiftList(ref List<Point> arr, Point p)
        {
            for (int i = 0; i < arr.Count; i++)
            {
                var x = arr[i].X - p.X; ;// p.X;
                var y = arr[i].Y - p.Y; ;
                arr[i] = new Point(x, y);
            }
        }

        #endregion
  


      
    }
}
