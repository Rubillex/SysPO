using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace fold
{
    internal class Program
    {

        public static void Help()
        {
            Console.WriteLine("{0,-5}   {1,-35}","-w или --windth=", "вывод строк с разделением по N символов");
            Console.WriteLine("{0,-5}   {1,-35}","-s или --spaces", "вывод строк с разделением по проблему");
            Console.WriteLine("{0,-5}   {1,-35}","-b или --bytes", "вывод строк с разделением по битам");
            Console.WriteLine("{0,-5}   {1,-35}","--help", "справка");
            Console.WriteLine("{0,-5}   {1,-35}","--version", "версия");
        }

        public static void Version()
        {
            Console.WriteLine("fold (GNU coreutils) 8.30" +
            "Copyright (C) 2018 Free Software Foundation, Inc." +
            "License GPLv3+: GNU GPL version 3 or later <https://gnu.org/licenses/gpl.html>." +
            "This is free software: you are free to change and redistribute it." +
                "There is NO WARRANTY, to the extent permitted by law.");
        }
        
        public static void Width(string path, int widthLine)
        {
            var textFromFile = "";
            string output = "";
            
            StreamReader file = new StreamReader(path);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                textFromFile += line;
            }

            for (int i = 0; i < textFromFile.Length; i++)
            {
                output += textFromFile[i];
                if (output.Length == widthLine)
                {
                    Console.WriteLine(output);
                    output = "";
                }
            }
        }
        
        public static void Bytes(string path, int widthLine)
        {
            try
            {
                using (var fsSource = new FileStream(path,
                    FileMode.Open, FileAccess.Read))
                {

                    // чтение файла в байт формате
                    byte[] bytes = new byte[fsSource.Length];
                    int numBytesToRead = (int)fsSource.Length;
                    int numBytesRead = 0;
                    while (numBytesToRead > 0)
                    {
                        // читаем файл, пока не дойдём до конца
                        int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                        // дошли до конца
                        if (n == 0)
                            break;

                        numBytesRead += n;
                        numBytesToRead -= n;
                    }
                    numBytesToRead = bytes.Length;

                    var line = 0;
                    
                    for (var i = 0; i < bytes.Length; i++)
                    {
                        Console.Write(bytes[i]);
                        line++;
                        if (line == widthLine)
                        {
                            Console.Write("\n");
                            line = 0;
                        }
                    }
                                
                }
            }
            catch (FileNotFoundException ioEx)
            {
                Console.WriteLine(ioEx.Message);
            }
        }

        public static void WidthSpace(string path, int widthLine)
        {
            var textFromFile = "";
            string output = "";
            
            StreamReader file = new StreamReader(path);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                textFromFile += line;
            }

            var tempWidthLine = 0;
            string[] words = textFromFile.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (output.Length + words[i].Length <= widthLine)
                {
                    output += words[i];
                }
                else
                {
                    Console.WriteLine(output);
                    output = "";
                    output += words[i];
                }
            }
        }

        public static void Main(string[] args)
        {
            
            var filePath = new List<string>();

            var bytes = false;
            var width = false;
            var widthLine = 0;
            var spaces = false;

            var help = false;
            var version = false;
            
            if (args.Length > 0)
            {
                var index = 0;
                foreach (var keys in args)
                {
                    
                    if(File.Exists(keys)) filePath.Add(keys);
                    if (keys == "--help" && !version)
                    {
                        help = true;
                        Help();
                    }

                    if (keys == "--version" && !help)
                    {
                        version = true;
                        Version();
                    }
                    if (keys == "-b" || keys == "--bytes")
                    {
                        bytes = true;
                        if (index + 1 < args.Length)
                        {
                            widthLine = Convert.ToInt32(args[index + 1]);
                        }
                    }
                    if (keys == "-s" || keys == "--spaces") spaces = true;
                    if (keys == "-w" || keys.StartsWith("--width="))
                    {
                        width = true;
                        if (index + 1 < args.Length)
                        {
                            if (keys.StartsWith("--width="))
                            {
                                widthLine = Convert.ToInt32(keys.Remove(0, 8));
                            }
                            else
                            {
                                widthLine = Convert.ToInt32(args[index + 1]);
                            }
                        }
                    }
                    index++;
                }

                foreach (var path in filePath)
                {
                    Console.WriteLine("filePath: " + path + "\n");
                    
                    if (bytes)
                    {
                        Bytes(path, widthLine);
                    }

                    if (width && spaces)
                    {
                        WidthSpace(path, widthLine);
                    }
                    
                    if (width && !spaces)
                    {
                        Width(path, widthLine);
                    }
                }
                
                

            }
        }
    }
}