using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicalModeling.SomeMethods
{
    internal class TravelingSalesmanProblem:GeneralClass
    {
        public static void startTSP()
        {
            int[,] MainMatrix = new int[5, 5];
            Console.WriteLine();

            //MainMatrix = ResultMatrix(MainMatrix);
            Console.WriteLine("Ваш результат");
            //ShowMatrixSimplex(MainMatrix);
            MainMatrix = InitRandNumb(MainMatrix);

        }

        //protected static int[,] InitRandNumb(int[,] Matrix)
        //{
        //    НАДО ЧТОБЫ НА СТОЛБЦЕ НЕ БЫЛО ПОВТОРЯЮЩИХСЯ ЗНАЧЕНИЙ
        //    int rows = Matrix.GetUpperBound(0) + 1;
        //    int columns = Matrix.Length / rows;
        //    Random r = new Random();
        //    int[] some = new int[rows];
        //    for (int i = 0; i < rows; i++)
        //    {
        //        bool flag = true;
        //        while (flag)
        //        {
        //            for (int j = 0; j < columns; j++)
        //            {
        //                if (i == j) { Matrix[i, j] = 0; }
        //                else { Matrix[i, j] = r.Next(1, 6); }
        //                for (int z = 0; z <= j; z++)
        //                {
        //                    if (Matrix[i, j] != 0 && Matrix[i, j] > 0)
        //                    {
        //                        continue;
        //                    }
        //                    else if (z != 0 && Matrix[i, z] == some[z] && j != z)
        //                    {
        //                        flag = true;
        //                        Matrix[i, z] = r.Next(1, 6);
        //                    }
        //                    else if (flag == true) { flag = false; }
        //                }
        //            }
        //        }
        //    }
        //    Console.WriteLine("Была воспроизведена следующая матрица:");
        //    ShowMatrix(Matrix);
        //    return Matrix;

        //    int count = r.Next(1, 6);
        //    bool flag = false;
        //    for (int k = 0; k < j; k++)
        //    {
        //        while (!flag)
        //        {

        //        }
        //    }
        //}

        protected static int[,] InitRandNumb(int[,] Matrix)
        {
            //It is always for this
            int rows = Matrix.GetUpperBound(0) + 1;
            int columns = Matrix.Length / rows;
            Random r = new Random();

            //Надо сделать так чтобы если i==j было всегда 0, и не повторялись значения ряда 
            for (int i = 0; i < rows; i++)
            {
                bool flag = true;

                while (flag)
                {
                    int count = 0;
                    for (int j = 0; j < columns; j++)
                    {
                        //Стадия заполнения
                        if (i == j) { Matrix[i, j] = 0; }
                        else { Matrix[i, j] = r.Next(1, 6); }
                        count++;
                        if (count >= 2)//Если заполенено больше двух значений
                        {
                            for (int z = 0; z <= j; z++)
                            {
                                if (Matrix[i, z] != 0)//Если не i == j
                                {
                                    if ( z != j) //Если взятые значения не одни и теже
                                    {
                                        if (Matrix[i,z] == Matrix[i,j])
                                        {//Если значения равны
                                            Matrix[i,z] = r.Next(1,6);
                                        }
                                        else { continue; }

                                    }
                                    else { continue; }
                                }
                                else { continue; }


                            }

                        }


                    }

                }

              

            }




            return Matrix;
        }













    }
}
