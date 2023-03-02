using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace MathematicalModeling.SomeMethods
{
    internal class MinimalElement:GeneralClass
    {
        public static void StartMinimElem()

        {
            int[] VectorM = new int[4];
            int[] VectorN = new int[4];
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
                ShowMatrix(Result);
                Console.WriteLine();
                //Console.WriteLine("Значение целевой функции - {0}", ValueObjectFunc(MatrixOfCoefficients, Result));
            }
            else
            {
                Console.WriteLine("Тождество не пременимо для двух векторов. Программа даёт сбой");
            }
        }

        private static int FindMinEl(int rows, int columns, int[,] MatCoef )
        {
            int min = 100;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                { if (MatCoef[i, j] <= min && MatCoef[i, j] != -1 && MatCoef[i,j] != -2) { min = MatCoef[i, j]; } }
            }//Найти  минимальный элемент
            return min;
        }
        private static int FindCountMinEl(int rows, int columns, int min, int[,] MatCoef)
        {
            int countMinEl = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                { if (min == MatCoef[i, j]) { countMinEl++; } }
            } // Найти количество минимальных элементов внутри массива
            return countMinEl;
        }  
        private static int FindNegativeEl(int rows, int columns, int min, int[,] MatCoef)
        {
            int count = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                { if (MatCoef[i, j] == -1) { count++; MatCoef[i, j] = -2;   } }
            } // Найти количество минимальных элементов внутри массива
            return count;
        }

        private static int findSum(int[] SomeVec, int someIterator, bool flag, int[,] MinRes)
        {
            if (flag == true)
            {
                int someCounter = 0, sum = 0;
                while (someCounter <= SomeVec.Length - 1)//Идёт по i
                { sum += MinRes[someCounter, someIterator]; someCounter++; }//считаем сумму  по j
                return sum;
            }
            else
            {
                int someCounter = 0, sum = 0;
                while (someCounter != SomeVec.Length - 1)
                { sum += MinRes[someIterator, someCounter]; someCounter++; }//считаем сумму  по i
                return sum;
            }
        }

        private static void clearI(int[] VeM, int[,] MinRes, int i, int j, int[,] MatCoef)
        {
            //Чиситм по i
            int sum = findSum(VeM, i, false, MinRes);

            if (sum - VeM[i] == 0) { MinRes[i, j] = VeM[j]; }//Отдали элемент         }
            else { MinRes[i, j] = VeM[i] - sum; } //Отдали разность до которой не хватает   

            if (MatCoef[i, j] != -2) { MatCoef[i, j] = -1; }



            if (j == VeM.Length - 1)
            {//Всех правее
                int someCounter = 0;
                while (someCounter != VeM.Length - 1)
                {
                    if (MatCoef[i, someCounter] != -2) // Нужно чтобы не переписывать -2 (элементы которые были)
                    {
                        MatCoef[i, someCounter] = -1;
                        someCounter++;
                    }
                    else { someCounter++; }
                }
            }
            else
            {//Либо слева либо по середине
                int someCounter = 0;
                while (someCounter <= VeM.Length - 1)
                {
                    if (someCounter == j || MatCoef[someCounter, j] == -2) { someCounter++; continue; } //someCounter was i
                    else
                    {
                        MatCoef[i, someCounter] = -1;
                        someCounter++;
                    }

                }
            }
        }

        private static void clearJ(int[] VeN, int[,] MinRes, int i, int j, int[,]MatCoef )
        {
            //Чистим по j
            int sum = findSum(VeN, j, true, MinRes);

            if (sum - VeN[j] == 0) { MinRes[i, j] = VeN[j]; }//Отдали элемент         } // VenJ -  was
            else { MinRes[i, j] = VeN[j] - sum; } //Отдали разность до которой не хватает    
            //Разные элементы

            if (MatCoef[i, j] != -2) { MatCoef[i, j] = -1; } //Как ориентир

            //Как то плохо чистит и не усматривает что там ещё 10

            if (i == VeN.Length - 1)
            {//Элемент внизу
                int someCounter = 0;
                while (someCounter != VeN.Length - 1)
                {
                    if (MatCoef[someCounter, j] != -2) //Поменял на MatCoef !!
                    {
                        MatCoef[someCounter, j] = -1;// Нужно чтобы не переписывать -2 (элементы которые были)
                        someCounter++;
                    }
                    else { someCounter++; }
                }    //Заполняем вверх
            }
            else
            {
                int someCounter = 0;
                while (someCounter <= VeN.Length - 1)
                {
                    //someCounter was j
                    if (someCounter == i || MatCoef[someCounter, j] == -2) { someCounter++; continue; }//someCounter was i
                    else
                    {
                        MatCoef[someCounter, j] = -1;
                        someCounter++;
                    }


                }
            }
        }





        private static int[,] MinimalResult(int[,] MatCoef, int[] VeM, int[] VeN)
        {
            int rows = MatCoef.GetUpperBound(0) + 1;
            int columns = MatCoef.Length / rows;
            int[,] MinRes = new int[rows, columns];


            int allCells = VeM.Length*VeN.Length;//все ячейки
            while (allCells != 0)
            {
                int min = FindMinEl(rows, columns, MatCoef);
                int countMinEl = FindCountMinEl(rows, columns, min, MatCoef);

                for (int i = 0; i < rows; i++)//Приводим в порядок коэфиценты
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (MatCoef[i, j] == min)//Находит минимальный
                        {
                            //if (findSum(VeN, j, false, MinRes) < VeM[i]) //Если сумма меньше правого
                            //{
                            //    MinRes[i,j] = VeM[i] - findSum(VeN, j, false, MinRes);
                            //    clearI(VeM, MinRes, i, j, MatCoef);
                            //}
                            //else if (findSum(VeM, i, true, MinRes) == VeN[i])
                            //{
                            //    MatCoef[i, j] = -1;
                            //}
                            if (VeM[i] > VeN[j]) //Право > низ
                            {
                                clearJ(VeN, MinRes, i, j, MatCoef);//Сравнивает по j

                                int sum = findSum(VeM, i, true, MinRes);
                                if (sum > VeN[i] ) // was Vem[i]
                                {
                                    clearI(VeM, MinRes, i, j, MatCoef);//Сравнивает по i
                                }     
                            }
                            else if (VeN[j] > VeM[i]) //Низ больше чем право
                            {
                                clearI(VeM, MinRes, i, j, MatCoef);//НЕ работает

                                if (findSum(VeN, j, false, MinRes) > VeM[i])
                                {
                                    clearJ(VeN, MinRes, i, j, MatCoef);
                                }


                                //int sum = findSum(VeN, j, false, MinRes);
                                //if (sum > VeN[j])
                                //{
                                //    clearJ(VeN, MinRes, i, j, MatCoef);//Сравнивает по j;
                                //}

                            }
                            else//Здесь равны
                            {//Чистим и i и j

                                int someCounterI = 0, someCounterJ = 0; //sumI = 0, sumJ = 0;

                                int sumI = findSum(VeM, j, true, MinRes);
                                int sumJ = findSum(VeN, i, false, MinRes);


                                if (sumJ == 0 && sumI == 0)
                                {
                                    MinRes[i, j] = VeN[j];
                                }
                                else if (sumJ < sumI)
                                {
                                    MinRes[i, j] = VeN[j] - sumJ;
                                }
                                else if (sumI < sumJ)
                                {
                                    MinRes[i, j] = VeM[i] - sumI;
                                }
                                else { MinRes[i, j] = VeM[i] - sumI; }


                                if (MatCoef[i, j] != -2) { MatCoef[i, j] = -1; }


                                
                                //Тут Очент слложно так как код отредачен из I J модулей
                                //Но в данном случае лучше всего работает всё нижеописанное, поэтому лучше разобраться
                                 
                                if (i == VeN.Length - 1) //По Верху Низу
                                {//Он внизу
                                    int someCounter = 0;
                                    while (someCounter != VeN.Length - 1)
                                    {
                                        if (MinRes[someCounter, j] != -2)
                                        {
                                            MatCoef[someCounter, j] = -1;// Нужно чтобы не переписывать -2 (элементы которые были)
                                            someCounter++;
                                        }
                                        else { someCounter++; }
                                    }    //Заполняем вверх
                                }
                                else
                                { //Если не равен 2, не с низу
                                    int someCounter = 0;
                                    while (someCounter <= VeN.Length - 1)
                                    {
                                        MatCoef[someCounter, j] = -1;
                                        someCounter++;
                                    }
                                }

                                if (j == VeM.Length - 1)    //По i По лева права
                                {//Всех правее
                                    int someCounter = 0;
                                    while (someCounter != VeM.Length - 1)
                                    {
                                        if (MatCoef[i, someCounter] != -2) // Нужно чтобы не переписывать -2 (элементы которые были)
                                        {
                                            MatCoef[i, someCounter] = -1;
                                            someCounter++;
                                        }
                                        else { someCounter++; }
                                    }
                                }
                                else
                                {//Либо слева либо по середине
                                    int someCounter = 0;
                                    while (someCounter <= VeM.Length - 1)
                                    {
                                        MatCoef[i, someCounter] = -1;
                                        someCounter++;
                                    }
                                }

                            }
                        }

                    }
                }



                //ShowMatrix(MatCoef);
                
               
                
                Console.WriteLine();
                allCells -= countMinEl;
                allCells = allCells -  FindNegativeEl(rows, columns, min, MatCoef) + countMinEl;

            }



            //Console.WriteLine($" {min}  and {countMinEl}");

            return MinRes;
        }

    }
}
