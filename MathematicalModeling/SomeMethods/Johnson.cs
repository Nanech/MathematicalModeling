using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicalModeling.SomeMethods
{
    internal class Johnson
    {

        public static void StartThisMethod()
        {
            string path = @"Files\jhonson-N2.csv";

            FileInfo fileInfo = new FileInfo(path);

            Console.WriteLine();
            string[] readFile = File.ReadAllLines(path);

            List<int> first = new List<int>();
            List<int> second = new List<int>();

             for (int i = 0; i < readFile.Length; i++)
            {
                string[] line = readFile[i].Split(';');
                first.Add(Convert.ToInt32(line[0]));
                second.Add(Convert.ToInt32(line[1]));
                Console.WriteLine("{0} {1}", first[i], second[i] );
            }

            //Делаю верх и низ
            List<int> topRight = new List<int>();
            List<int> topLeft = new List<int>();
            List<int> bottomRight = new List<int>();
            List<int> bottomLeft = new List<int>();

            int length = first.Count;
            for (int i = 0; i < length; i++)
            {
                //Нахожу минимальные
                int minOfFirst = first.Min(); //Левый
                int minOfSecond = second.Min(); //Правый

                if (minOfFirst < minOfSecond)
                {
                    //Если левый элемент меньше правого
                    topLeft.Add(minOfFirst);
                    int index = first.IndexOf(minOfFirst);
                    Console.WriteLine(index);
                    topRight.Add(second[index]);
                    first.RemoveAt(index);
                    second.RemoveAt(index);

                    //topRight.Add(second[second.IndexOf(first.Min())]);
                }
                else
                {
                    //Если правый меньше левого
                    bottomRight.Add(minOfSecond);
                    int index = second.IndexOf(minOfSecond);
                    bottomLeft.Add(first[index]);

                    first.RemoveAt(index);
                    second.RemoveAt(index);
                }
            }

            Console.WriteLine("\n");
            bottomLeft.Reverse();
            bottomRight.Reverse();
            topLeft.AddRange(bottomLeft);
            topRight.AddRange(bottomRight);

            List<int> was = new List<int>();
            List<int> optimal = new List<int>();

            for (int i = 0; i < readFile.Length; i++)
            {
                string[] line = readFile[i].Split(';');
                first.Add(Convert.ToInt32(line[0]));
                second.Add(Convert.ToInt32(line[1]));
                Console.WriteLine("{0} {1}", first[i], second[i]);
            }

            int fstcounter = 0, sndcounter = 0;
            for (int i = 0; i < length-1; i++)
            {
                fstcounter++;
                int result = 0, scndres = 0;
                for (int j = 0; j < fstcounter; j++)
                {
                    result += topLeft[j];
                    scndres += first[j];       
                    for (int z = 0; z < fstcounter - 1; z++)
                    {
                        result = result - topRight[z];
                        scndres = scndres - second[z];
                    }
                }
                was.Add(scndres);
                optimal.Add(result);
            }


            for (int i = 0; i < length; i++)
            {
                Console.WriteLine("{0} {1}", topLeft[i], topRight[i]);
            }
            Console.WriteLine("раньше {0}, потом {1}", was.Max(), optimal.Max());
        }


    }
}
