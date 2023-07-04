using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicalModeling.SomeMethods
{
    internal class PruferCode
    {
        public static void Decoding()
        {
            string path = @"Files\FirstCode.csv";

            FileInfo fileInfo = new FileInfo(path);

            Console.WriteLine();
            string[] readFile = File.ReadAllLines(path);

            //Точка входа и выхода 
            List<int> pointOfEntry = new List<int>();
            List<int> pointOfExit = new List<int>();
            List<int> otvet = new List<int>();

            for (int i = 0; i < readFile.Length; i++)
            {
                string[] line = readFile[i].Split(';');
                pointOfEntry.Add(Convert.ToInt32(line[0]));
                pointOfExit.Add(Convert.ToInt32(line[1]));
            }

            Console.WriteLine("entry points: ");
            foreach(int entry in pointOfEntry)
            {
                Console.Write(entry + " ");
            }

            Console.WriteLine("\nexit points: ");
            foreach (int exit in pointOfExit)
            {
                Console.Write(exit + " ");
            }

            while (pointOfEntry.Count() != 1)
            {
                //Находим то чего нету
                var result = pointOfExit.Except(pointOfEntry);
                //Находим индекс минимального элемента из точки выхода, которой не существует
                int index = pointOfExit.IndexOf(result.Min());
                //Добавляем в ответ
                otvet.Add(pointOfEntry[index]);
                //Удаляем эти элементы
                pointOfEntry.RemoveAt(index);
                pointOfExit.RemoveAt(index);
            }

            Console.WriteLine("\n");
            string fileWithAnswer = @"Files/otevet.txt";
            using (StreamWriter w = new StreamWriter(fileWithAnswer))
            {
                otvet.ForEach(x => w.Write(x + " "));
            }
        }

        public static void Coding()
        {
            string path = @"C:\Users\агеевнс\Source\Repos\MathematicalModeling\MathematicalModeling\Files\DecodedCodeOfPruffer.txt";
            FileInfo fileInfo = new FileInfo(path);
            string[] readFile = File.ReadAllLines(path);
            
            List<int> decoded = new List<int>();
            string[] values = readFile[0].Trim().Split(' ');
            foreach (string str in values) { decoded.Add(Convert.ToInt32(str));}
            decoded.ForEach(x => Console.Write(x + " " ));

            List<int> ints = new List<int>();
            for (int i = 0; i < decoded.Count()+2; i++) { ints.Add(i+1); }

            Console.WriteLine();
            ints.ForEach(x => Console.Write(x + " "));

            List <string> otvet = new List<string >();

            while(ints.Count() != 2)
            {
                var result = ints.Except(decoded);

                int minimal = result.Min();

                otvet.Add(decoded[0].ToString() + " " + minimal.ToString() + ",");

                decoded.RemoveAt(0);
                ints.RemoveAt(ints.IndexOf(minimal));
            }

            otvet.Add(ints[0] + " " + ints[1]);

            Console.WriteLine("\n");
            otvet.ForEach(x => Console.Write(x + " "));

        }

    }
}
