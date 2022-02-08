using System;
using System.IO;
using System.Security.Policy;
using System.Threading;
using Microsoft.Win32;

namespace Va_Tor
{
    class FishShark
    {
        private int age;
        private int golod;
        private int reproduction;
        private bool hod;

        private bool isAlive;

        private char kind;

        private int maxAge;
        private int maxGolod;
        private int maxReproduction;


        public FishShark( //конструктор, если точку нужно заполнить
            int _age,
            int _golod,
            int _reproduction,
            bool _hod,
            char _kind,
            int _maxAge,
            int _maxGolod,
            int _maxReproduction
        )
        {
            this.age = _age;
            this.golod = _golod;
            this.reproduction = _reproduction;
            this.hod = _hod;
            this.kind = _kind;
            this.isAlive = true;
            this.maxAge = _maxAge;
            this.maxGolod = _maxGolod;
            this.maxReproduction = _maxReproduction;
        }

        public FishShark() //конструктор, если точка пустая
        {
            this.isAlive = false;
            this.kind = '-';
            this.age = 0;
            this.golod = 0;
            this.hod = false;
            this.reproduction = 0;
        }
        public bool getHod()
        {
            return this.hod;
        }
        public void postHod()
        {
            this.hod = true;
            this.age++;
            this.reproduction++;
            this.golod++;
            if (this.age >= this.maxAge)
            {
                this.kind = '-';
                this.isAlive = false;
                this.age = 0;
                this.golod = 0;
                this.hod = false;
                this.reproduction = 0;
            }
            if (this.kind == 'S')
            {
                if (this.golod >= this.maxGolod)
                {
                    this.kind = '-';
                    this.isAlive = false;
                }
            }
        }
        public int getReproduction()
        {
            return this.reproduction;
        }
        public void changeReproduction(int _new)
        {
            this.reproduction = _new;
        }
        public bool getIsAlive()
        {
            return this.isAlive;
        }
        public char getKind()
        {
            return this.kind;
        }
        public void changeHod(bool _hod)
        {
            this.hod = _hod;
        }
    }

    internal class Program
    {
        static Random rnd = new Random();

        public static int random(int a, int b)
        {
            return rnd.Next(a, b);
        }

        public static int numFish = 0;
        public static int numShark = 0;

        public static int h = 1000;

        private static int NUM_ROWS = 500;
        private static int NUM_COLUMS = 500;

        //константы начало

        private static int ageShark = 127;
        private static int repShark = 35;
        private static int golodShark = 20;

        private static int ageFish = 50;
        private static int repFish = 6;

        //константы конец

        static FishShark[,] pole = new FishShark[NUM_ROWS, NUM_COLUMS];

        static int[,] XY = new int[4, 2];
        private static int points = 0;

