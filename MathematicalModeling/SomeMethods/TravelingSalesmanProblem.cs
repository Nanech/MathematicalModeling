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

        protected static int[,] InitRandNumb(int[,] Matrix)
        {
            //НАДО ЧТОБЫ НА СТОЛБЦЕ НЕ БЫЛО ПОВТОРЯЮЩИХСЯ ЗНАЧЕНИЙ
            int rows = Matrix.GetUpperBound(0) + 1;
            int columns = Matrix.Length / rows;
            Random r = new Random();
            int[] some = new int[rows];
            for (int i = 0; i < rows; i++)
            {
                bool flag = true;
                while(flag)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (i == j) { Matrix[i, j] = 0; }
                        else { Matrix[i, j] = r.Next(1, 6); }
                        for (int z = 0; z <= j; z++)
                        {
                            if (Matrix[i, j] != 0 && Matrix[i, j] > 0)
                            {
                                continue;
                            }
                            else if (z != 0 && Matrix[i, z] == some[z] && j != z)
                            {
                                flag = true;
                                Matrix[i, z] = r.Next(1, 6);
                            }
                            else if (flag == true) { flag = false; }
                        }
                    }
                }
            }
            Console.WriteLine("Была воспроизведена следующая матрица:");
            ShowMatrix(Matrix);
            return Matrix;

            //int count = r.Next(1, 6);
            //bool flag = false;
            //for (int k = 0; k < j; k++)
            //{
            //    while (!flag)
            //    {

            //    }
            //}
        }




        







    }
}
