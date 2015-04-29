using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace twelve
{
    /// <summary>
    ///  инкаплсулирует ломаную линию 
    /// </summary>
    public class LittleShape : ICloneable
    {
        public List<Line> path = new List<Line>();
        public List<double> path2 = new List<double>();
        int counter;
        // масса фигуры 2-9(8)
        public int Mass { get; set; }

        public LittleShape()
        {
            counter = 0;
        }

        public void add(Line l)
        {
            path.Add(l);
        }
        /// <summary>
        /// выдает следущюю линию 
        /// </summary>
        /// <returns>пока не NULL можно брать линии</returns>
        public Line getNext()
        {
            if (counter == path.Count) { counter = 0; return null; }

            //  Line temp = new Line();

            return path[counter++];
        }
        /// <summary>
        ///        все точки в линии в упорядочненом массиве
        /// </summary>
        /// <returns></returns>
        public PointF[] getPointF()
        {
            List<PointF> temp = new List<PointF>();

            foreach (Line item in path)
            {
                PointF a = new PointF((float)item.X1, (float)item.Y1);
                PointF b = new PointF((float)item.X2, (float)item.Y2);
                temp.Add(a);
                temp.Add(b);
            }
            return temp.ToArray();

        }
        /// <summary>
        ///        назад в линию после трансформации
        /// </summary>
        /// <returns></returns>
        public void setPointF(PointF[] p)
        {
            path.Clear();

            for (int i = 0; i < p.Length; i += 2)
            {
                Line l = new Line();
                l.Stroke = System.Windows.Media.Brushes.Black;
                l.StrokeThickness = 3;
                l.X1 = p[i].X;
                l.Y1 = p[i].Y;
                l.X2 = p[i + 1].X;
                l.Y2 = p[i + 1].Y;
                path.Add(l);
            }



        }

        public LittleShape(LittleShape obj)
        {
            List<Line> pathNew = new List<Line>();
            counter = obj.counter;
            foreach (var item in obj.path)
            {
                Line l = new Line();
                l.X1 = item.X1;
                l.X2 = item.X2;
                l.Y1 = item.Y1;
                l.Y2 = item.Y2;
                l.Stroke = item.Stroke;
                l.StrokeThickness = item.StrokeThickness;
                pathNew.Add(l);
            }
            path = pathNew;
            Mass = obj.Mass;

        }
        public object Clone()
        {
            return new LittleShape(this);
        }
    }
}
