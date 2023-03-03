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

        {
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
                //Console.WriteLine("Значение целевой функции - {0}", ValueObjectFunc(MatrixOfCoefficients, Result));
            }
            else
            {
                Console.WriteLine("Тождество не пременимо для двух векторов. Программа даёт сбой");
            }
        }

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
                    while (counter != SomeVector.Length-1) //it is may in start of row or bellow
                    {
                        if (counter == j || MatCoef[i, counter] == -2) { counter++; continue; } //check it !!
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
                    while (counter != SomeVector.Length-1)
                    {
                        if (counter == i || MatCoef[counter, j] == -2) { counter++; continue; } //!!
                        else { MatCoef[counter, j] = -1; counter++; }
                    }
                }
            }
        }


        private static int countEmptyCoef(int[,] MatCoef, int rows, int columns)
        {
            int count = 0;
            for (int i = 0; i < rows; i++)
			{
                for (int j = 0; j < columns; j++)
			    {
                    if (MatCoef[i,j] == -2) {count++; }
			    }
			}
            return count;
        }

        private static void markAsEmptyColumnOrRow(int[,] MinRes, int[,]MatCoef, int[] VeN, int[]VeM, int i, int j, bool flag )
        {
            //MinRes[i,j]|  VeM[i]
            //___________|
            //VeN[j]
            if (flag == true)
            {
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
            else
            {
                if (MatCoef[i, j] != -2) { MatCoef[i, j] = -1; }
                if (MinRes[i,j] == VeN[j])
                { 
                    ClearSomeRowOrColumn(MatCoef, VeN, i, j, false);
                }
                else
                {
                    ClearSomeRowOrColumn(MatCoef, VeM, i, j, true);
                }
            }
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
            while (allCells > 0 ) 
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
                                markAsEmptyColumnOrRow(MinRes, MatCoef, VeN, VeM, i, j, true);
                            }
                            else if ((sumOfI < VeN[j] || sumOfJ < VeM[i]) && (sumOfI != VeN[i] || sumOfJ != VeM[i]))
                            {//like [j,1]+[j,2]+...+[j,j] > VeN[j] 

                                if (sumOfI > 0 && sumOfJ > 0)
                                {//Need to know what is bigger
                                    if (sumOfI - sumOfJ > 0)
                                    {
                                        MinRes[i, j] = sumOfI - sumOfJ;
                                        markAsEmptyColumnOrRow(MinRes, MatCoef, VeN, VeM, i, j, false);
                                    }
                                    else
                                    {
                                        MinRes[i, j] = sumOfJ - sumOfI; //Start from !!
                                        markAsEmptyColumnOrRow(MinRes, MatCoef, VeN, VeM, i, j, false);
                                    }
                                }
                                else if (sumOfI > 0)
                                {//when row is not clear
                                    MinRes[i, j] = sumOfI - VeM[i];
                                    markAsEmptyColumnOrRow(MinRes, MatCoef, VeN, VeM, i, j, true);
                                }
                                else if (sumOfJ > 0)
                                {//when column is not clear
                                    MinRes[i, j] = sumOfJ - VeN[j];
                                    markAsEmptyColumnOrRow(MinRes, MatCoef, VeN, VeM, i, j, true);
                                }


                            }
                        }
                    }
                }
                int countNul = countNull(rows, columns, MatCoef);
                allCells -= countNul;
                clearMarkedColumnsOrRow(MatCoef, rows, columns, min); //кароче хуй знает потом посмотри
                
                
            }

            return MinRes;
        }


        //private static int FindCountMinEl(int rows, int columns, int min, int[,] MatCoef)
        //{
        //    int countMinEl = 0;
        //    for (int i = 0; i < rows; i++)
        //    {
        //        for (int j = 0; j < columns; j++)
        //        { if (min == MatCoef[i, j]) { countMinEl++; } }
        //    } // Найти количество минимальных элементов внутри массива
        //    return countMinEl;
        //}  
        //private static int FindNegativeEl(int rows, int columns, int min, int[,] MatCoef)
        //{
        //    int count = 0;
        //    for (int i = 0; i < rows; i++)
        //    {
        //        for (int j = 0; j < columns; j++)
        //        { if (MatCoef[i, j] == -1) { count++; MatCoef[i, j] = -2;   } }
        //    } // Найти количество минимальных элементов внутри массива
        //    return count;
        //}

        //private static int findSum(int[] SomeVec, int someIterator, bool flag, int[,] MinRes)
        //{
        //    if (flag == true)
        //    {
        //        int someCounter = 0, sum = 0;
        //        while (someCounter <= SomeVec.Length - 1)//Идёт по i
        //        { sum += MinRes[someCounter, someIterator]; someCounter++; }//считаем сумму  по j
        //        return sum;
        //    }
        //    else
        //    {
        //        int someCounter = 0, sum = 0;
        //        while (someCounter != SomeVec.Length - 1)
        //        { sum += MinRes[someIterator, someCounter]; someCounter++; }//считаем сумму  по i
        //        return sum;
        //    }
        //}

        //private static void clearI(int[] VeM, int[,] MinRes, int i, int j, int[,] MatCoef)
        //{
        //    //Чиситм по i
        //    int sum = findSum(VeM, i, false, MinRes);

        //    if (sum - VeM[i] == 0) { MinRes[i, j] = VeM[j]; }//Отдали элемент         }
        //    else { MinRes[i, j] = VeM[i] - sum; } //Отдали разность до которой не хватает   

        //    if (MatCoef[i, j] != -2) { MatCoef[i, j] = -1; }



        //    if (j == VeM.Length - 1)
        //    {//Всех правее
        //        int someCounter = 0;
        //        while (someCounter != VeM.Length - 1)
        //        {
        //            if (MatCoef[i, someCounter] != -2) // Нужно чтобы не переписывать -2 (элементы которые были)
        //            {
        //                MatCoef[i, someCounter] = -1;
        //                someCounter++;
        //            }
        //            else { someCounter++; }
        //        }
        //    }
        //    else
        //    {//Либо слева либо по середине
        //        int someCounter = 0;
        //        while (someCounter <= VeM.Length - 1)
        //        {
        //            if (someCounter == j || MatCoef[someCounter, j] == -2) { someCounter++; continue; } //someCounter was i
        //            else
        //            {
        //                MatCoef[i, someCounter] = -1;
        //                someCounter++;
        //            }

        //        }
        //    }
        //}

        //private static void clearJ(int[] VeN, int[,] MinRes, int i, int j, int[,]MatCoef )
        //{
        //    //Чистим по j
        //    int sum = findSum(VeN, j, true, MinRes);

        //    if (sum - VeN[j] == 0) { MinRes[i, j] = VeN[j]; }//Отдали элемент         } // VenJ -  was
        //    else { MinRes[i, j] = VeN[j] - sum; } //Отдали разность до которой не хватает    
        //    //Разные элементы

        //    if (MatCoef[i, j] != -2) { MatCoef[i, j] = -1; } //Как ориентир

        //    //Как то плохо чистит и не усматривает что там ещё 10

        //    if (i == VeN.Length - 1)
        //    {//Элемент внизу
        //        int someCounter = 0;
        //        while (someCounter != VeN.Length - 1)
        //        {
        //            if (MatCoef[someCounter, j] != -2) //Поменял на MatCoef !!
        //            {
        //                MatCoef[someCounter, j] = -1;// Нужно чтобы не переписывать -2 (элементы которые были)
        //                someCounter++;
        //            }
        //            else { someCounter++; }
        //        }    //Заполняем вверх
        //    }
        //    else
        //    {
        //        int someCounter = 0;
        //        while (someCounter <= VeN.Length - 1)
        //        {
        //            //someCounter was j
        //            if (someCounter == i || MatCoef[someCounter, j] == -2) { someCounter++; continue; }//someCounter was i
        //            else
        //            {
        //                MatCoef[someCounter, j] = -1;
        //                someCounter++;
        //            }


        //        }
        //    }
        //}

        //private static int[,] MinimalResult(int[,] MatCoef, int[] VeM, int[] VeN)
        //{
        //    int rows = MatCoef.GetUpperBound(0) + 1;
        //    int columns = MatCoef.Length / rows;
        //    int[,] MinRes = new int[rows, columns];


        //    int allCells = VeM.Length*VeN.Length;//все ячейки
        //    while (allCells != 0)
        //    {
        //        int min = FindMinEl(rows, columns, MatCoef);
        //        int countMinEl = FindCountMinEl(rows, columns, min, MatCoef);

        //        for (int i = 0; i < rows; i++)//Приводим в порядок коэфиценты
        //        {
        //            for (int j = 0; j < columns; j++)
        //            {
        //                if (MatCoef[i, j] == min)//Находит минимальный
        //                {
        //                    //if (findSum(VeN, j, false, MinRes) < VeM[i]) //Если сумма меньше правого
        //                    //{
        //                    //    MinRes[i,j] = VeM[i] - findSum(VeN, j, false, MinRes);
        //                    //    clearI(VeM, MinRes, i, j, MatCoef);
        //                    //}
        //                    //else if (findSum(VeM, i, true, MinRes) == VeN[i])
        //                    //{
        //                    //    MatCoef[i, j] = -1;
        //                    //}
        //                    if (VeM[i] > VeN[j]) //Право > низ
        //                    {
        //                        clearJ(VeN, MinRes, i, j, MatCoef);//Сравнивает по j

        //                        int sum = findSum(VeM, i, true, MinRes);
        //                        if (sum > VeN[i] ) // was Vem[i]
        //                        {
        //                            clearI(VeM, MinRes, i, j, MatCoef);//Сравнивает по i
        //                        }     
        //                    }
        //                    else if (VeN[j] > VeM[i]) //Низ больше чем право
        //                    {
        //                        clearI(VeM, MinRes, i, j, MatCoef);//НЕ работает

        //                        if (findSum(VeN, j, false, MinRes) > VeM[i])
        //                        {
        //                            clearJ(VeN, MinRes, i, j, MatCoef);
        //                        }


        //                        //int sum = findSum(VeN, j, false, MinRes);
        //                        //if (sum > VeN[j])
        //                        //{
        //                        //    clearJ(VeN, MinRes, i, j, MatCoef);//Сравнивает по j;
        //                        //}

        //                    }
        //                    else//Здесь равны
        //                    {//Чистим и i и j

        //                        int someCounterI = 0, someCounterJ = 0; //sumI = 0, sumJ = 0;

        //                        int sumI = findSum(VeM, j, true, MinRes);
        //                        int sumJ = findSum(VeN, i, false, MinRes);


        //                        if (sumJ == 0 && sumI == 0)
        //                        {
        //                            MinRes[i, j] = VeN[j];
        //                        }
        //                        else if (sumJ < sumI)
        //                        {
        //                            MinRes[i, j] = VeN[j] - sumJ;
        //                        }
        //                        else if (sumI < sumJ)
        //                        {
        //                            MinRes[i, j] = VeM[i] - sumI;
        //                        }
        //                        else { MinRes[i, j] = VeM[i] - sumI; }


        //                        if (MatCoef[i, j] != -2) { MatCoef[i, j] = -1; }


                                
        //                        //Тут Очент слложно так как код отредачен из I J модулей
        //                        //Но в данном случае лучше всего работает всё нижеописанное, поэтому лучше разобраться
                                 
        //                        if (i == VeN.Length - 1) //По Верху Низу
        //                        {//Он внизу
        //                            int someCounter = 0;
        //                            while (someCounter != VeN.Length - 1)
        //                            {
        //                                if (MinRes[someCounter, j] != -2)
        //                                {
        //                                    MatCoef[someCounter, j] = -1;// Нужно чтобы не переписывать -2 (элементы которые были)
        //                                    someCounter++;
        //                                }
        //                                else { someCounter++; }
        //                            }    //Заполняем вверх
        //                        }
        //                        else
        //                        { //Если не равен 2, не с низу
        //                            int someCounter = 0;
        //                            while (someCounter <= VeN.Length - 1)
        //                            {
        //                                MatCoef[someCounter, j] = -1;
        //                                someCounter++;
        //                            }
        //                        }

        //                        if (j == VeM.Length - 1)    //По i По лева права
        //                        {//Всех правее
        //                            int someCounter = 0;
        //                            while (someCounter != VeM.Length - 1)
        //                            {
        //                                if (MatCoef[i, someCounter] != -2) // Нужно чтобы не переписывать -2 (элементы которые были)
        //                                {
        //                                    MatCoef[i, someCounter] = -1;
        //                                    someCounter++;
        //                                }
        //                                else { someCounter++; }
        //                            }
        //                        }
        //                        else
        //                        {//Либо слева либо по середине
        //                            int someCounter = 0;
        //                            while (someCounter <= VeM.Length - 1)
        //                            {
        //                                MatCoef[i, someCounter] = -1;
        //                                someCounter++;
        //                            }
        //                        }

        //                    }
        //                }

        //            }
        //        }



        //        //ShowMatrix(MatCoef);
                
               
                
        //        Console.WriteLine();
        //        allCells -= countMinEl;
        //        allCells = allCells -  FindNegativeEl(rows, columns, min, MatCoef) + countMinEl;

        //    }



        //    //Console.WriteLine($" {min}  and {countMinEl}");

        //    return MinRes;
        //}

    }
}
