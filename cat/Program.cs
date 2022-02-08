using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace cat
{
    internal class Program
    {
        public static void outputFunc(string path, string key)
        {
            // Console.WriteLine("path: " + path + " key: " + key);
            List<String> textFromFile = new List<string>();
            string output = "";
            var numLine = 1;
            
            byte[] buffer = File.ReadAllBytes(path);

            string[] h = buffer.Select(x => x.ToString("x2")).ToArray();
            string hex = string.Concat(h);

            string[] hexBytes = new string[hex.Length / 2];
            for (int i = 0; i < hexBytes.Length; i++)
            {
                hexBytes[i] = hex.Substring(i * 2, 2);
            }
            byte[] resultBytes = hexBytes.Select(value => Convert.ToByte(value, 16)).ToArray();
            string result = Encoding.UTF8.GetString(resultBytes);

            string[] lines = result.Split('\n');

            foreach (var line in lines)
            {
                switch (key)
                {
                    //нумерация непустых строк
                    case "-b":
                        //если в строке больше чем один символ
                        if (line.Length != 1)
                        {
                            output += numLine;
                            numLine++;
                            output += " ";
                            output += line;
                            textFromFile.Add(output);
                            output = "";
                        }
                        else
                        {
                            //если строка не последняя
                            if (line != lines[lines.Length - 1])
                            {
                                output += line;
                                textFromFile.Add(output);
                                output = "";
                            }
                            //если строка последняя
                            else
                            {
                                output += numLine;
                                numLine++;
                                output += " ";
                                output += line;
                                textFromFile.Add(output);
                                output = "";
                            }
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
                        if (textFromFile.Count > 0)
                        {
                            if (output == textFromFile.Last() 
                                && output.Length == 1 
                                && output != lines[lines.Length - 1])
                            {
                                textFromFile.RemoveAt(textFromFile.Count-1);
                            }
                            else
                            {
                                textFromFile.Add(output);
                            }
                        }
                        else
                        {
                            textFromFile.Add(output);
                        }
                        output = "";
                        break;
                    //$ в конце каждой строки
                    case "-e":
                        StringBuilder tempLine = new StringBuilder(line);
                        if (line != lines[lines.Length - 1])
                        {
                            tempLine[tempLine.Length - 1] = '$';
                            Console.WriteLine(tempLine);
                        }
                        else
                        {
                            tempLine.Append("$");
                            Console.WriteLine(tempLine);
                        }
                        
                        break;
                    case "-t":
                        StringBuilder tempLine_t = new StringBuilder(line);
                        tempLine_t.Replace("\t", "^I");
                        Console.WriteLine(tempLine_t);
                        break;
                    case "":
                        output += line;
                        textFromFile.Add(output);
                        output = "";
                        break;
                }
            }
            //вывод
            
            foreach (var lineFromList in textFromFile)
            {
                Console.WriteLine(lineFromList);
            }
            textFromFile.Clear();
        }

        public static void help()
        {
            Console.WriteLine("{0,-5}   {1,-35}","-b", "нумерует только непустые строки");
            Console.WriteLine("{0,-5}   {1,-35}","-E", "показывает символ $ в конце каждой строки");
            Console.WriteLine("{0,-5}   {1,-35}","-n", "нумерует все строки");
            Console.WriteLine("{0,-5}   {1,-35}","-s", "удаляет пустые повторяющиеся строки");
            Console.WriteLine("{0,-5}   {1,-35}","-T", "отображает табуляции в виде ^I");
            Console.WriteLine("{0,-5}   {1,-35}","--help", "справка");
            Console.WriteLine("{0,-5}   {1,-35}","--version", "версия");
        }

        public static void version()
        {
            Console.WriteLine("cat (GNU coreutils) 8.30" + "\n" +
                              "Copyright (C) 2018 Free Software Foundation, Inc." + "\n" +
                              "License GPLv3+: GNU GPL version 3 or later <https://gnu.org/licenses/gpl.html>." + "\n" +
                              "This is free software: you are free to change and redistribute it. +" + "\n" +
                              "There is NO WARRANTY, to the extent permitted by law."+ "\n" +
                              "\n" +
                              "Written by Torbjorn Granlund and Richard M. Stallman.");
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

                bool v = false;
                bool h = false;
                
                foreach (var keys in args)
                {
                    if (keys == "--version")
                    {
                        v = true;
                        if (!h)
                        {
                            version();
                        }
                    }

                    if (keys == "--help")
                    {
                        h = true;
                        if (!v)
                        {
                            help();
                        }
                    }
                }


                foreach (var filename in args)
                {
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

                    if (args[0].ToLower() == "-t")
                    {
                        key = "-t";
                    }

                    if (File.Exists(filename))
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
                }
            }
            else
            {
                Console.WriteLine("Неверно введена команда");
            }
        }
    }
}