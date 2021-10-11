using System;
using System.Threading;

namespace Lab2
{
    internal class Program
            {
                public static int t = 0;
                
                public static void Main(string[] args)
                {
                    //запуск потока 2
                    new Thread(secondFunc).Start();
                    Console.Read();
                }
                
                static void firstFunc()
                {
                    //простой рассчёт f по формуле
                    double f;
                    while (true)
                    {
                        f = Math.Sin((2*Math.PI*t)/200);
                        Console.Out.WriteLine("t: " + t + " f: " + f);   
                        t++;
                    }
                }
        
                static void secondFunc()
                {
                    //счётчик обнулений
                    int num = 0;
                    //запуск первого потока
                    var first = new Thread(firstFunc);
                    first.Start();
                    //ВАЙЛТРУ
                    while (true) 
                    {
                        if (t == 50)
                        {
                            //выбираем убить поток 1 или остановить
                            if (num == 9) first.Abort();
                            else first.Suspend();
                            //обнуление и инкремент счётчика обнулений
                            t = 0;
                            num++;
                            //вывод в stderr
                            Console.Error.WriteLine("Прервал num:" + num);
                            //выход из потока 2 или восстановление потока 1
                            if (num == 10) return;
                            if (num != 10) first.Resume();
                        }
                    }
                }
            }
}