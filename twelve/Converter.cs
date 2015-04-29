using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace twelve
{
    
    class Converter
    {
        Random rand = new Random();

        List<PointCollection> mainlist;
        PointCollection pc;

        int countline;
        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="x">все</param>
        /// <param name="countline"> количество линий в фигуре потом считать размер сейчас 8</param>
        public Converter(List<double[,]> x, int countline){
            mainlist=new List<PointCollection>();   // лист 
       //     this.countline=countline;
           // int counter = 0;
            List<Point> www = new List<Point>();
            double[,] exemplyar;
            for (int i = 0; i <x.Count; i++)
            {
                var ooo = x[i];
                for (int j = 0; j < countline; j++)
                {
                    var q1 = ooo[0, j];
                    var q2 = ooo[1, j];
                    www.Add(new Point((q1+3)*5, (q2+3)*5));
                }
              
              //  break;
            }  var ter = www[5];

             foreach (var item in x)
             {
                 exemplyar = null;  
                 exemplyar= item;
                 pc = new PointCollection();
                 for (int i = 0; i < 2; i++)
                 {
                     for (int j = 0; j < countline; j++)
                     {
                         Point point=new Point(exemplyar[0,j],exemplyar[1,j]);
                       //  exemplyar[i, j] = (exemplyar[i, j]+3) * 50 ;

                         pc.Add(point);
                      //   counter++;
                     }

                 }
              //   System.Diagnostics.Debug.WriteLine(pc.ToString());
                 mainlist.Add(pc);
             }

             int t = 0;
           // mainlist=x;
           
              
          
        }
        /// <summary>
        /// для теста возврат одного значения если ок пусктат в цикл
        /// </summary>
        /// <returns></returns>
        public PointCollection getter()
        {
            return pc;
        }

        private void someDO(){

        }
        /// <summary>
        ///     cлучайная точка 300 300
        /// </summary>
        /// <returns>Point</returns>
        public Point newPoint()
        {


            return new Point(rand.Next(300), rand.Next(300));
        }
        public List<PointCollection> Mainlist
        {
            get
            {
                                  return mainlist;
              
            }
        }
    }
}
