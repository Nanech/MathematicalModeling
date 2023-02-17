using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MathematicalModeling.SomeMethods
{
    internal class NorthWestMethod : GeneralClass
    {

        public static void StartNorthWest()
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
                Result = ResultMatrix(MatrixOfCoefficients, VectorM, VectorN);
                Console.WriteLine();
                Console.WriteLine("Значение целевой функции - {0}", ValueObjectFunc(MatrixOfCoefficients, Result));
            }
            else
            {
                Console.WriteLine("Тождество не пременимо для двух векторов. Программа даёт сбой");
            }
        }

        private static int[,] ResultMatrix(int[,] MatrixCoff, int[] VecM, int[] VecN)
        {
            Console.WriteLine();    
            int rows = MatrixCoff.GetUpperBound(0) + 1;
            int columns = MatrixCoff.Length / rows;
            int[,] ResultMatrix = new int[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        if (VecM[i] < VecN[j])
                        {
                            ResultMatrix[i, j] = VecM[i];
                            int count = 1;
                            while (count != columns)
                            {
                                ResultMatrix[i, count] = -1;
                                count++;
                            }
                        }
                        else
                        {
                            ResultMatrix[i, j] = VecN[j];
                            int count = 1;
                            while (count != rows) 
                            {
                                ResultMatrix[count, j] = -1;
                                count++;
                            }
                        }
                    }
                    else if (ResultMatrix[i, j] == -1) { continue; }
                    else if (VecM[i] < VecN[j])
                    {
                        int sumOfColumn = 0;
                        int count = i;
                        while (count != -1)
                        {
                            if (ResultMatrix[count, j] == -1) { sumOfColumn++; }
                            sumOfColumn += ResultMatrix[count, j];
                            count--;
                        }
                        if (sumOfColumn >0)
                        {
                            sumOfColumn = VecN[j] - sumOfColumn;
                            ResultMatrix[i,j] = sumOfColumn;
                        }
                        else
                        {
                            int sumOfRows = 0;
                            count = j;
                            while (count != -1)
                            {
                                if (ResultMatrix[i, count] == -1) { sumOfRows++; }
                                sumOfRows += ResultMatrix[i, count];
                                count--;
                            }
                            ResultMatrix[i, j] = VecM[i] - sumOfRows;
                        }   
                    }
                    else if (VecN[j] < VecM[i]) 
                    {
                        int sumOfRows = 0;
                        int count = j;
                        while (count != -1)
                        {
                            if (ResultMatrix[i, count] == -1) { sumOfRows++; }
                            sumOfRows += ResultMatrix[i, count];
                            count--;
                        }
                        if (sumOfRows > 0)
                        {
                            sumOfRows = VecM[i] - sumOfRows;
                            ResultMatrix[i, j] = sumOfRows;
                        }
                        else
                        {
                            int sumOfColumns = 0;
                            count = i;
                            while (count != -1)
                            {
                                if (ResultMatrix[count, j] == -1) { sumOfColumns++; }
                                sumOfColumns += ResultMatrix[count, j];
                                count--;
                            }
                            ResultMatrix[i, j] = VecN[j] - sumOfColumns;
                        }
                    }
                    else
                    {
                        int sumOfColumns = 0, sumOfRows = 0, countI = i, countJ = j;
                        while (countJ != -1)
                        {
                            if (ResultMatrix[i, countJ] == -1) { sumOfRows++; }
                            sumOfRows += ResultMatrix[i, countJ];
                            countJ--;
                        }
                        while (countI != -1)
                        {
                            if (ResultMatrix[countI, j] == -1) { sumOfColumns++; }
                            sumOfColumns += ResultMatrix[countI, j];
                            countI--;
                        }
                        if (VecM[i] - sumOfRows < VecN[j] - sumOfColumns) 
                        {
                            ResultMatrix[i, j ] = VecM[i]- sumOfRows;       
                        }
                        else 
                        {
                            ResultMatrix[i, j] = VecN[j] - sumOfColumns;     
                        }
                    }
                }
            }
            for (int i = 0; i < rows; i++) //Чищю матрицу
            {
                for (int j = 0; j < columns; j++)
                {
                    if (ResultMatrix[i,j]==-1)
                    {
                        ResultMatrix[i,j] = 0;
                    } 
                }

            }
            ShowMatrix(ResultMatrix);
            return ResultMatrix;
        }

    }
}
