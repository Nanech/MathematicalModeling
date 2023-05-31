using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicalModeling
{
    internal class Models
    {


        public static void HunterXVictim()
        {
            double startValueVictim = 100;
            double startValueHunter = 20;
            double riseNature = 0.01;
            double deathCoefConcurent = 0.00001;
            double deathCoefHunter = 0.001;
            double deathCoef = 0.1;
            double riseCoef = 0.0001;


            for (int i = 0; i < 32; i++)
            {
                Console.WriteLine("{0} {1} {2}", i, Math.Round( startValueVictim), Math.Round( startValueHunter ));

                double newValueVitim = startValueVictim + (riseNature - deathCoefConcurent
                    * startValueVictim) * startValueVictim - deathCoefHunter * startValueVictim * startValueHunter;
          



                double newValueHunter = startValueHunter - deathCoef * startValueHunter
                    + riseCoef * startValueVictim * startValueHunter;

                startValueHunter = newValueHunter;
                startValueVictim = newValueVitim;

            }


        }


    }
}
