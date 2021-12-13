using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Threading;

namespace pole4ka
{
    
    internal class Program
    {
        static int currentLine = 0;
        static int symbolInLine = 8;
        
        static string hexLines;

        static List<String> lines = new List<string>();
                
        //получаем номер строки в 16-чном виде
        static string numberOfLine16(int num)
        {
            string[] B = { "0","1","2","3","4","5","6","7","8","9","A","B","C","D","E","F" };
            string answr = "";
            int[] A = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            int num2;
            int[] ssc = new int[6];
            for (int i = 0; i < 6; i++)
            {
                num2 = num / 16;
                ssc[i] = num - (num2 * 16);
                num = num2;
            }
            for (int j = 0; j < 6; j++)
            {
                for (int k = 0; k < 16; k++)
                { 
                    if (ssc[j] == A[k]) answr += B[k];
                }
        
            }
                    
            string temp = "";
        
            if (answr != null)
                for (int i = 0; i < answr.Length; i++)
                {
                    temp += answr[answr.Length - (i + 1)];
                }
        
            return temp;
        }
        
        static string ConvertStringToHex(String input)
        {
            Byte[] stringBytes = Encoding.ASCII.GetBytes(input);
            StringBuilder sbBytes = new StringBuilder(stringBytes.Length * 2);
            foreach (byte b in stringBytes)
            { 
                sbBytes.AppendFormat("{0:X2}", b);
            }
            return sbBytes.ToString();
        }
                
        static void Spliter() {
            lines.Clear(); //чистим старый массив
            string output = ""; //хранилище
            int num = 0; //количество байт (пар символов)
            int sym = symbolInLine; // количество байт в строке
            sym *= 2; //просто так надо
            
            
            for(int i = 0; i < hexLines.Length; i++){ //разбиваем строчку по парам
                if(i%2 == 0)//если номер символа чётный
                            output += " ";//добавляем пробел
                
                output += hexLines[i];
        
                //+1 пара
                num++;
        
        
                //если у нас количество пар совпадает с ограничением, то сохраняем строчку в массив
                if(num == sym){
                    lines.Add(output);
                    output = "";
                    num = 0;
                }
            }

            if(output != ""){
                lines.Add(output);
            }
        }
        
        static void openFile(string fileName)
        {
            if (fileName.Contains(".exe"))
            {
                byte[] buffer = File.ReadAllBytes(fileName);
                string base64Encoded = BitConverter.ToString(buffer).Replace("-","");

                string[] lines64 = base64Encoded.Split('\n');
                
                bool firstLine = true; //false
                    
                //0D 0A DD FF AA
            
                // \n = 0D 0A
                foreach (var line in lines64)
                {
                    if(firstLine){
                        firstLine = !firstLine;
                    } else{
                        hexLines += "0D0A";
                    }
                    hexLines += line;
                }
            }
            else
            {
                StreamReader file = new StreamReader(fileName);
                    
                bool firstLine = true; //false
                    
                //0D 0A DD FF AA
            
                // \n = 0D 0A
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if(firstLine){
                        firstLine = !firstLine;
                    } else{
                        hexLines += "0D0A";
                    }
                    hexLines += ConvertStringToHex(line);
                }
            }
        }
        
        static void updateScreen()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
        
            string output = "";
            //отображение 20 строк после текущей
            for(int i = currentLine; i < (currentLine + 20) && i < lines.Count; i++){
                output += numberOfLine16(i);
                output += " ";
                output += lines[i];
                if(i != currentLine + 19) output += '\n';
            }
            
            Console.Write(output);
        }

        public static void Main(string[] args)
        {
            Console.Title = "Pole4ka";
            Console.SetWindowSize(100, 50);

            if (!File.Exists("Lab2.exe"))
            {
                Process.GetCurrentProcess().Kill();
            }
            
            openFile("Lab2.exe");
            Spliter();
            updateScreen();

            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                {
                    if(currentLine != 0)
                        currentLine--;
                    updateScreen();
                }
                if (key == ConsoleKey.DownArrow)
                {
                    if(currentLine != lines.Count)
                        currentLine++;
                    updateScreen();
                }
                if (key == ConsoleKey.Q)
                {
                    symbolInLine = 8;
                    currentLine = 0;
                    Spliter();
                    updateScreen();
                }

                if (key == ConsoleKey.W)
                {
                    symbolInLine = 16;
                    currentLine = 0;
                    Spliter();
                    updateScreen();
                }

                if (key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    }
}