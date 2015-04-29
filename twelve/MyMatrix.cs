using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twelve
{
    class MyMatrix
    {
        /// <summary>
        /// 0 1 2 первая строчка матрицы
        /// </summary>
        float[] a = new float[3];
        /// <summary>
        /// 0 1 2 вторая строчка матрицы
        /// </summary>
        float[] b = new float[3];
        /// <summary>
        /// 0 1 2 третья строчка матрицы
        /// </summary>
        float[] c = new float[3];
        public MyMatrix(float[] a1, float[] a2, float[] a3)
        {
            a = a1;
            b = a2;
            c = a3;
        }
        /// <summary>
        /// матричное умножение
        /// </summary>
        /// <param name="a">x,y,z точки</param>
        public PointF multy1(float[] aN)
        {
            float x = (aN[0] * a[0] + aN[1] * b[0] + aN[2] * c[0]) / c[2];
            float y = (aN[0] * a[1] + aN[1] * b[1] + aN[2] * c[1]) / c[2];
            PointF pTemp = new PointF(x, y);
            
            return pTemp;
        }
    }
}
