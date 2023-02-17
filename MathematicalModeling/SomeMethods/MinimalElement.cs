using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicalModeling.SomeMethods
{
    internal class MinimalElement:GeneralClass
    {
        public static void StartMinimElem()
        {
            int[] VectorM = new int[3];
            int[] VectorN = new int[3];
            int[,] MatrixOfCoefficients = new int[3, 3];
            int[,] Result = new int[3, 3];
            Console.WriteLine("Сейчас вы будете вводить данные для вектора М, или же вектора мощности поставщиков");
            VectorM = InitVector(VectorM);
            Console.WriteLine("Сейчас вы будете вводить данные для вектора N, или же вектора спроса потребителей");
            VectorN = InitVector(VectorN);
            if (CheckSum(VectorN, VectorM))
            {
                Console.WriteLine("Сейчас вы будете вводить данные матрицы коэфицентов");
                MatrixOfCoefficients = InitMatrix(MatrixOfCoefficients);
                Result = MinimalResult(MatrixOfCoefficients, VectorM, VectorN);
                Console.WriteLine();
                //Console.WriteLine("Значение целевой функции - {0}", ValueObjectFunc(MatrixOfCoefficients, Result));
            }
            else
            {
                Console.WriteLine("Тождество не пременимо для двух векторов. Программа даёт сбой");
            }
        }



        private static int[,] MinimalResult(int[,] MatCoef, int[] VeM, int[] VeN)
        {
            int rows = MatCoef.GetUpperBound(0) + 1;
            int columns = MatCoef.Length / rows;
            int[,] MinRes = new int[rows, columns];
            int countMinEl = 0; int min = 10;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (MatCoef[i, j]  <= min ) { countMinEl++; min = MatCoef[i,j]; }
                }
            }
            Console.WriteLine($" {min}  and {countMinEl}");



            return MinRes;
        }

    }
}
