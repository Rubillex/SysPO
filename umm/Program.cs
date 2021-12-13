using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace umm
{
    internal class Program
    {
        public static int Main(string[] args)
        {
            //заготовка для вывода текста
            var fileOut = "";
            //для рассчёта md5
            var md5 = MD5.Create();
            //хэш карта для удобного вывода
            var myHashMap = new Dictionary<string,string>();

            if (args.Length > 0)
            {
                if (args[0] == "--version")
                {
                    Console.WriteLine("version: 123");
                } 
                else if(args[0] == "--help")
                {
                    Console.WriteLine("HELP");
                } else
                    //если это не проверка, то
                if (args[0] != "-c" && args[0] != "--check")
                {
                    //идём по всем ключам т.к. не знаем сколько файлов мы подали на проверку
                    foreach (var key in args)
                    {
                        //индекст ключа
                        var index = args.ToList().IndexOf(key);
                        //если файл существует, проверяем его контрольную сумму
                        if (File.Exists(key))
                        {
                            var hashSumm = BitConverter.ToString(md5.ComputeHash(File.OpenRead(key))).Replace("-", "").ToLowerInvariant();
                            myHashMap.Add(key, hashSumm);
                            fileOut += hashSumm;
                            fileOut += "\n";
                        }
                        //если ключ перенаправления то пишем вывод в файл после этого ключа и выходим из программы
                        if (key == ">")
                        {
                            try
                            {
                                if (File.Exists(args[index+1]))
                                {
                                    using (var writer = new StreamWriter(args[index + 1]))  
                                    {  
                                        writer.WriteLine(fileOut);
                                    }
                                }
                                else
                                {
                                    File.Create(args[index + 1]);
                                    using (var writer = new StreamWriter(args[index + 1]))  
                                    {  
                                        writer.WriteLine(fileOut);
                                    }
                                }
                                return 0;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("после > должно быть имя файла");
                                throw;
                            }
                        }
                    }
                    //на случай, если перенаправления нет
                    foreach (var VARIABLE in myHashMap)
                    {
                        Console.WriteLine(VARIABLE.Value + " " + VARIABLE.Key);
                    }
                }
                //если проверка "-c"
                else
                {   
                    //если файл с суммами и именами файлов существует
                    if (File.Exists(args[1]))
                    {
                        //читаем этот файл
                        List<String> textFromFile = new List<string>();
                        StreamReader file = new StreamReader(args[1]);
                        string line;
                        while ((line = file.ReadLine()) != null)
                        {
                            textFromFile.Add(line);
                        }
                        
                        foreach (var textLine in textFromFile)
                        {
                            try
                            {
                                string[] text = textLine.Split(' ');
                                //проверяем новую сумму и сравниваем со старой
                                var fileSumm = BitConverter.ToString(md5.ComputeHash(File.OpenRead(text[1]))).Replace("-", "").ToLowerInvariant();
                                if (fileSumm == text[0])
                                {
                                    Console.WriteLine(text[1] + ": ЦЕЛ");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                throw;
                            }
                        }
                    }
                }
            }
            return 0;
        }
    }
}