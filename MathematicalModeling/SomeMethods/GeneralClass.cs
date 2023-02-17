using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicalModeling.SomeMethods
{
    internal class GeneralClass
    {

        protected static void ShowMatrix(int[,]matrix)
        {
            int rows = matrix.GetUpperBound(0) + 1;
            int columns = matrix.Length / rows;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write("{0} ", matrix[i, j]);
                }
                Console.WriteLine();
            }            
        }

        protected static void ShowArray(int[] somearray)
        {
            for (int i = 0; i < somearray.Length; i++)
            {
                Console.Write("{0} ", somearray[i]);
            }
            Console.WriteLine();
        }


        protected static int[] InitVector(int[] SomeVector)
        {
            for (int i = 0; i < SomeVector.Length; i++)
            {
                Console.WriteLine("Введите цифру вашего вектора, под индексом  {0}", i);
                SomeVector[i] = Convert.ToInt32(Console.ReadLine());
            }
            Console.Write($"Вы ввели следующий вектор: ");
            ShowArray(SomeVector);
            return SomeVector;
        }

        protected static int[,] InitMatrix(int[,] SomeMatrix)
        {
            int rows = SomeMatrix.GetUpperBound(0) + 1;
            int columns = SomeMatrix.Length / rows;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write("Введите цифру под индексом {0} {1} - ", i, j);
                    SomeMatrix[i, j] = Convert.ToInt32(Console.ReadLine());
                }
            }
            Console.WriteLine("Вы ввели следующую матрицу");
            ShowMatrix(SomeMatrix);
            return SomeMatrix;
        }


    }
}
