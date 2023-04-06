using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MathematicalModeling.SomeMethods
{
    internal class TravelingSalesmanProblem:GeneralClass
    {
        public static void startTSP()
        {
            int[,] MainMatrix = new int[5, 5];
            Console.WriteLine();

            Console.WriteLine("Ваш результат");

            InitTheArray(MainMatrix);
            GeneralClass.ShowMatrix(MainMatrix);

            DoTheMethod(MainMatrix);
        }


        static private int[,] InitTheArray(int[,] MainMatrix)
        {
            int rows = MainMatrix.GetUpperBound(0) + 1;
            int columns = MainMatrix.Length / rows;
            //The CHAT GPT-4 code
            Random random = new Random(); // Create a random number generator
            // Iterate through the rows of the 2D array
            for (int i = 0; i < rows; i++)
            {
                // Create a HashSet to store used numbers in the current row
                // This ensures no duplicate numbers in the same row
                HashSet<int> usedNumbers = new HashSet<int>();
                // Iterate through the columns of the 2D array
                for (int j = 0; j < columns; j++)
                {
                    // If i equals j, set the element to 0
                    if (i == j)
                    { MainMatrix[i, j] = 0;}
                    else
                    {
                        int randomNumber;
                        // Generate a random number that hasn't been used in the current row
                        do
                        {
                            randomNumber = random.Next(1, columns + 1);
                        } while (usedNumbers.Contains(randomNumber));

                        // Add the random number to the usedNumbers HashSet and set the array element
                        usedNumbers.Add(randomNumber);
                        MainMatrix[i, j] = randomNumber;
                    }
                }
            }
            return MainMatrix;
        }

        static private List<int> Repeater(int[,] matrix, List<int> row, int[] values,  int start, int currentI, int column )
        {
            if (column == row.Count) { return row; }

             
            int[] a = Enumerable.Range(0, matrix.GetLength(1)).Select(x => matrix[currentI, x]).ToArray();//Достали интересуемую нас строку из matrix

            int min = 100;
            for (int i = 0; i < column-1; i++)
            {
                if (min > a[i] && a[i] != 0) { min = a[i]; }
            }
            int index = Array.IndexOf(a, min); // Нашли индекс минимального элемента

            //нужно ещё убедиться что рассматриваемый элемент не был уже записан

            for (int i = 0; i < column-1; i++)
            {
                values[0] = start;
                if (values[i] == 0) { values[i] = index;}
            }
            
            currentI = index; //переходим на тот элемент который имеет наименьшее значение

            return Repeater(matrix, row, values,  start, currentI, column);

        }


        static private void DoTheMethod(int[,] MainMatrix)
        {
            int rows = MainMatrix.GetLength(0);
            int columns = MainMatrix.GetLength(1);

            int[,] listOfValues = new int[rows, columns]; //Хранить пути

            for (int i = 0; i < rows; i++)
            {
                List<int> row = new List<int>();
                int[] array = new int[rows];
                Repeater(MainMatrix, row,  array, i, i, columns);

            }



            //do
            //{
            //    List<int> row = new List<int>();
            //    int i = 0;
            //    do
            //    {

            //        int[] a = Enumerable.Range(0, MainMatrix.GetLength(1)).Select(x => MainMatrix[i, x]).ToArray();
            //        int min = a[0];
            //        for (int z = 0; z < a.Length; z++)
            //        {
            //            if (a[z] != 0 && min > a[z]) { min = a[z]; }
            //        }
            //        int someJ = Array.IndexOf(a, min);
            //        list[count].Add(someJ);
            //        count++;

            //    } while (count < columns);
            //}
            //while (count < columns);



        }


    }
}
