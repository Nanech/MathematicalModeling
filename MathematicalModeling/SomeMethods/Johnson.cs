using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicalModeling.SomeMethods
{
    internal class Johnson
    {
        protected void MethodForCheck()
        {
            //Check 
        }

        public static void StartThisMethod()
        {
            List<int> first = new List<int>() {2, 8,  2  /*4*/  , 8, 6, 9 };
            List<int> second = new List<int>() {3, 3, 6, 5, 8, 7 };

            int minOfFirst = first.IndexOf(first.Min());
            int minOfSecond = second.Min();

            Console.WriteLine(minOfFirst);


            List<int> topRight = new List<int>();
            List<int> topLeft = new List<int>();
            List<int> bottomRight = new List<int>();
            List<int> bottomLeft = new List<int>();


            if (minOfFirst < minOfSecond)
            {
                topLeft.Add(minOfFirst);
                int index = first.IndexOf(minOfFirst);
                Console.WriteLine(index);
                //topRight.Add(second[second.IndexOf(first.Min())]);

            }
            else
            {

                bottomRight.Add(minOfSecond);
                bottomLeft.Add(first[first.IndexOf(minOfSecond)]);

            }

        }


    }
}
