using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace tail
{
    internal class Program
    {
        public static int Main(string[] args)
        {

            int linesCount;
            int tempLine;
            
                switch (args.Length)
                {   
                    //КЕЙСЫ С ПРОВЕРКАМИ НА ДУРАКА

                    case 1 when args[0] == "--help":
                    {
                        Console.Write("Для вывода 10 последних строк из файла, воспользуйтесь командой 'tail filename' или 'tail -n filename'\n" +
                                      "Для вывода N последних строк из файла, воспользуйтесь командой 'tail -n N filename'\n" +
                                      "Для вывода 10 последних байт из файла, воспользуйтесь кмандой 'tail -c filename'\n" +
                                      "Для вывода N последних байт из файла, воспользуйтесь кмандой 'tail -c  N filename'\n" +
                                      "Проверка версии 'tail -version'");
                        return 0;
                    }
                    case 1 when args[0] == "--version":
                    {
                        Console.WriteLine("version: 1.0");
                        return 0;
                    }
                    
                    //-n
                    //tail filename
                    case 1 when args[0] != "-n" && args[0] != "-c":
                    {
                        if (File.Exists(args[0]))
                        {
                            linesCount = 10;
                            List<String> textFromFile = new List<string>();

                            StreamReader file = new StreamReader(args[0]);
                            string line;
                            while ((line = file.ReadLine()) != null)
                            {
                                textFromFile.Add(line);
                            }
                            
                            for (var i = textFromFile.Count - linesCount; i < textFromFile.Count; i++)
                            {
                                Console.WriteLine(textFromFile[i]);
                            }

                        }

                        break;
                    }
                    //tail N
                    case 2 when int.TryParse(args[1], out linesCount):
                        return 1;
                    //tail -n filename
                    case 2 when args[0] == "-n":
                    {
                        if (File.Exists(args[1]))
                        {
                            linesCount = 10;
                            var textFromFile = new List<string>();

                            var file = new StreamReader(args[1]);
                            string line;
                            while ((line = file.ReadLine()) != null)
                            {
                                textFromFile.Add(line);
                            }

                            for (var i = textFromFile.Count - linesCount; i < textFromFile.Count; i++)
                            {
                                Console.WriteLine(textFromFile[i]);
                            }
                            
                        }
                        
                        break;
                    }
                    //tail -n N filename
                    case 3 when args[0] == "-n" && int.TryParse(args[1], out linesCount) &&
                                !int.TryParse(args[2], out tempLine):
                    {
                        if (File.Exists(args[2]))
                        {
                            var textFromFile = new List<string>();

                            var file = new StreamReader(args[2]);
                            string line;
                            while ((line = file.ReadLine()) != null)
                            {
                                textFromFile.Add(line);
                            }

                            for (var i = textFromFile.Count - linesCount; i < textFromFile.Count; i++)
                            {
                                Console.WriteLine(textFromFile[i]);
                            }
                            
                        }
                        return 0;
                    }
                    
                    //-c
                    
                    //tail -c filename
                    case 2 when args[0] == "-c" && !int.TryParse(args[1], out tempLine):
                    {
                        try
                        {
                            using (var fsSource = new FileStream(args[1],
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

                                for (var i = bytes.Length - 10; i < bytes.Length; i++)
                                {
                                    Console.WriteLine(bytes[i]);
                                }
                                
                            }
                        }
                        catch (FileNotFoundException ioEx)
                        {
                            Console.WriteLine(ioEx.Message);
                        }
                        
                        return 0;
                    }
                    //tail -c N filename
                    case 3 when args[0] == "-c" && 
                                int.TryParse(args[1], out linesCount) &&
                                !int.TryParse(args[2], out tempLine):
                    {
                        try
                        {
                            using (var fsSource = new FileStream(args[2],
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

                                for (var i = bytes.Length - linesCount; i < bytes.Length; i++)
                                {
                                    Console.WriteLine(bytes[i]);
                                }
                                
                            }
                        }
                        catch (FileNotFoundException ioEx)
                        {
                            Console.WriteLine(ioEx.Message);
                        }
                        
                        return 0;
                    }
                }
            

            return 0;
        }
    }
}