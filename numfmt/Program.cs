using System;

namespace numfmt
{
    internal class Program
    {
        
        public static void help()
        {
            Console.WriteLine("{0,-5}   {1,-35}","--from=iec", "пример: 1K -> 1024");
            Console.WriteLine("{0,-5}   {1,-35}","--from=iec", "пример: 1Ki -> 1024");
            Console.WriteLine("{0,-5}   {1,-35}","--from=si", "пример: 1K -> 1000");
            Console.WriteLine("{0,-5}   {1,-35}","--help", "справка");
            Console.WriteLine("{0,-5}   {1,-35}","--version", "версия");
        }

        public static void version()
        {
            Console.WriteLine("numfmt (GNU coreutils) 8.30" + "\n" +
                              "Copyright (C) 2018 Free Software Foundation, Inc." + "\n" +
                              "License GPLv3+: GNU GPL version 3 or later <https://gnu.org/licenses/gpl.html>." + "\n" +
                              "This is free software: you are free to change and redistribute it. +" + "\n" +
                              "There is NO WARRANTY, to the extent permitted by law."+ "\n" +
                              "\n" +
                              "Written by Torbjorn Granlund and Richard M. Stallman.");
        }
        
        public static void Main(string[] args)
        {
            string[] modify = {"K", "M", "G", "T", "P", "E"};
            string[] modify_i = {"Ki", "Mi", "Gi", "Ti", "Pi", "Ei"};

            if (args.Length > 0)
            {
                
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
            }
            
            if (args.Length == 2)
            {
                var num = args[0];
                var key = args[1];

                var mn = 1;

                switch (key)
                {
                    case "--from=si":
                        foreach (var mod in modify)
                        {
                            if (num.Contains(mod))
                            {
                                var nums = num.Replace(mod, "");
                                if (nums.Contains("."))
                                {
                                    try
                                    {
                                        string[] number = nums.Split('.');

                                        double itog = Convert.ToInt32(number[0]);
                                        double itog_i = Convert.ToInt32(number[1]);
                                        Console.WriteLine(itog + " " + itog_i);
                                        while (itog_i >= 1)
                                        {
                                            itog_i /= 10;
                                        }

                                        Console.WriteLine(itog + " " + itog_i);

                                        itog += itog_i;
                                        Console.WriteLine(itog + " " + itog_i);
                                        switch (mod)
                                        {
                                            case "K":
                                                itog *= Math.Pow(10, 3);
                                                Console.WriteLine(itog);
                                                break;
                                            case "M":
                                                itog *= Math.Pow(10, 6);
                                                Console.WriteLine(itog);
                                                break;
                                            case "G":
                                                itog *= Math.Pow(10, 9);
                                                Console.WriteLine(itog);
                                                break;
                                            case "P":
                                                itog *= Math.Pow(10, 12);
                                                Console.WriteLine(itog);
                                                break;
                                            case "T":
                                                itog *= Math.Pow(10, 15);
                                                Console.WriteLine(itog);
                                                break;
                                            case "E":
                                                itog *= Math.Pow(10, 18);
                                                Console.WriteLine(itog);
                                                break;
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                        throw;
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        double itog = Convert.ToInt32(nums);
                                        switch (mod)
                                        {
                                            case "K":
                                                itog *= Math.Pow(10, 3);
                                                Console.WriteLine(itog);
                                                break;
                                            case "M":
                                                itog *= Math.Pow(10, 6);
                                                Console.WriteLine(itog);
                                                break;
                                            case "G":
                                                itog *= Math.Pow(10, 9);
                                                Console.WriteLine(itog);
                                                break;
                                            case "P":
                                                itog *= Math.Pow(10, 12);
                                                Console.WriteLine(itog);
                                                break;
                                            case "T":
                                                itog *= Math.Pow(10, 15);
                                                Console.WriteLine(itog);
                                                break;
                                            case "E":
                                                itog *= Math.Pow(10, 18);
                                                Console.WriteLine(itog);
                                                break;
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

                        break;
                    case "--from=iec":
                        foreach (var mod in modify)
                        {
                            if (num.Contains(mod))
                            {
                                var nums = num.Replace(mod, "");
                                if (nums.Contains("."))
                                {
                                    try
                                    {
                                        string[] number = nums.Split('.');

                                        double itog = Convert.ToInt32(number[0]);
                                        double itog_i = Convert.ToInt32(number[1]);
                                        Console.WriteLine(itog + " " + itog_i);
                                        while (itog_i >= 1)
                                        {
                                            itog_i /= 10;
                                        }

                                        Console.WriteLine(itog + " " + itog_i);

                                        itog += itog_i;
                                        Console.WriteLine(itog + " " + itog_i);
                                        switch (mod)
                                        {
                                            case "K":
                                                itog *= Math.Pow(2, 10);
                                                Console.WriteLine(itog);
                                                break;
                                            case "M":
                                                itog *= Math.Pow(2, 20);
                                                Console.WriteLine(itog);
                                                break;
                                            case "G":
                                                itog *= Math.Pow(2, 30);
                                                Console.WriteLine(itog);
                                                break;
                                            case "P":
                                                itog *= Math.Pow(2, 40);
                                                Console.WriteLine(itog);
                                                break;
                                            case "T":
                                                itog *= Math.Pow(2, 50);
                                                Console.WriteLine(itog);
                                                break;
                                            case "E":
                                                itog *= Math.Pow(2, 60);
                                                Console.WriteLine(itog);
                                                break;
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                        throw;
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        double itog = Convert.ToInt32(nums);
                                        switch (mod)
                                        {
                                            case "K":
                                                itog *= Math.Pow(2, 10);
                                                Console.WriteLine(itog);
                                                break;
                                            case "M":
                                                itog *= Math.Pow(2, 20);
                                                Console.WriteLine(itog);
                                                break;
                                            case "G":
                                                itog *= Math.Pow(2, 30);
                                                Console.WriteLine(itog);
                                                break;
                                            case "P":
                                                itog *= Math.Pow(2, 40);
                                                Console.WriteLine(itog);
                                                break;
                                            case "T":
                                                itog *= Math.Pow(2, 50);
                                                Console.WriteLine(itog);
                                                break;
                                            case "E":
                                                itog *= Math.Pow(2, 60);
                                                Console.WriteLine(itog);
                                                break;
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

                        break;
                    case "--from=iec-i":

                        if (num.Contains("i"))
                        {
                            foreach (var mod in modify_i)
                            {
                                if (num.Contains(mod))
                                {
                                    var nums = num.Replace(mod, "");
                                    if (nums.Contains("."))
                                    {
                                        string[] number = nums.Split('.');

                                        double itog = Convert.ToInt32(number[0]);
                                        double itog_i = Convert.ToInt32(number[1]);
                                        Console.WriteLine(itog + " " + itog_i);
                                        while (itog_i >= 1)
                                        {
                                            itog_i /= 10;
                                        }

                                        Console.WriteLine(itog + " " + itog_i);

                                        itog += itog_i;
                                        Console.WriteLine(itog + " " + itog_i);
                                        switch (mod)
                                        {
                                            case "Ki":
                                                itog *= Math.Pow(2, 10);
                                                Console.WriteLine(itog);
                                                break;
                                            case "Mi":
                                                itog *= Math.Pow(2, 20);
                                                Console.WriteLine(itog);
                                                break;
                                            case "Gi":
                                                itog *= Math.Pow(2, 30);
                                                Console.WriteLine(itog);
                                                break;
                                            case "Pi":
                                                itog *= Math.Pow(2, 40);
                                                Console.WriteLine(itog);
                                                break;
                                            case "Ti":
                                                itog *= Math.Pow(2, 50);
                                                Console.WriteLine(itog);
                                                break;
                                            case "Ei":
                                                itog *= Math.Pow(2, 60);
                                                Console.WriteLine(itog);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        double itog = Convert.ToInt32(nums);
                                        switch (mod)
                                        {
                                            case "Ki":
                                                itog *= Math.Pow(2, 10);
                                                Console.WriteLine(itog);
                                                break;
                                            case "Mi":
                                                itog *= Math.Pow(2, 20);
                                                Console.WriteLine(itog);
                                                break;
                                            case "Gi":
                                                itog *= Math.Pow(2, 30);
                                                Console.WriteLine(itog);
                                                break;
                                            case "Pi":
                                                itog *= Math.Pow(2, 40);
                                                Console.WriteLine(itog);
                                                break;
                                            case "Ti":
                                                itog *= Math.Pow(2, 50);
                                                Console.WriteLine(itog);
                                                break;
                                            case "Ei":
                                                itog *= Math.Pow(2, 60);
                                                Console.WriteLine(itog);
                                                break;
                                        }
                                    }
                                }
                            }
                        }

                        break;
                }
            }
            else
            {
                Console.WriteLine("error");
            }

            return;
        }
    }
}