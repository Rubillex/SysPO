using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text.RegularExpressions;

namespace seq
{
    internal class Program
    {
        public static int Main(string[] args) {
            float start, end, inc;
            const string patternSplit = @"(.*?)";

            Regex rgx = new Regex(patternSplit);
            switch (args.Length)
            {
                //seq N
                case 1 when float.TryParse(args[0], out start): {
                    for (var i = 1; i <= start; i++) {
                        Console.WriteLine(i);
                    }
                    return 0;
                }
                case 1 when args[0] == "--help": {
                
                    Console.Write("Чтобы посчитать до  N, воспльзуйтесь ключoм N\n" +
                                  "Чтобы посчитать от N до M, воспльзуйтесь ключами N M\n" +
                                  "Чтобы посчитать от N до M с шагом K, воспользуйтесь ключами N K M\n" +
                                  "Для изменения разделителя в выводе используйте -s \"%S\", а далее любой из выше перечисленных ключей\n" +
                                  "Для форматирования вывода используйте -f \"*%*.*X*\", где X - любой из следующих символов: e, f, g, c. А после данной команды можно вводить начало и конец отсчёта, а так же инкремент");
                    
                    return 0;
                }
                case 1 when args[0] == "--version": {
                    Console.WriteLine("version: 0.1");
                    return 0;
                }
                case 1: return 1;
                
                //seq N M
                case 2 when float.TryParse(args[0], out start) && float.TryParse(args[1], out end): {
                    //смотрим в какую сторону считаем
                    if (start <= end) {
                        for (var i = start; i <= end; i++) {
                            Console.WriteLine(i);
                        }
                    } else {
                        for (var i = start; i >= end; i--) {
                            Console.WriteLine(i);
                        }
                    }
                    return 0;
                }
                
                //seq -w N
                case 2 when args[0] == "-w" &&
                            float.TryParse(args[1], out end):
                {
                    var lengthSym = end.ToString().Length;

                    
                    
                    for (float i = 0; i <= end; i++)
                    {
                        var lengthThisSym = Math.Round(i,1).ToString().Length;
                        if (lengthSym > lengthThisSym)
                        {
                            for (var j = 0; j < lengthSym - lengthThisSym; j++)
                            {
                                Console.Write(0);
                            }
                        }
                        Console.Write(i + "\n");
                    }
                    
                    return 0;
                }

                case 2: return 1;
                
                //seq N M K
                case 3 when float.TryParse(args[0], out start) &&
                            float.TryParse(args[1], out inc) &&
                            float.TryParse(args[2], out end): {

                    if (start <= end) {
                        if (inc > 0)
                        {
                            for (var i = start; i <= end; i+=inc) {
                                Console.WriteLine(i);
                            } 
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        //если мы идём от большего к меньшему с положительным шагом, то прога бьёт по голове
                        if (inc > 0)
                        {
                            return 1;
                        }
                        else
                        {
                            for (var i = start; i >= end; i+=inc) {
                                Console.WriteLine(i);
                            }
                        }
                    }

                    return 0;
                }
                
                //seq -w N M
                case 3 when args[0] == "-w" &&
                            float.TryParse(args[1], out start) &&
                            float.TryParse(args[2], out end):
                {
                    
                    //определяем длина какого из чисел больше
                    var lengthSymStart = start.ToString().Length;
                    var lengthSymEnd = end.ToString().Length;
                    var lengthSym = Math.Max(lengthSymStart, lengthSymEnd);
                    int lengthThisSym;

                    if (start <= end)
                    {
                        for (var i = start; i <= end; i++)
                        {
                            lengthThisSym = Math.Round(i,1).ToString().Length;
                            if (lengthSym >= lengthThisSym)
                            {
                                if (i < 0)
                                {
                                     Console.Write("-");
                                     for (var j = 0; j < lengthSym - lengthThisSym; j++)
                                     {
                                         Console.Write(0);
                                     }
                                }
                                else
                                {
                                    for (var j = 0; j < lengthSym - lengthThisSym; j++)
                                    {
                                        Console.Write(0);
                                    }
                                }
                            }
                            Console.Write(Math.Round(Math.Abs(i),1) + "\n");
                        }
                    }
                    else
                    {
                        for (var i = start; i >= end; i--)
                        {
                            lengthThisSym = Math.Round(Math.Abs(i),1).ToString().Length;
                            if (lengthSym > lengthThisSym)
                            {
                                if (i < 0) Console.Write("-");
                                for (var j = 0; j < lengthSym - lengthThisSym - 1; j++)
                                {
                                    Console.Write(0);
                                }
                            }
                            Console.Write(Math.Round(Math.Abs(i),1) + "\n");
                        }
                    }


                    return 0;
                }
                
                //seq -s ".*" N
                case 3 when args[0] == "-s" &&
                            rgx.IsMatch(args[1]) &&
                            float.TryParse(args[2], out end):
                {
                    
                    //получаем разделитель
                    var split = args[1].Substring(0, args[1].Length);
                    
                    for (var i = 0; i <= end; i++) {
                        Console.Write(i);
                        if(i != end) Console.Write(split);
                    }
                    
                    return 0;
                }
                
                //seq -f '.*%?.*' N
                case 3 when args[0] == "-f" &&
                            float.TryParse(args[2], out end):
                {
                    string[] words = args[1].Split('%');
                    
                    //разделители
                    char sym = ' ';

                    Console.WriteLine(words[1]);
                    
                    foreach (var VARIABLE in words[1])
                    {
                        if (char.ToLower(VARIABLE) == 'e' ||
                            char.ToLower(VARIABLE) == 'f' ||
                            char.ToLower(VARIABLE) == 'g' ||
                            char.ToLower(VARIABLE) == 'c')
                        {
                            sym = char.ToLower(VARIABLE);
                            break;
                        }
                    }

                    //перед числом
                    string spliter1 = words[0].Substring(0);
                    //после числа
                    string spliter2 = "";
                    
                    string[] modifier = words[1].Split('e', 'f', 'g', 'c');

                    string[] symInModifier = modifier[0].Split('.');

                    bool zero = false;
                        
                    int before = 0;
                    int afther = 0;
                    if (symInModifier.Length > 0)
                    {
                        if (words[1].StartsWith("0")) zero = true;
                        
                        int.TryParse(symInModifier[0], out before);
                    }

                    if (symInModifier.Length > 1)
                    {
                        int.TryParse(symInModifier[1], out afther);
                    }

                    Console.WriteLine("before: " + before + " afther: " + afther);
                    
                    for (var i = 1; i < modifier.Length; i++)
                    {
                        spliter2 += modifier[i];
                    }

                    double beforeSym = Math.Pow(10, before);

                    double consoleOut = 0;
                    string outer = "";

                    string outFormat = "";
                    for (float i = 1; i <= end; i++)
                    {
                        if (zero)
                        {
                            outer += spliter1.ToString();
                            if (before > 0)
                            {
                                consoleOut = i % beforeSym;
                            }
                            else
                            {
                                consoleOut = i;
                            }
                            if (afther > 0)
                            {
                                outFormat += String.Format("{0:" + sym + afther + "}", consoleOut);
                            }
                            else
                            {
                                outFormat += String.Format("{0:" + sym + "}", consoleOut);
                            }
                            
                            for (int j = 0; j < before - outFormat.ToString().Length; j++)
                            {
                                outer += "0";
                            }

                            outer += outFormat;
                            outer += spliter2.ToString();
                            Console.WriteLine(outer);
                            outer = "";
                            outFormat = "";
                        }
                        else
                        {
                            Console.Write(spliter1);
                            if (afther > 0 && before > 0)
                            {
                                consoleOut = i % beforeSym;
                                Console.Write("{0," + before  + ":" + sym + afther + "}", consoleOut);
                            }

                            if (afther > 0 && before == 0)
                            {
                                consoleOut = i;
                                Console.Write("{0:" + sym + afther + "}", consoleOut);
                            }

                            if (afther == 0 && before > 0)
                            {
                                consoleOut = i % beforeSym;
                                Console.Write("{0," + before  + ":" + before  + ":" + sym + "}", consoleOut);
                            }
                            if (afther == 0 && before == 0) Console.Write("{0:" + sym + "}", i); 
                            Console.Write(spliter2 + '\n');
                        }
                    }
                    
                    return 0;
                }


                case 3: return 1;

                
                //seq -f '.*%?.*' N M
                case 4 when args[0] == "-f" &&
                            float.TryParse(args[2], out start) &&
                            float.TryParse(args[3], out end):
                {
                    string[] words = args[1].Split('%');
                    
                    //разделители
                    char sym = ' ';
                    
                    
                    Console.WriteLine(words[1]);
                    
                    foreach (var VARIABLE in words[1])
                    {
                        if (char.ToLower(VARIABLE) == 'e' ||
                            char.ToLower(VARIABLE) == 'f' ||
                            char.ToLower(VARIABLE) == 'g' ||
                            char.ToLower(VARIABLE) == 'c')
                        {
                            sym = char.ToLower(VARIABLE);
                            break;
                        }
                    }

                    //перед числом
                    string spliter1 = words[0].Substring(0);
                    //после числа
                    string spliter2 = "";
                    
                    string[] modifier = words[1].Split('e', 'f', 'g', 'c');

                    string[] symInModifier = modifier[0].Split('.');

                    bool zero = false;
                        
                    int before = 0;
                    int afther = 0;
                    if (symInModifier.Length > 0)
                    {
                        if (words[1].StartsWith("0")) zero = true;
                        
                        int.TryParse(symInModifier[0], out before);
                    }

                    if (symInModifier.Length > 1)
                    {
                        int.TryParse(symInModifier[1], out afther);
                    }
                    
                    Console.WriteLine("before: " + before + " afther: " + afther);
                    
                    for (var i = 1; i < modifier.Length; i++)
                    {
                        spliter2 += modifier[i];
                    }

                    double beforeSym = Math.Pow(10, before);

                    double consoleOut = 0;

                    string outer = "";
                    string outFormat = "";
                    if (start <= end)
                    {
                        for (float i = start; i <= end; i++)
                        {
                            if (zero)
                        {
                            outer += spliter1.ToString();
                            if (before > 0)
                            {
                                consoleOut = i % beforeSym;
                            }
                            else
                            {
                                consoleOut = i;
                            }
                            if (afther > 0)
                            {
                                outFormat += String.Format("{0:" + sym + afther + "}", consoleOut);
                            }
                            else
                            {
                                outFormat += String.Format("{0:" + sym + "}", consoleOut);
                            }
                            
                            for (int j = 0; j < before - outFormat.ToString().Length; j++)
                            {
                                outer += "0";
                            }

                            outer += outFormat;
                            outer += spliter2.ToString();
                            Console.WriteLine(outer);
                            outer = "";
                            outFormat = "";
                        }
                        else
                        {
                            Console.Write(spliter1);
                            if (afther > 0 && before > 0)
                            {
                                consoleOut = i % beforeSym;
                                Console.Write("{0," + before  + ":" + sym + afther + "}", consoleOut);
                            }

                            if (afther > 0 && before == 0)
                            {
                                consoleOut = i;
                                Console.Write("{0:" + sym + afther + "}", consoleOut);
                            }

                            if (afther == 0 && before > 0)
                            {
                                consoleOut = i % beforeSym;
                                Console.Write("{0," + before  + ":" + before  + ":" + sym + "}", consoleOut);
                            }
                            if (afther == 0 && before == 0) Console.Write("{0:" + sym + "}", i); 
                            Console.Write(spliter2 + '\n');
                        }
                        }   
                    }
                    else
                    {
                        for (float i = start; i >= end; i--)
                        {
                            if (zero)
                        {
                            outer += spliter1.ToString();
                            if (before > 0)
                            {
                                consoleOut = i % beforeSym;
                            }
                            else
                            {
                                consoleOut = i;
                            }
                            if (afther > 0)
                            {
                                outFormat += String.Format("{0:" + sym + afther + "}", consoleOut);
                            }
                            else
                            {
                                outFormat += String.Format("{0:" + sym + "}", consoleOut);
                            }
                            
                            for (int j = 0; j < before - outFormat.ToString().Length; j++)
                            {
                                outer += "0";
                            }

                            outer += outFormat;
                            outer += spliter2.ToString();
                            Console.WriteLine(outer);
                            outer = "";
                            outFormat = "";
                        }
                        else
                        {
                            Console.Write(spliter1);
                            if (afther > 0 && before > 0)
                            {
                                consoleOut = i % beforeSym;
                                Console.Write("{0," + before  + ":" + sym + afther + "}", consoleOut);
                            }

                            if (afther > 0 && before == 0)
                            {
                                consoleOut = i;
                                Console.Write("{0:" + sym + afther + "}", consoleOut);
                            }

                            if (afther == 0 && before > 0)
                            {
                                consoleOut = i % beforeSym;
                                Console.Write("{0," + before  + ":" + before  + ":" + sym + "}", consoleOut);
                            }
                            if (afther == 0 && before == 0) Console.Write("{0:" + sym + "}", i); 
                            Console.Write(spliter2 + '\n');
                        }
                        }
                    }

                    return 0;
                }

                //seq -s ".*" N M
                case 4 when args[0] == "-s" &&
                            rgx.IsMatch(args[1]) &&
                            float.TryParse(args[2], out start) &&
                            float.TryParse(args[3], out end):
                {
                    
                    //получаем разделитель
                    var split = args[1].Substring(0, args[1].Length);

                    if (start <= end) {
                        for (var i = start; i <= end; i++) {
                            Console.Write(i);
                            if(i != end) Console.Write(split);
                        }
                    }
                    else {
                        for (var i = start; i >= end; i--) {
                            Console.Write(i);
                            if(i != end) Console.Write(split);
                        }
                    }
                    
                    
                    
                    return 0;
                }
                
                //seq -w N M K
                case 4 when args[0] == "-w" &&
                            float.TryParse(args[1], out start) &&
                            float.TryParse(args[2], out inc) &&
                            float.TryParse(args[3], out end):
                {
                    //определяем длина какого из чисел больше
                    int lengthSymStart = start.ToString().Length;
                    int lengthSymEnd = end.ToString().Length;
                    var lengthSym = Math.Max(lengthSymStart, lengthSymEnd);
                    int lengthThisSym;

                    string[] first = start.ToString().Split(',');
                    string[] second = end.ToString().Split(',');

                    int zeroLen = 0;
                    
                    if (first.Length > 1 && second.Length > 1)
                    {
                        zeroLen = Math.Max(first[1].Length, second[1].Length);
                    }

                    if (first.Length > 1 && second.Length < 1)
                    {
                        zeroLen = first[1].Length;
                    }
                    if (second.Length > 1 && first.Length < 1)
                    {
                        zeroLen = second[1].Length;
                    }
                    
                    string increment = inc.ToString();
                    
                    string[] nums = increment.Split(',');

                    if (nums.Length > 1)
                    {
                        zeroLen = Math.Max(nums[1].Length, zeroLen);
                    }

                    string outer = "";
                    string outFormat = "";
                    float consoleOut = 0;
                    if (start <= end && inc > 0)
                    {
                        for (var i = start; i <= end; i += inc)
                        {
                            lengthThisSym = Math.Round(i).ToString().Length;
                            if (i < 0)
                            {
                                Console.Write("-");
                                if (Math.Round(i) == 0)
                                {
                                    for (int j = 0; j < lengthSym - lengthThisSym - 1; j++)
                                    {
                                        Console.Write(0);
                                    } 
                                }
                                else
                                {
                                    for (int j = 0; j < lengthSym - lengthThisSym; j++)
                                    {
                                        Console.Write(0);
                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < lengthSym - lengthThisSym; j++)
                                {
                                    Console.Write(0);
                                }
                            }
                            
                            consoleOut = Math.Abs(i);
                            
                            
                            if (zeroLen > 0)
                            {
                                outFormat += String.Format("{0:" + "f" + zeroLen + "}", consoleOut);
                            }
                            else
                            {
                                outFormat += String.Format("{0:" + "f" + "}", consoleOut);
                            }
                            for (int j = 0; j < lengthSym - outFormat.Length; j++)
                            {
                                outer += "0";
                            }

                            outer += outFormat;
                            Console.WriteLine(outer);
                            outer = "";
                            outFormat = "";
                            
                        }
                    }

                    // if (start <= end && inc > 0)
                    // {
                    //     for (var i = start; i <= end; i+=inc)
                    //     {
                    //         lengthThisSym = Math.Round(i, 1).ToString().Length;
                    //         if (lengthSym >= lengthThisSym)
                    //         {
                    //             if (i < 0)
                    //             {
                    //                 Console.Write("-");
                    //                 for (var j = 0; j < lengthSym - lengthThisSym; j++)
                    //                 {
                    //                     Console.Write(0);
                    //                 }
                    //             }
                    //             else
                    //             {
                    //                 for (var j = 0; j < lengthSym - lengthThisSym; j++)
                    //                 {
                    //                     Console.Write(0);
                    //                 }
                    //             }
                    //         }
                    //         Console.Write(Math.Round(Math.Abs(i),1) + "\n");
                    //     }
                    // }
                    // else
                    // {
                    //     if (inc > 0)
                    //     {
                    //         for (var i = start; i >= end; i-=inc)
                    //         {
                    //             lengthThisSym = Math.Round(Math.Abs(i),1).ToString().Length;
                    //             if (lengthSym >= lengthThisSym)
                    //             {
                    //                 if (i < 0)
                    //                 {
                    //                     Console.Write("-");
                    //                 }
                    //                 
                    //                 for (int j = 0; j < lengthSym - outFormat.ToString().Length; j++)
                    //                 {
                    //                     outer += "0";
                    //                 }
                    //                 
                    //             }
                    //             Console.Write(Math.Round(Math.Abs(i),1) + "\n");
                    //         }
                    //     }
                    //     else
                    //     {
                    //         for (var i = start; i >= end; i+=inc)
                    //         {
                    //             lengthThisSym = Math.Round(Math.Abs(i),1).ToString().Length;
                    //             if (lengthSym >= lengthThisSym)
                    //             {
                    //                 if (i < 0)
                    //                 {
                    //                     Console.Write("-");
                    //                     for (var j = 0; j < lengthSym - lengthThisSym; j++)
                    //                     {
                    //                         Console.Write(0);
                    //                     }
                    //                 }
                    //                 else
                    //                 {
                    //                     for (var j = 0; j < lengthSym - lengthThisSym; j++)
                    //                     {
                    //                         Console.Write(0);
                    //                     }
                    //                 }
                    //             }
                    //             Console.Write(Math.Round(Math.Abs(i),1) + "\n");
                    //         }
                    //     }
                    // }


                    return 0;
                } 
                
                //seq -f '.*%?.*' N M K
                case 5 when args[0] == "-f" &&
                            float.TryParse(args[2], out start) &&
                            float.TryParse(args[3], out inc) &&
                            float.TryParse(args[4], out end):{
                    string[] words = args[1].Split('%');
                    
                    //разделители
                    char sym = ' ';
                    
                    
                    Console.WriteLine(words[1]);
                    
                    foreach (var VARIABLE in words[1])
                    {
                        if (char.ToLower(VARIABLE) == 'e' ||
                            char.ToLower(VARIABLE) == 'f' ||
                            char.ToLower(VARIABLE) == 'g' ||
                            char.ToLower(VARIABLE) == 'c')
                        {
                            sym = char.ToLower(VARIABLE);
                            break;
                        }
                    }

                    //перед числом
                    string spliter1 = words[0].Substring(0);
                    //после числа
                    string spliter2 = "";
                    
                    string[] modifier = words[1].Split('e', 'f', 'g', 'c');

                    string[] symInModifier = modifier[0].Split('.');
                    bool zero = false;
                        
                    int before = 0;
                    int afther = 0;
                    if (symInModifier.Length > 0)
                    {
                        if (words[1].StartsWith("0")) zero = true;
                        
                        int.TryParse(symInModifier[0], out before);
                    }

                    if (symInModifier.Length > 1)
                    {
                        int.TryParse(symInModifier[1], out afther);
                    }
                    
                    Console.WriteLine("before: " + before + " afther: " + afther);
                    
                    for (var i = 1; i < modifier.Length; i++)
                    {
                        spliter2 += modifier[i];
                    }

                    double beforeSym = Math.Pow(10, before);

                    double consoleOut = 0;
                    string outer = "";
                    string outFormat = "";
                    if (start <= end)
                    {
                        for (float i = start; i <= end && inc > 0; i+=inc)
                        {
                            if (zero)
                        {
                            outer += spliter1.ToString();
                            if (before > 0)
                            {
                                consoleOut = i % beforeSym;
                            }
                            else
                            {
                                consoleOut = i;
                            }
                            if (afther > 0)
                            {
                                outFormat += String.Format("{0:" + sym + afther + "}", consoleOut);
                            }
                            else
                            {
                                outFormat += String.Format("{0:" + sym + "}", consoleOut);
                            }
                            
                            for (int j = 0; j < before - outFormat.ToString().Length; j++)
                            {
                                outer += "0";
                            }

                            outer += outFormat;
                            outer += spliter2.ToString();
                            Console.WriteLine(outer);
                            outer = "";
                            outFormat = "";
                        }
                        else
                        {
                            Console.Write(spliter1);
                            if (afther > 0 && before > 0)
                            {
                                consoleOut = i % beforeSym;
                                Console.Write("{0," + before  + ":" + sym + afther + "}", consoleOut);
                            }

                            if (afther > 0 && before == 0)
                            {
                                consoleOut = i;
                                Console.Write("{0:" + sym + afther + "}", consoleOut);
                            }

                            if (afther == 0 && before > 0)
                            {
                                consoleOut = i % beforeSym;
                                Console.Write("{0," + before  + ":" + before  + ":" + sym + "}", consoleOut);
                            }
                            if (afther == 0 && before == 0) Console.Write("{0:" + sym + "}", i); 
                            Console.Write(spliter2 + '\n');
                        }
                        }
                    }
                    else
                    {
                        for (float i = start; i >= end && inc < 0; i-=inc)
                        {
                            if (zero)
                            {
                                outer += spliter1.ToString();

                                if (afther > 0)
                                {
                                    outFormat += String.Format("{0:" + sym + afther + "}", i);
                                }
                                else
                                {
                                    outFormat += String.Format("{0:" + sym + "}", i);
                                }
                            
                                for (int j = 0; j < before - outFormat.ToString().Length; j++)
                                {
                                    outer += "0";
                                }

                                outer += outFormat;
                                outer += spliter2.ToString();
                                Console.WriteLine(outer);
                                outer = "";
                                outFormat = "";
                            }
                            else
                            {
                                Console.Write(spliter1);
                                if (afther > 0 && before > 0)
                                {
                                    Console.Write("{0," + before  + ":" + sym + afther + "}", i);
                                }

                                if (afther > 0 && before == 0)
                                {
                                    consoleOut = i;
                                    Console.Write("{0:" + sym + afther + "}", consoleOut);
                                }

                                if (afther == 0 && before > 0)
                                {
                                    Console.Write("{0," + before  + ":" + before  + ":" + sym + "}", i);
                                }
                                if (afther == 0 && before == 0) Console.Write("{0:" + sym + "}", i); 
                                Console.Write(spliter2 + '\n');
                            }
                        }
                    }

                    return 0;
                }
                
                //seq -s ".*" N M K
                case 5 when args[0] == "-s" &&
                            rgx.IsMatch(args[1]) &&
                            float.TryParse(args[2], out start) &&
                            float.TryParse(args[3], out inc) &&
                            float.TryParse(args[4], out end):
                {
                    
                    //получаем разделитель
                    var split = args[1].Substring(0, args[1].Length);

                    if (start <= end && inc > 0) {
                        for (var i = start; i <= end; i+=inc) {
                            Console.Write(i);
                            if(i != end) Console.Write(split);
                        }
                    }
                    else {
                        if (inc < 0) {
                            for (var i = start; i >= end; i+=inc) {
                                Console.Write(i);
                                if(i != end) Console.Write(split);
                            }
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    
                    
                    
                    return 0;
                }
            }
            return 0;
        }
    }
}