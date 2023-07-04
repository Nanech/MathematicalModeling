using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MathematicalModeling.SomeMethods
{
    internal class MinimalElement:GeneralClass
    {
        public static void StartMinimElem()
        {//Переделывать General Class ValueObjectFunc()
            int[] VectorM = new int[3];
            int[] VectorN = new int[3];
            int[,] MatrixOfCoefficients = new int[VectorM.Length, VectorN.Length];
            int[,] Result = new int[VectorM.Length, VectorN.Length];
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
                ShowMatrix(Result);
                Console.WriteLine();
                Console.WriteLine("Значение целевой функции - {0}", ValueObjectFunc(MatrixOfCoefficients, Result));
            }
            else
            {
                Console.WriteLine("Тождество не пременимо для двух векторов. Программа даёт сбой");
            }
        }

        //public static void MethodFromGPT()
        //{
        //    int[,] costs = { /* заполнить стоимостями из условия задачи */ };
        //    int[,] distribution = new int[costs.GetLength(0), costs.GetLength(1)];

        //    while (true)
        //    {
        //        int min = int.MaxValue;
        //        int minRow = -1;
        //        int minColumn = -1;

        //        for (int i = 0; i < costs.GetLength(0); i++)
        //        {
        //            for (int j = 0; j < costs.GetLength(1); j++)
        //            {
        //                if (costs[i, j] < min)
        //                {
        //                    min = costs[i, j];
        //                    minRow = i;
        //                    minColumn = j;
        //                }
        //            }
        //        }

        //        if (minRow == -1 || minColumn == -1)
        //            break;

        //        int demand = /* получить потребность в строке minRow */;
        //        int supply = /* получить запас в столбце minColumn */;

        //        if (demand == supply)
        //        {
        //            distribution[minRow, minColumn] = demand;

        //            /* удалить строку minRow и столбец minColumn из рассмотрения */
        //        }
        //        else if (demand < supply)
        //        {
        //            distribution[minRow, minColumn] = demand;
        //            /* уменьшить запас в столбце minColumn на величину demand */
        //            /* удалить строку minRow из рассмотрения */
        //        }
        //        else
        //        {
        //            distribution[minRow, minColumn] = supply;
        //            /* увеличить запас в столбце minColumn на величину supply */
        //            /* удалить столбец minColumn из рассмотрения */
        //        }
        //    }

        //    // Вывод матрицы распределения перевозок
        //    for (int i = 0; i < distribution.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < distribution.GetLength(1); j++)
        //        {
        //            Console.Write(distribution[i, j] + " ");
        //        }
        //        Console.WriteLine();
        //    }
        //}


        /// <summary>
        /// find the minimall element in the Matrix of Coeficence
        /// </summary>
        /// <param name="rows">it is like I</param>
        /// <param name="columns">it is like J</param>
        /// <param name="MatCoef">Matrix of Coeficence</param>
        /// <returns></returns>
        private static int findMinEl(int rows, int columns, int[,]MatCoef)
        {
            int min = 100;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (min > MatCoef[i, j] && MatCoef[i,j] != -1 && MatCoef[i,j] != -2) { min = MatCoef[i, j]; }
                }
            }
            return min;
        }


        /// <summary>
        /// find count min element in matrix of coeficence
        /// </summary>
        /// <param name="rows">it is i</param>
        /// <param name="columns">it is j</param>
        /// <param name="MatCoef">it is matrix of coeficence</param>
        /// <param name="min">it is minimal element</param>
        /// <returns></returns>
        private static int countMinEl(int rows, int columns, int[,]MatCoef, int min)
        {
            int count = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (MatCoef[i, j] == min) { count++; } 
                }
            }
            return count; 
        }

        private static int findSumOnSomething(int[,] MinRes, int[] SomeVector, int SomeIterator, bool flag  )
        {
            int sum = 0, counter = 0;
            if (flag == true){ //it is like we need to know sum on I
                while (counter != SomeVector.Length)
                {//going to the end like [SomeIterator,1] -> [SomeIterator,2]
                    sum += MinRes[SomeIterator, counter]; counter++; }
            }
            else{ //it is like we need to know sum on j
                while(counter != SomeVector.Length)
                {//going to the end like [0,SomeIterator] -> [1,SomeIterator]
                    sum += MinRes[counter, SomeIterator]; counter++; }
            }
            return sum;
        }

        private static void ClearSomeRowOrColumn(int[,] MatCoef, int[] SomeVector, int i, int j, bool flag)
        {
            int counter = 0;
            if (flag == true)
            { //We need to clear row (J)
                if (j == SomeVector.Length - 1)
                {//it is means the iterator is last in row
                    while (counter <= j) //clear all row till the [SomeIterator, counter]
                    { MatCoef[i, counter] = -1; counter++; } }
                else 
                {
                    while (counter <= SomeVector.Length-1) //it is may in start of row or bellow
                    {
                        //counter <= SomeVector
                        //add MatCoef[i, counter]
                        if (counter == j || MatCoef[i, counter] == -1 || MatCoef[i, counter] == -2) { counter++; continue; } //check it !!
                        else { MatCoef[i, counter] = -1; counter++; }
                    }
                }
            }
            else
            {//We need to clear column (I)
                if (i == SomeVector.Length-1)
                {//it is means the iterator is last in column

                    while (counter <= i)
                    { MatCoef[counter, j] = -1; counter++;}
                }
                else
                {
                    while (counter <= SomeVector.Length-1)
                    {
                        //counter <= SomeVector
                        if (counter == i || MatCoef[counter, j] == -1 || MatCoef[counter, j] == -2) { counter++; continue; }
                        else { MatCoef[counter, j] = -1; counter++; }
                    }
                }
            }
        }



        private static void markAsEmptyColumnOrRow(int[,] MinRes, int[,]MatCoef, int[] VeN, int[]VeM, int i, int j )
        {
            //MinRes[i,j]|  VeM[i]
            //___________|
            //VeN[j]
             //when sum is empty
            if (VeN[j] < VeM[i]) //here problem !! 
            {
                MinRes[i, j] = VeN[j];
                //We need to clear all row (J)
                    
                if (MatCoef[i, j] != -2) { MatCoef[i, j] = -1; } //If coef is not cleared early  -> mark it is empty
                ClearSomeRowOrColumn(MatCoef, VeM, i, j, false); //J is fixed on column cleaning
                    
            }
            else if (VeM[i] < VeN[j])
            {
                MinRes[i, j] = VeM[i];
                //We nees to clear all row (I) on the MatrixCoef
                if (MatCoef[i, j] != -2) { MatCoef[i, j] = -1; }
                ClearSomeRowOrColumn(MatCoef, VeN, i, j, true); //I is fixed on row clearning
            }
            else
            {//it is equals
                MinRes[i, j] = VeN[j]; //No matter what we place in it
                if (MatCoef[i, j] != -2) { MatCoef[i, j] = -1; }
                ClearSomeRowOrColumn(MatCoef, VeM, i, j, true); //J is fixed on column cleaning
                ClearSomeRowOrColumn(MatCoef, VeN, i, j, false); //I is fixed on row clearning
            }
        }

        private static void markAsEmptyColumnOrRow(int[,] MinRes, int[,] MatCoef, int[] VeN, int[] VeM, int i, int j, int sum)
        {
             //when sum isn`t empty 
            if (MatCoef[i, j] != -2) { MatCoef[i, j] = -1; } //Then we need to check is sum isn`t > 0, then по отдельности

            if (sum + MinRes[i,j] == VeN[j]) //when sum of J == Ven[J]
            {
                ClearSomeRowOrColumn(MatCoef, VeN, i, j, false);
            }
            else if (sum + MinRes[i,j] == VeM[i])
            {
                ClearSomeRowOrColumn(MatCoef, VeM, i, j, true);
            }
            //if (MinRes[i, j] == VeN[j]) //Strange if maybe we need sum right here?
            //{
            //    ClearSomeRowOrColumn(MatCoef, VeN, i, j, false);
            //}
            //else
            //{
            //    ClearSomeRowOrColumn(MatCoef, VeM, i, j, true);
            //}       
        }

        private static int clearMarkedColumnsOrRow(int[,]MatCoef ,int rows, int columns, int min)
        {
            int count = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (MatCoef[i, j] == -1) { count++; MatCoef[i, j] = -2; }
                }
            }
            return count;
        }

        private static int countNull(int rows, int columns, int[,]MatCoef)
        {
            int count = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (MatCoef[i, j] == -1) { count++; }
                }
            }
            return count;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="MatCoef">матрица коэфицентов</param>
        /// <param name="VeM">вектор располагается справа в виде J</param>
        /// <param name="VeN">вектор располагается снизу в виде I</param>
       /// <returns></returns>
        private static int[,] MinimalResult(int[,] MatCoef, int[] VeM, int[] VeN) 
        {
            int rows = MatCoef.GetUpperBound(0) + 1;
            int columns = MatCoef.Length / rows;
            int[,] MinRes = new int[rows, columns];
            int allCells = VeM.Length * VeN.Length;//The Cells of Matrix
            while (allCells >= 0 ) 
            {
                int min = findMinEl(rows, columns, MatCoef);
                int count = countMinEl(rows, columns, MatCoef, min);
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (MatCoef[i, j] == min)
                        {//when min is MatCoef
                            int sumOfI = findSumOnSomething(MinRes, VeM, i, true);//
                            int sumOfJ = findSumOnSomething(MinRes, VeN, j, false);
                            if (sumOfI == 0 && sumOfJ == 0)
                            { //It like all is empty
                                markAsEmptyColumnOrRow(MinRes, MatCoef, VeN, VeM, i, j); //was true
                            }
                            else if ((sumOfI < VeN[j] || sumOfJ < VeM[i]) && (sumOfI != VeN[i] || sumOfJ != VeM[i]))
                            {//like [j,1]+[j,2]+...+[j,j] > VeN[j] 

                                if (sumOfI > 0 && sumOfJ > 0)
                                {//Need to know what is bigger
                                    if (sumOfI - sumOfJ > 0)
                                    {
                                        //when row sum bigger then column sum
                                        MinRes[i, j] = sumOfI - sumOfJ;
                                        markAsEmptyColumnOrRow(MinRes, MatCoef, VeN, VeM, i, j, sumOfI); //false
                                    }
                                    else
                                    {
                                        //when column sum bigger then row sum
                                        MinRes[i, j] = sumOfJ - sumOfI; //Start from !!
                                        markAsEmptyColumnOrRow(MinRes, MatCoef, VeN, VeM, i, j, sumOfJ); //false
                                    }
                                }
                                else if (sumOfI > 0)
                                {//when row is not clear
                                    MinRes[i, j] = VeM[i]  - sumOfI;  //change sumOf with Vector 
                                    markAsEmptyColumnOrRow(MinRes, MatCoef, VeN, VeM, i, j, sumOfI) ; //false
                                    //here were true is not right?
                                }
                                else if (sumOfJ > 0)
                                {//when column is not clear
                                    MinRes[i, j] = VeN[j] -  sumOfJ; //change sumOf with Vector
                                    markAsEmptyColumnOrRow(MinRes, MatCoef, VeN, VeM, i, j, sumOfJ); //false
                                }


                            }
                        }

                    }
                }
                int countNul = countNull(rows, columns, MatCoef);
                allCells -= countNul;
                clearMarkedColumnsOrRow(MatCoef, rows, columns, min); 
            }
            //Check sum idk why is -10
            return MinRes;
        }
    }
}
