using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace twelve
{
    /// <summary>
    ///  класс для рисования фигур
    /// </summary>
    class Painter
    {
        /// <summary>
        ///  все фигуры  с определеной площей 2-9
        /// </summary>
        private List<PointCollection> paths = new List<PointCollection>();
        /// <summary>
        /// указатель на текущую фигуру
        /// </summary>
     private int index;

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="p">все фигуры с определеной площадю</param>
        public Painter(List<PointCollection> p)
        {
              paths=p;
            index=0;
        }
        /// <summary>
        /// возврат следцющей фигуры для рисования
        /// </summary>
        /// <returns>Path</returns>
        public PointCollection getNext()
        {
            try 
	{	        

		  if(index==paths.Count-1) return paths[index];
            else
              return paths[index++];
        
	}
	catch (Exception e)
	{
        MessageBox.Show("Alert "+e.Message);
	}
            return null;
        }
      
        /// <summary>
        /// возврат предыдцщей фигуры для рисования
        /// </summary>
        /// <returns>Path </returns>
        public PointCollection getPrev()
        {
                   try 
	{	        

		  if(index==0) return paths[index];
            else return paths[index--];
        
	}
	catch (Exception e)
	{
        MessageBox.Show("Alert "+e.Message);
	}
            return null;
        }

        /// <summary>
        /// рисует фигуру след или пред true/ false
        /// </summary>
        /// <param name="x">true/ false</param>
        //public void draw(bool x)
       // {
            //if (x == true)
            //{

        //    }
      //  }

    }

}
