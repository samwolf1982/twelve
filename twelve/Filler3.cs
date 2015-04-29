using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twelve
{
    class Filler3
    {
        int rank;
        double angle;
        /// <summary>
        ///  список совпадение потом координаты
        /// </summary>
        public List<bool> mainList = new List<bool>();
        public bool search()
        {


            return false;
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

        }
    }
}
