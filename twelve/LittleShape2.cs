﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace twelve
{
    public class LittleShape2
    {
        public List<Line> path = new List<Line>();
        double[,] doublePath = null;
        int counter;
    
        // масса фигуры 2-9(8)
        public int Mass { get; set; }

        public LittleShape2()
        {
            counter = 0;
        }

        public void add(double[,] t,int rank)
        {
            // копирование массива          
            doublePath = new double[2, rank];
            for (int i = 0; i < rank; i++)
            {
                doublePath[0, i] = t[0, i];
                doublePath[1, i] = t[1, i];
   
            }
            



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
        public PointF[] getPointF(int rank)
        {
            List<Line> tt = new List<Line>();
            tt = LittleShape2ToLine(rank);
            List<PointF> temp = new List<PointF>();

            foreach (Line item in tt)
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

        public LittleShape2(LittleShape2 obj)
        {
           // doublePath = new double[2, rank];
          
       //  var v = obj.doublePath.GetLength();
            var size=obj.doublePath.Length / 2;
            doublePath = new double[2, size];
         for (int i = 0; i < size; i++)
            {
                doublePath[0, i] = obj.doublePath[0, i];
                doublePath[1, i] = obj.doublePath[1, i];

            }

/////////////////////
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
            return new LittleShape2(this);
        }
        public LittleShape2(int rank)
        {
            doublePath = new double[2, rank];
        }
        public List<Line> LittleShape2ToLine( int rank)
        {
            LittleShape2 temp = new LittleShape2();
            List<Line> res2 = new List<Line>();
            double[,] d = doublePath;
            // mas все что с d[0, - x  a d[1, y
            for (int i = 0; i < rank; i++)
            {
                Line l = new Line();
                l.Stroke = System.Windows.Media.Brushes.Black;
                l.StrokeThickness = 3;
                l.X1 = d[0, i];
                l.Y1 = d[1, i];
                l.X2 = d[0, i < rank - 1 ? i + 1 : 0];
                l.Y2 = d[1, i < rank - 1 ? i + 1 : 0];
                res2.Add(l);
            }
            return res2;

        }
    }
}