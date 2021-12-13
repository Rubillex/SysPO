using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using static System.Threading.Thread;

namespace taskmngr
{
    internal class Program
    {   
        public static Process[] list_of_processes = Process.GetProcesses();
        public static List<Process> tttt = list_of_processes.ToList();
        public static int iter = 0;
        public static int total_processes_length = list_of_processes.Length;
        public static bool sorted1 = false;
        public static bool sorted2 = false;
        public static bool sorted3 = false;
        private static DateTime lastTime;
        private static TimeSpan lastTotalProcessorTime;
        private static DateTime curTime;
        private static TimeSpan curTotalProcessorTime;

        static int Compare(int a, int b)
        {
            if(a > b)
            {
                return b;
            }
            if(b > a)
            {
                return a;
            }
            if(a == b)
            {
                return a;
            }
            return 0;
        }
        
        public static void Main(string[] args)
        {
            Console.Title = "TaskManager";
            Console.SetWindowSize(120, 31);
            var first_thrd = new Thread(first_output);
            first_thrd.Start();
            
            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                {
                    if(iter > 0)
                    {
                        iter--;
                    }
                }
                if (key == ConsoleKey.DownArrow)
                {
                    if (iter < total_processes_length)
                    {
                        iter++;
                    }

                    if (iter > total_processes_length-28)
                    {
                        iter = total_processes_length-28;
                    }
                }

                if (key == ConsoleKey.A) 
                {
                    sorted1 = true;
                    sorted2 = false;
                    sorted3 = false;
                }
                
                if (key == ConsoleKey.S)
                {
                    sorted1 = false;
                    sorted2 = true;
                    sorted3 = false;
                }
                if (key == ConsoleKey.D)
                {
                    sorted1 = false;
                    sorted2 = false;
                    sorted3 = true;
                }
                
                if(key == ConsoleKey.Escape)
                {   
                    first_thrd.Abort();
                    break;
                }
            }
        }

        //     ДОЛЖНО СЧИТАТЬ CPU_USAGE про его имени
        // static string CPU_USAGE(string processName)
        // {
        //     var result_cpu_usage = "";
        //     Process[] pp = Process.GetProcessesByName(processName);
        //     Process p = pp[0];
        //     if (lastTime == null || lastTime == new DateTime())
        //     {
        //         lastTime = DateTime.Now;
        //         lastTotalProcessorTime = p.TotalProcessorTime;
        //     }
        //     else
        //     {
        //         curTime = DateTime.Now;
        //         curTotalProcessorTime = p.TotalProcessorTime;
        //
        //         double CPUUsage = (curTotalProcessorTime.TotalMilliseconds - lastTotalProcessorTime.TotalMilliseconds) / curTime.Subtract(lastTime).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount);
        //         //Console.WriteLine("{0} CPU: {1:0.0}%",processName,CPUUsage * 100);
        //         lastTime = curTime;
        //         lastTotalProcessorTime = curTotalProcessorTime;
        //         result_cpu_usage = String.Format("CPU: {0:0.0}%",CPUUsage * 100);
        //         return result_cpu_usage;
        //     }
        //     
        //     return result_cpu_usage;
        // }
        //
        
        static void first_output()
        {
            while (true)
            {   
                Console.CursorVisible = false;
                Console.SetCursorPosition(0,0);
                //Console.Clear();
                Console.WriteLine("{0,-5}   {1,-35} {2,-5} \t {3,-7}      {4,-20}","№", "Name", "Ram" , "PID","CPU");
                Console.WriteLine("-------------------------------------------------------------------------------------------");
                list_of_processes = Process.GetProcesses();
                tttt = list_of_processes.ToList();
                total_processes_length = tttt.Count;
                
                if (sorted1 == true)
                {
                    tttt = tttt.OrderBy(process => process.ProcessName).ToList();
                }

                if (sorted2 == true)
                {
                    tttt = tttt.OrderBy(process => process.PrivateMemorySize64).ToList();
                }
                
                if (sorted3 == true)
                {
                    tttt = tttt.OrderBy(process => process.Id).ToList();
                }
                
                for (int i = iter; i < Compare(iter + 28 , total_processes_length); i++)
                {
                    Console.WriteLine("{0,-5}   {1,-35} {2,-5}M \t {3,-7} \t ", i, tttt[i].ProcessName, tttt[i].PrivateMemorySize64 / 1000000L, tttt[i].Id);
                }
                Thread.Sleep(10);
                //Sleep(50);
            }
        }
    }
}