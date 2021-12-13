using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace cat
{
    internal class Program
    {
        public static void outputFunc(string path, string key)
        {
            Console.WriteLine("path: " + path + " key: " + key);
            List<String> textFromFile = new List<string>();
            string output = "";
            var numLine = 1;
            StreamReader file = new StreamReader(path);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                switch (key)
                {
                    //нумерация непустых строк
                    case "-b":
                        if (line != "")
                        {
                            output += numLine;
                            numLine++;
                            output += " ";
                            output += line;
                            textFromFile.Add(output);
                            output = "";
                        }
                        break;
                    //нумерация каждой строки
                    case "-n":
                        output += numLine;
                        numLine++;
                        output += " ";
                        output += line;
                        textFromFile.Add(output);
                        output = "";
                        break;
                    //удаление повторяющихся пустых строк
                    case "-s":
                        output += line;
                        if (output == textFromFile.Last() && output == "")
                        {
                            textFromFile.RemoveAt(textFromFile.Count);
                        }
                        else
                        {
                            textFromFile.Add(output);
                        }

                        output = "";
                        break;
                    //$ в конце каждой строки
                    case "-e":
                        output += line;
                        output += "$";
                        textFromFile.Add(output);
                        output = "";
                        break;
                    case "":
                        output += line;
                        textFromFile.Add(output);
                        output = "";
                        break;
                }
            }
            Console.WriteLine("Вывод");
            //вывод
            foreach (var lineFromList in textFromFile)
            {
                Console.WriteLine(lineFromList);
            }
        }

        public static void outToFile(List<string> path, string newFile)
        {
            if (File.Exists(newFile))
            {
                foreach (var filename in path)
                {
                    using (var writer = new StreamWriter(newFile))
                    {
                        StreamReader file = new StreamReader(filename);
                        string line;
                        while ((line = file.ReadLine()) != null)
                        {
                            writer.WriteLine(line);
                        }
                    }
                }
            }
            else
            {
                File.Create(newFile);
                foreach (var filename in path)
                {
                    using (var writer = new StreamWriter(newFile))
                    {
                        StreamReader file = new StreamReader(filename);
                        string line;
                        while ((line = file.ReadLine()) != null)
                        {
                            writer.WriteLine(line);
                        }
                    }
                }
            }
        }

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                var read = Console.ReadLine();
                Console.WriteLine(read);
                return;
            }
            if (args.Length > 0)
            {
                //вывод одного или нескольких файлов по очереди
                var key = "";
                List<string> filePath = new List<string>();

                //перенаправление ввода в файл
                if (args.Length == 3)
                {
                    if (args[1] == ">"
                        && args[2] != "")
                    {
                        var read = Console.Read();
                        if (File.Exists(args[2]))
                        {
                            using (var writer = new StreamWriter(args[2]))
                            {
                                writer.WriteLine(read);
                            }
                        }
                        else
                        {
                            File.Create(args[2]);
                            using (var writer = new StreamWriter(args[2]))
                            {
                                writer.WriteLine(read);
                            }
                        }
                        return;
                    }
                }
                
                //перенаправление содержимого файла/файлов в файл
                foreach (var arg in args)
                {
                    Console.WriteLine("Перенаправление");
                    var index = args.ToList().IndexOf(arg);
                    if (File.Exists(arg))
                    {
                        filePath.Add(arg);
                    }

                    if (arg == ">")
                    {
                        outToFile(filePath, args[index + 1]);
                        return;
                    }
                }

                foreach (var filename in args)
                {
                    Console.WriteLine("вывод");
                    //если ключ перенаправления то пишем вывод в файл после этого ключа и выходим из программы

                    if (args[0].ToLower() == "-b")
                    {
                        key = "-b";
                    }

                    if (args[0].ToLower() == "-n")
                    {
                        key = "-n";
                    }

                    if (args[0].ToLower() == "-s")
                    {
                        key = "-s";
                    }

                    if (args[0].ToLower() == "-e")
                    {
                        key = "-e";
                    }

                    if (File.Exists(filename)
                        && filename != "cat"
                        && filename != key)
                    {
                        outputFunc(filename, key);
                    }
                    //комбинация вывода из файла и из стандартного ввода
                    else if (filename == "-")
                    {
                        string read = "";
                        read = Console.ReadLine();
                        Console.WriteLine(read);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("Неверно введена команда");
            }
        }
    }
}