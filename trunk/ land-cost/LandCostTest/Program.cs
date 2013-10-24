using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LandCost.Entities;

namespace LandCostTest
{
    class Program
    {
        static void Main(string[] args)
        {
            FunctionalUsage fu1 = new FunctionalUsage("For fun", 2.5);
            FunctionalUsage fu2 = new FunctionalUsage("For work", 2.4);

            LocalCoefficient c1 = new LocalCoefficient("coef 1");
            LocalCoefficient c2 = new LocalCoefficient("coef 2");

            LocalCoefficientValue cv1 = new LocalCoefficientValue(c1, 0.2);
            LocalCoefficientValue cv2 = new LocalCoefficientValue(c2, 0.3);

            FunctionalUsageCoefficients fuc1 = new FunctionalUsageCoefficients(fu1);
            fuc1.AddCoefficientValue(cv1);

            FunctionalUsageCoefficients fuc2 = new FunctionalUsageCoefficients(fu1);
            fuc2.AddCoefficientValue(cv2);

            FunctionalUsageCoefficients fuc3 = new FunctionalUsageCoefficients(fu2);
            fuc3.AddCoefficientValue(cv1);
            fuc3.AddCoefficientValue(cv2);

            FunctionalUsageCoefficients fuc4 = new FunctionalUsageCoefficients(fu1);
            fuc4.AddCoefficientValue(cv1);

            Console.WriteLine(fuc1.Equals(fuc4));
            Console.ReadKey();
        }
    }
}
