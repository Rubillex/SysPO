using System;
using System.Threading;
namespace ConsoleApplication1
{
    internal class Program {
        public static int t = 0;
        
        public static int random(int a, int b)
        {
            Random rnd = new Random();
            return  rnd.Next(a, b);
        }
        
        public static void Main(string[] args) {
            Random rnd = new Random();
            for (int i = 0; i < 20; i++)
            {
                
                Console.WriteLine(rnd.Next(0, 10));
            }
            
        }
        static void first() {
            double f;
            while (true) {
                f = Math.Sin((2*Math.PI*t)/200);
                Console.Out.WriteLine("t=" + t + " f=" + f);
                t++;
            }
        }
        static void sec() {

            int num = 0;

            var first = new Thread(Program.first);
            first.Start();

            while (true) {
                if (t == 50) {
                    if (num == 9) first.Abort();
                    else first.Suspend();
                    t = 0;
                    num++;
                    Console.Error.WriteLine("Прерываний " + num);
                    if (num == 10) {
                        Console.WriteLine("Закончил работу");
                        return;
                    }
                    else first.Resume();
                }
            }
        }
    }
}