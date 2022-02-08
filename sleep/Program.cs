using System;
using System.Diagnostics;
using System.Threading;

namespace sleep
{
    internal class Program
    {
        private double sleepTime = 0;

        private static void Sleep(double time, char key)
        {
            switch (key)
            {
                case 's':
                    time *= 1000;
                    break;
                case 'm':
                    time *= (1000 * 60);
                    break;
                case 'h':
                    time *= (1000 * 60 * 60);
                    break;
                case 'd':
                    time *= (1000 * 60 * 60 * 24);
                    break;
            }
            
            Thread.Sleep(Convert.ToInt32(time));
        }
        
        public static void Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            
            if (args.Length >  0)
            {
                var time = 0.0;
                foreach (var key in args)
                {
                    if (key.Contains("s") ||
                        key.Contains("m") ||
                        key.Contains("h") ||
                        key.Contains("d"))
                    {
                        time = Convert.ToDouble(key.Remove(key.Length - 1));
                        var _key = key[key.Length - 1];
                        Sleep(time, _key);
                    }
                    else
                    {
                        time = Convert.ToDouble(key);
                        Sleep(time, 's');
                    }
                }
                timer.Stop();
                Console.WriteLine(timer.Elapsed);
            }
        }
    }
}