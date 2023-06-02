using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicalModeling
{
    internal class Models
    {
        public static void HunterXVictim()
        {
            double startValueVictim = 100;
            double startValueHunter = 20;
            double riseNature = 0.01;
            double deathCoefConcurent = 0.00001;
            double deathCoefHunter = 0.001;
            double deathCoef = 0.1;
            double riseCoef = 0.0001;


            for (int i = 0; i < 32; i++)
            {
                Console.WriteLine("{0} {1} {2}", i, Math.Round( startValueVictim), Math.Round( startValueHunter ));

                double newValueVitim = startValueVictim + (riseNature - deathCoefConcurent
                    * startValueVictim) * startValueVictim - deathCoefHunter * startValueVictim * startValueHunter;
          



                double newValueHunter = startValueHunter - deathCoef * startValueHunter
                    + riseCoef * startValueVictim * startValueHunter;

                startValueHunter = newValueHunter;
                startValueVictim = newValueVitim;

            }


        }


        public static void MonteCarlo()
        {
            int n = 100000, k = 0;
            double x, y, S;

            Random rnd = new Random();

            for (int i = 0; i < n; i++)
            {
                x =  rnd.NextDouble() * 2;
                y =  rnd.NextDouble() * 2;
                if ( Math.Pow(x-1, 2) + Math.Pow(y-1,2) <= 1)
                {
                    k++;
                }
            }
            S = (4 * (double)k) / (double)n;

            Console.WriteLine("Результат pi в ходе решении программы {0}", S); 
            Console.WriteLine("Точное математическое pi {0}", Math.PI); 

        }

        public static void MonteCarloEtap4()
        {
            int n = 1000 *  10000, k = 0;
            double x, y, S, a = 5, b = 8.5;

            Random rnd = new Random();

            for (int i = 0; i < n; i++)
            {
                x = rnd.NextDouble() * (b);
                y = rnd.NextDouble() * (a);
                if ( (x/3<y) && (y<x*(10-x)/5) )
                {
                    k++;
                }
            }
            S = (a* b * (double)k)/ (double)n;

            Console.WriteLine("Результат площади фигуры {0}", Math.Round(S, 2) );

        }

        public static void MonteCarloEtap5()
        {
            int n = 1000 * 10000, k = 0;
            double x, y, S, a = 5, b = 8.5;

            Random rnd = new Random();

            for (int i = 0; i < n; i++)
            {
                x = rnd.NextDouble() * (b);
                y = rnd.NextDouble() * (a);
                if ((x / 3 < y) && (y < x * (10 - x) / 5))
                {
                    k++;
                }
            }
            S = (a * b * (double)k) / (double)n;

            Console.WriteLine("Результат площади фигуры {0}", Math.Round(S, 1) );
        }


  
        public static void zad1()
        {
            // >7
            int n = 1000 * 10000, k = 0;
            double x, y, S, a = 1, b = 20;

            Random rnd = new Random();

            for (int i = 0; i < n; i++)
            {
                x = rnd.NextDouble() * (b) - 5;
                y = rnd.NextDouble() * (a);
                if ( Math.Sin(x) > y  && y>0  )
                {
                    k++;
                }
            }
            S =  (b * a * (double)k) / (double)n;

            Console.WriteLine("Результат площади фигуры 1 {0}", S);
        }

        public static void zad2()
        {
            // >28.6

            int n = 1000 * 10000, k = 0;
            double x, y, S, a = 8, b = 7; // x - b, a - y

            Random rnd = new Random();

            for (int i = 0; i < n; i++)
            {
                x = rnd.NextDouble() * (b);
                y = rnd.NextDouble() * (a);
                if ( y >= x/2 && y <= (x*(8-x))/2 )
                {
                    k++;
                }
            }
            S = (b * a * (double)k) / (double)n;

            Console.WriteLine("Результат площади фигуры 2 {0}", S);
        }

        public static void zad3()
        {
            // >48

            int n = 1000 * 10000, k = 0;
            double x, y, S, a = 6, b = 12; // x - b, a - y

            Random rnd = new Random();

            for (int i = 0; i < n; i++)
            {
                x = rnd.NextDouble() * (b);
                y = rnd.NextDouble() * (a);
                if ( y<6 && y>Math.Pow(x-6,2)/6)
                {
                    k++;
                }
            }
            S = (b * a * (double)k) / (double)n;

            Console.WriteLine("Результат площади фигуры 3 {0}", S);
        }


        public static void zad4()
        {
            // >19 - 20

            int n = 1000 * 10000, k = 0;
            double x, y, S, a = 4, b = 10; // x - b, a - y

            Random rnd = new Random();

            for (int i = 0; i < n; i++)
            {
                x = rnd.NextDouble() * (b);
                y = rnd.NextDouble() * (a);
                if ( y > x/5 && y<x*(12-x)/9 )
                {
                    k++;
                }
            }
            S = (b * a * (double)k) / (double)n;

            Console.WriteLine("Результат площади фигуры 4 {0}", S);
        }

        public static void zad5()
        {
            // >17.5, 18

            int n = 1000 * 10000, k = 0;
            double x, y, S, a = 4, b = 8; // x - b, a - y

            Random rnd = new Random();

            for (int i = 0; i < n; i++)
            {
                x = rnd.NextDouble() * (b);
                y = rnd.NextDouble() * (a);
                if ( y > (8-x)/8 &&  y < x*(8-x)/4  )
                {
                    k++;
                }
            }
            S = (b * a * (double)k) / (double)n;

            Console.WriteLine("Результат площади фигуры 5 {0}", S);
        }


        public static void zad6()
        {
            // 1-1.5

            int n = 1000 * 10000, k = 0;
            double x, y, S, a = 2, b = 3; // x - b, a - y

            Random rnd = new Random();

            for (int i = 0; i < n; i++)
            {
                x = rnd.NextDouble() * (b);
                y = rnd.NextDouble() * (a);
                if ( y < Math.Sin(x) && y > Math.Pow(x-2,2)/2 )
                {
                    k++;
                }
            }
            S = (b * a * (double)k) / (double)n;

            Console.WriteLine("Результат площади фигуры 6 {0}", S);
        }

    }
}
