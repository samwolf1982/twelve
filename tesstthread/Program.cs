using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace tesstthread
{
    class Program
    {
    static     List<int> arrr = new List<int>();
    static Random rand = new Random();
    static object locker = new object();

        static void Main(string[] args)
        {


            Thread t = new Thread(Wr);
            Thread t2 = new Thread(Wr);
            Thread.CurrentThread.Name = "Main";
            t.Name = "Worker1";
            t2.Name = "worker2";
            t.Start();
            t2.Start();

           // bw.DoWork += bw_DoWork;
         //   bw.RunWorkerAsync("Message to worker");     

            for (int i = 0; i < 2000; i++)
            {
                lock (locker)
                {
                    if (arrr.Count > 0)
                    {
                        Console.WriteLine(Thread.CurrentThread.Name + "-------  " + arrr[i]);
                    }
                }
            }
         

        }
        static void Wr()
        {
            for (int i = 0; i < 1000000; i++)
            {
            lock (locker)
              {
                  int a = rand.Next();
                if(i%125==0)
                  Console.WriteLine(Thread.CurrentThread.Name + " add " + a);
                arrr.Add( a);
              }
            }
        }
    }
}
