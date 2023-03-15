using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicalModeling.SomeMethods
{
    internal class SimplexMethod:GeneralClass
    {
        public static void StartSimplex()
        {
            //int[] VectorM = new int[3];
            //int[] VectorN = new int[3];
            //int[,] MatrixOfCoefficients = new int[3, 3];
            //int[,] Result = new int[3, 3];
            //Console.WriteLine("Сейчас вы будете вводить данные для вектора М, или же вектора мощности поставщиков");
            //VectorM = InitVector(VectorM);
            //Console.WriteLine("Сейчас вы будете вводить данные для вектора N, или же вектора спроса потребителей");
            //VectorN = InitVector(VectorN);
            //if (CheckSum(VectorN, VectorM))
            //{
            //    Console.WriteLine("Сейчас вы будете вводить данные матрицы коэфицентов");
            //    MatrixOfCoefficients = InitMatrix(MatrixOfCoefficients);
            //    Result = ResultMatrix(MatrixOfCoefficients, VectorM, VectorN);
            //    Console.WriteLine();
            //    Console.WriteLine("Значение целевой функции - {0}", ValueObjectFunc(MatrixOfCoefficients, Result));
            //}
            //else
            //{
            //    Console.WriteLine("Тождество не пременимо для двух векторов. Программа даёт сбой");
            //}

            double[,] MainMatrix = new double[3, 5];
            Console.WriteLine("Введите матрицу");
            MainMatrix = InitMatrixOfSimplex(MainMatrix);
            MainMatrix = ResultMatrix(MainMatrix);


        }

        protected static double[,] InitMatrixOfSimplex(double[,] SomeMatrix)
        {
            int rows = SomeMatrix.GetUpperBound(0) + 1;
            int columns = SomeMatrix.Length / rows;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write("Введите цифру под индексом {0} {1} - ", i, j);
                    SomeMatrix[i, j] = Convert.ToDouble(Console.ReadLine());
                }
            }
            Console.WriteLine("Вы ввели следующую матрицу");
            ShowMatrixSimplex(SomeMatrix);
            return SomeMatrix;
        }

        protected static void ShowMatrixSimplex(double[,] matrix)
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

        private static double FindMaxOnTheBottom(double[,] Matrix, int rows, int columns)
        {
            double max = 0;
            for (int i = rows-1; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (max < Matrix[i, j]) { max = Matrix[i, j];}
                }
            }
            return max;
        }

        private static int FindColumn(double[,] Matrix, int rows, int columns, double max)
        {
            int findColumn = 0;
            for (int i = rows - 1; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (max == Matrix[i, j]) {  findColumn = j; break;  }
                }
            }
            return findColumn;
        }

        private static int FindRow(double[,] Matrix, int rows, int columns, int column)
        {
            double[] Divided = new double[rows];
            for (int i = 0; i < rows; i++)
            {
                Divided[i] = Matrix[i, columns - 1] / Matrix[column, i];
            }
            int Row = Array.IndexOf(Divided, Divided.Min());
            return Row;
        }


        private static double[,] DivideOnFoundedEl(double[,] Matrix, int rows, int columns, int row,  int column)
        { // here something wrong
            for (int i = column; i <= column; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Matrix[i, j] = Matrix[i, j] / Matrix[column, row ];
                }
            }
            return Matrix;
        }



        private static double[,] ResultMatrix(double[,] MainMatrix)
        {
            //find max element the bootom
            int rows = MainMatrix.GetUpperBound(0) + 1;
            int columns = MainMatrix.Length / rows;
            
            //for (int i = 0; i < rows; i++)
            //{
            //    for (int j = 0; j < columns; j++)
            //    {
                   

            //    }
            //}

            double max = FindMaxOnTheBottom(MainMatrix, rows, columns);

            int column = FindColumn(MainMatrix, rows, columns, max);
            int row = FindRow(MainMatrix, rows, columns, column);
            DivideOnFoundedEl(MainMatrix, rows, columns, row, column);

            //if (max >= 0)
            //{
            //    int column = FindColumn(MainMatrix, rows, columns, max);
            //    int row = FindRow(MainMatrix, rows, columns, column);
            //    DivideOnFoundedEl(MainMatrix, rows, columns, row, column);


            //}



            return MainMatrix;
        }



    }
}
