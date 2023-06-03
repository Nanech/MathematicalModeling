using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathematicalModeling.SomeMethods;//Подключили папку

namespace MathematicalModeling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //NorthWestMethod.StartNorthWest();

            //MinimalElement.StartMinimElem();

            //SimplexMethod.StartSimplex();

            //TravelingSalesmanProblem.startTSP();

            // Code of Prufer
            //PruferCode.Decoding();
            //PruferCode.Coding();

            // Johnson method
            
            Johnson.StartThisMethod();

            // Models

            //Models.HunterXVictim();

            //Models.MonteCarlo();

            //Models.MonteCarloEtap4();

            //Models.MonteCarloEtap5();

            Models.zad1();

            Models.zad2();

            Models.zad3();

            Models.zad4();

            Models.zad5();

            Models.zad6();

            Console.ReadKey();
        }
    }
}