        public static void Main(string[] args)
        {
            createPole();

            int time = 0;

            string fileName = "output.txt";

            using (StreamWriter sw = new StreamWriter(fileName, false, System.Text.Encoding.Default))
            {
                sw.WriteLine();
            }

            while (time < h)
            {
                time++;

                for (int i = 0; i < NUM_COLUMS; i++)
                {
                    for (int j = 0; j < NUM_ROWS; j++)
                    {
                        if (pole[i, j].getIsAlive())
                        {
                            if (pole[i, j].getKind() == 'F')
                            {
                                if (numFish > 0)
                                {
                                    if (!pole[i, j].getHod())
                                    {
                                        fishHod(i, j);
                                    }
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < NUM_COLUMS; i++)
                {
                    for (int j = 0; j < NUM_ROWS; j++)
                    {
                        if (pole[i, j].getIsAlive())
                        {
                            if (pole[i, j].getKind() == 'S')
                            {
                                if (numShark > 0)
                                {
                                    if (!pole[i, j].getHod())
                                    {
                                        sharkHod(i, j);
                                    }
                                }
                            }
                        }
                    }
                }
                Console.Clear();
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, 0);
                
                for (int i = 0; i < NUM_COLUMS; i++)
                {
                    for (int j = 0; j < NUM_ROWS; j++)
                    {
                        if (pole[i, j].getIsAlive())
                        {
                            pole[i, j].changeHod(false);
                        }
                    }
                }
                
                
                string text = time + " " + numFish + " " + numShark;

                using (StreamWriter sw = new StreamWriter(fileName, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }
            }
        }

        public static void fishHod(int i, int j)
        {
            int targetI, targetJ;

            pole[i, j].postHod();

            if (pole[i, j].getIsAlive())
            {
                //DLC "a stranger among their own" (чужой среди своих)
                points = 0;
                findPoints(i, j, 'F');

                if (points == 4)
                {
                    int chance = random(0, 100);
                    if (chance < 20)
                    {
                        pole[i, j] = new FishShark(0, 0, 0, true, 'S', ageShark, golodShark, repShark);
                        numFish--;
                        numShark++;
                    }
                }
                else
                {
                    points = 0;
                    findPoints(i, j, '-');

                    if (points > 0)
                    {
                        int napravlenie = random(0, points);
                        
                        targetI = XY[napravlenie, 0];
                        targetJ = XY[napravlenie, 1];

                        (pole[i, j], pole[targetI, targetJ]) = (pole[targetI, targetJ], pole[i, j]);

                        if (pole[targetI, targetJ].getReproduction() >= repFish)
                        {
                            pole[i, j] = new FishShark(0, 0, 0, true, 'F', ageFish, 0, repFish);
                            pole[targetI, targetJ].changeReproduction(1);
                            numFish++;
                        }
                    }
                }
                
            }
            else
            {
                numFish--;
            }
        }
        
        public static void sharkHod(int i, int j)
        {
            int targetI, targetJ;

            pole[i, j].postHod();

            if (pole[i, j].getIsAlive())
            {
                points = 0;

                if (numFish > 0)
                {
                    findPoints(i, j, 'F');
                }
                if (points > 0)
                {
                    int napravlenie = random(0, points);

                    targetI = XY[napravlenie, 0];
                    targetJ = XY[napravlenie, 1];

                    pole[targetI, targetJ] = pole[i, j];
                    pole[i, j] = new FishShark();
                    numFish--;
                    
                    if (pole[targetI, targetJ].getReproduction() >= repShark)
                    {
                        pole[i, j] = new FishShark(0, 0, 0, true, 'S', ageShark, golodShark, repShark);
                        pole[targetI, targetJ].changeReproduction(1);
                        numShark++;
                    }
                }
                else
                {
                    points = 0;
                    findPoints(i, j, '-');

                    if (points > 0)
                    {
                        int napravlenie = random(0, points);

                        targetI = XY[napravlenie, 0];
                        targetJ = XY[napravlenie, 1];

                        (pole[i, j], pole[targetI, targetJ]) = (pole[targetI, targetJ], pole[i, j]);

                        if (pole[targetI, targetJ].getReproduction() >= repShark)
                        {
                            pole[i, j] = new FishShark(0, 0, 0, true, 'S', ageShark, golodShark, repShark);
                            pole[targetI, targetJ].changeReproduction(1);
                            numShark++;
                        }
                    }
                }
            }
            else
            {
                numShark--;
            }
        }

        public static void createPole()
        {
            for (int i = 0; i < NUM_COLUMS; i++)
            {
                for (int j = 0; j < NUM_ROWS; j++)
                {
                    var chance = random(0, 100);

                    if (chance < 40) //Рыба
                    {
                        pole[i, j] = new FishShark(
                            random(0, ageFish),
                            0,
                            random(0, repFish),
                            false,
                            'F',
                            ageFish,
                            0,
                            repFish
                            );
                        numFish++;
                    }
                    else if (chance > 95) //акула
                    {
                        pole[i, j] = new FishShark(
                            random(0, ageShark),
                            random(0, golodShark),
                            random(0, repShark),
                            false,
                            'S',
                            ageShark,
                            golodShark,
                            repShark
                            );
                        numShark++;
                    }
                    else
                    {
                        pole[i, j] = new FishShark();
                    }
                }
            }
        }

        public static void findPoints(int i, int j, char point)
        {
            int newI, newJ;

            newI = (i == 0) ? NUM_COLUMS - 1 : i - 1;
            if (pole[newI, j].getKind() == point)
            {
                points++;
                XY[points - 1, 0] = newI;
                XY[points - 1, 1] = j;
            }

            newI = (i == NUM_COLUMS - 1) ? 0 : i + 1;
            if (pole[newI, j].getKind() == point)
            {
                points++;
                XY[points - 1, 0] = newI;
                XY[points - 1, 1] = j;
            }

            newJ = (j == 0) ? NUM_ROWS - 1 : j - 1;
            if (pole[i, newJ].getKind() == point)
            {
                points++;
                XY[points - 1, 0] = i;
                XY[points - 1, 1] = newJ;
            }

            newJ = (j == NUM_ROWS - 1) ? 0 : j + 1;
            if (pole[i, newJ].getKind() == point)
            {
                points++;
                XY[points - 1, 0] = i;
                XY[points - 1, 1] = newJ;
            }
        }
    }
}