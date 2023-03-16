using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            //    Console.WriteLine("Сейчас вы будете вводить данные матрицы коэффициентов");
            //    MatrixOfCoefficients = InitMatrix(MatrixOfCoefficients);
            //    Result = ResultMatrix(MatrixOfCoefficients, VectorM, VectorN);
            //    Console.WriteLine();
            //    Console.WriteLine("Значение целевой функции - {0}", ValueObjectFunc(MatrixOfCoefficients, Result));
            //}
            //else
            //{
            //    Console.WriteLine("Тождество не применимо для двух векторов. Программа даёт сбой");
            //}

            double[,] MainMatrix = new double[4, 7] { { 1, 1, -1, -1, 0, 0, 8 },
                                                    { 1, -1, 2, 0, -1, 0, 2 },
                                                    { -2, -8, 3, 0, 0, -1, 1},
                                                    { -2, -1, 2, 0, 0, 0, 0} };


            //{
            //                                        { 1, 2, 1, 0, 5 },
            //                                        { 1, 1, 0, 1, 4 },
            //                                        { 2, 4, 0, 0, 0}
            //};

            //Console.WriteLine("Введите матрицу");
            //MainMatrix = InitMatrixOfSimplex(MainMatrix);
            Console.WriteLine();
            MainMatrix = ResultMatrix(MainMatrix);
            Console.WriteLine("Ваш результат");
            ShowMatrixSimplex(MainMatrix);
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

        /// <summary>
        /// find max element the bootom
        /// </summary>
        /// <param name="Matrix">the matrix</param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
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
                if (Matrix[column, i] != 0 && ((Matrix[column, i] > 0 && Matrix[i, columns-1] > 0) || ((Matrix[column, i] < 0 && Matrix[i, columns - 1] < 0)))   )
                { //Whem number divide equals positive number
                    Divided[i] = Matrix[i, columns - 1] / Matrix[i, column];
                }
                else { Divided[i] = 99; }//Needs to skip the 0 / Number 
            }
            int Row = Array.IndexOf(Divided, Divided.Min());
            return Row;
        }


        private static double[,] DivideOnFoundedEl(double[,] Matrix, int rows, int columns, int row,  int column)
        { 
            double temp = Matrix[row, column] /*/ Matrix[row, column]*/; // Needed to divide
            for (int i = row; i <= row; i++)
            {//Cycle which divide all row on element
                for (int j = 0; j < columns; j++)
                {
                    Matrix[i, j] = Matrix[i, j] / temp;
                }
            }
            return Matrix;
        }




        private static void DivideAllColumns(double[,] Matrix, int rows, int columns, int row, int column)
        {
            for (int i = 0; i < rows; i++)
            {
                if (i == row) { continue; }
                int temp = 0;
                //Need some method that shows we need to *1 or -1
                if (Matrix[i, column] >= 0) { temp = -1; }
                //else if (Matrix[i, column] <= 0 && Matrix[rows, column] <= 0) { temp = -1; }
                else { temp = 1; }
                double Numb = Matrix[i, column];
                for (int j = 0; j < columns; j++)
                {
                    Matrix[i,j]  = Matrix[row, j] * temp  * Numb + Matrix[i, j]; //problem
                }
            }
        }



        private static void TheBoneMethod(double[,] Matrix, int rows, int columns, double max)
        {
            int column = FindColumn(Matrix, rows, columns, max);
            int row = FindRow(Matrix, rows, columns, column);
            DivideOnFoundedEl(Matrix, rows, columns, row, column);
            DivideAllColumns(Matrix, rows, columns, row, column);
        }


        private static double[,] ResultMatrix(double[,] MainMatrix)
        {
            int rows = MainMatrix.GetUpperBound(0) + 1;
            int columns = MainMatrix.Length / rows;
            double max;
            do
            {
                max = FindMaxOnTheBottom(MainMatrix, rows, columns);
                if (max > 0)
                {TheBoneMethod(MainMatrix, rows, columns, max); }
            } while (max > 0);

            return MainMatrix;
        }
    }
}
