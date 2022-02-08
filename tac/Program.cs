using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace tac
{
    internal class Program
    {
        
        public static void Help()
        {
            Console.WriteLine("{0,-5}   {1,-35}","имя файла/имена файлов", "вывод строк в обратной последовательности");
            Console.WriteLine("{0,-5}   {1,-35}","-s N", "вывод строк в обратном порядке, но с переносом по разделителю N");
            Console.WriteLine("{0,-5}   {1,-35}","-b", "вывод строк, но разделитель ставится в начале нового предложения");
            Console.WriteLine("{0,-5}   {1,-35}","--help", "справка");
            Console.WriteLine("{0,-5}   {1,-35}","--version", "версия");
        }

        public static void Version()
        {
            Console.WriteLine("Tac (GNU coreutils) 8.30" +
                              "Copyright (C) 2018 Free Software Foundation, Inc." +
                              "License GPLv3+: GNU GPL version 3 or later <https://gnu.org/licenses/gpl.html>." +
                              "This is free software: you are free to change and redistribute it." +
                              "There is NO WARRANTY, to the extent permitted by law.");
        }
        public static void TacFunc(string path, bool s, bool b, string separator)
        {
            var lineFromFile = new List<string>();

            var FileLines = "";

            StreamReader file = new StreamReader(path);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                lineFromFile.Add(line);
            }

            if (!s && !b)
            {
                for (int i = lineFromFile.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine(lineFromFile[i]);
                }
            }

            if (!s && b)
            {
                var tempLinesFromFile = new List<string>();

                foreach (var lineTemp in lineFromFile)
                {
                    var temp = lineTemp.TrimEnd('\n');
                    tempLinesFromFile.Add(temp);
                }
                
                Console.WriteLine("");
                
                for (int i = tempLinesFromFile.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine(tempLinesFromFile[i]);
                }
                
            }
            
            if (s && !b)
            {

                foreach (var l in lineFromFile)
                {
                    FileLines += l;
                }

                lineFromFile.Clear();

                string[] tempLines = FileLines.Split(new[] { separator }, StringSplitOptions.None);

                for (int i = 0; i < tempLines.Length - 1; i++)
                {
                    tempLines[i] += separator;
                }
                
                for (int i = tempLines.Length - 1; i >= 0; i--)
                {
                    Console.WriteLine(tempLines[i]);
                }
            }

            if (s && b)
            {

                foreach (var l in lineFromFile)
                {
                    FileLines += l;
                }

                lineFromFile.Clear();

                string[] tempLines = FileLines.Split(new[] { separator }, StringSplitOptions.None);
                
                for (int i = 1; i < tempLines.Length; i++)
                {
                    tempLines[i] = string.Concat(separator, tempLines[i]);
                }

                for (int i = tempLines.Length - 1; i >= 0; i--)
                {
                    Console.WriteLine(tempLines[i]);
                }
            }
        }

        public static void Main(string[] args)
        {
            var filePath = new List<string>();

            var s = false;
            var b = false;
            
            var help = false;
            var version = false;
            
            var separator = "";

            if (args.Length > 0)
            {
                var index = 0;
                foreach (var key in args)
                {

                    if (key == "--help" && !version)
                    {
                        help = true;
                        Help();
                    }

                    if (key == "--version" && !help)
                    {
                        version = true;
                        Version();
                    }
                    
                    if (File.Exists(key))
                    {
                        filePath.Add(key);
                    }

                    if (key == "-s")
                    {
                        s = true;
                        if (index + 1 < args.Length)
                        {
                            separator = Convert.ToString(args[index + 1]);
                        }
                    }

                    if (key == "-b")
                    {
                        b = true;
                    }

                    index++;
                }

                foreach (var path in filePath)
                {
                    TacFunc(path, s, b, separator);
                }
            }
        }
    }
}