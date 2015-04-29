using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace twelve
{
    class Filler2
    {

     //   List<List<int>> mainArr = new List<List<int>>();
       public List<List<int>> result = new List<List<int>>();
      public int from, to;

       static int z=0;

        // Это рекурсивный метод, 
     static   public int FactR(int n)
        {
            int result;
            if (n == 1) return 1;
                  result = FactR(n - 1) * n;
            return result;
        }

     public int testRect( int n,int a,int b,int r)
     {
         /// найти те числа какие сумма каких равна даному числу
         int result=0;
      //   if(check(a,--b,r)==true)return 1;
        // if (n == 1) return 1;
         //result = FactR(n - 1) * n;
         return result;
     }

     public List<int> testFun()
     {

       List<int> arr = Enumerable.Range(1,5).ToList();
       //  int [10] tt=new int[10];
                   //  Enumerable.Range(20,20).ToArray();
       return arr;
      
     }
      // XmlNode mainTree = new XmlNode();
        /// <summary>
        ///  шаг угла для дележки дерева( для начала 10) число кратное 360
        /// </summary>
        /// <param name="step"></param>
        public Filler2(){
            //      доделать индексайи.
            from = 3; to = 2;
            // заполнение массива
       // mainArr=    Enumerable.Range(1, 5).ToList();

        }
     /// <summary>
     ///  общая сумма углов для фигуры
    ///  https://ru.wikipedia.org/wiki/%D0%A2%D0%B5%D0%BE%D1%80%D0%B5%D0%BC%D0%B0_%D0%BE_%D1%81%D1%83%D0%BC%D0%BC%D0%B5_%D1%83%D0%B3%D0%BB%D0%BE%D0%B2_%D0%BC%D0%BD%D0%BE%D0%B3%D0%BE%D1%83%D0%B3%D0%BE%D0%BB%D1%8C%D0%BD%D0%B8%D0%BA%D0%B0
     /// </summary>
     /// <param name="x"> количество дуг число не менне 4 !!!</param>
     /// <returns>общая сума</returns>

        public List<List<int>> searchMain(int num)
        {
            List<List<int>> result2 = new List<List<int>>();

           // int[,]cdcd
            return result2;

        }
        /// <summary>
        // 
        /// </summary>
        /// <param name="breakpoint">сумма углов минус количество граней для квадрата [360-3] </param>
        /// <param name="arr">массив с начальними значениями [1][1][1][360-3] для квадрата</param>
        /// <returns></returns>
        public bool search(int breakpoint,ref List<int> arr){

            // todo
             List<int> res = new List<int>();
             // все проверки
             if (check(ref arr,breakpoint))
             {
                 enlarger2(ref arr, to, from);

                 res.Add((arr[0]));
                 res.Add(arr[1]);
                 res.Add(arr[2]);
                 res.Add(arr[3]);
                 result.Add(res);
                 search(breakpoint, ref arr);
                 return false;
             }
             else return true;


            // смещение
    
      
            //if (arr[0] == breakpoint && arr[arr.Count-1]==0)
            //{  
            //    return true;
            //}
            //else
            //{
             
            //    return false;
            //}
        }
        /// <summary>
        /// проверка на то перенос из последней стопки в первую false если все перенеслось
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="brecpoint"></param>
        /// <returns> false если все перенеслось</returns>
        public bool check(ref List<int> arr,int brekpoint)
        {
            
            // если все перенеслось
            if (arr[0] == brekpoint) return false;
         else return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="breakpoint">сумма углов минус количество граней для квадрата [360-3] </param>
        /// <param name="arr">массив с начальними значениями [1][1][1][360-3] для квадрата</param>
        /// <returns></returns>
        public bool searchDEMORECURSION(int breakpoint, ref List<int> arr)
        {


           
            // todo
            List<int> res = new List<int>();
            //if (arr[0] == breakpoint) { arr[0] = 1; arr[1]++; }
            //if (arr[1] > breakpoint) { arr[1] = 1; arr[2]++; }
            //if (arr[2] > breakpoint) { arr[2] = 1; arr[3]++; }

            res.Add((arr[0]++));
            res.Add(arr[1]);
            res.Add(arr[2]);
            res.Add(arr[3]);
            result.Add(res);
            if (arr[0] == breakpoint && arr[arr.Count - 1] == 0)
            {
                return true;
            }
            else
            {
                search(breakpoint, ref arr);
                return false;
            }
        }
        /// <summary>
        /// увеличивает или уменьшает определенную переменнуб в массиве работате как счетчик если невозможно увел тогда false
        /// </summary>
        /// <param name="arr"> массив</param>
        /// <param name="x"> максимальное значение</param>
       

    
  public void enlarger2(ref List<int> arr,int to,int from )
        {
             
             // todo
            arr[from]--;
            arr[to]++;
             // cмещение головки
            if (to == 0 && from != 1) { to = from - 1; }
              
            --to;

                 
        }
/// <summary>
 /// увеличивает или уменьшает определенную переменнуб в массиве работате как счетчик если невозможно увел тогда false
/// </summary>
/// <param name="arr"> массив</param>
/// <param name="x"> максимальное значение</param>
        public bool enlarger(ref List<int> arr,int x)
        {
             z = arr.Count;
         // todo      
         // arr[indexPlus]++;
         //   arr[indexPlus]--;
            //уменьшение последнего числа на 1
            try
            {
            arr[3]--;
            arr[0]++;

            if (arr[0] > x) { 
                arr[0] = 1; arr[1]++;
            }
            if (arr[1] > x) { arr[1] = 1; arr[2]++; }
            if (arr[2] > x) { arr[2] = 1;// arr[3]++;
            }
            if (arr[3] == 0)
            {
                // a
            } 
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        
            return true;

                 
        }

        
        private int sumCount(int x)
        {
            // x= 180(n-2)
            return 180 * (x - 2);   
        }
    }
}
