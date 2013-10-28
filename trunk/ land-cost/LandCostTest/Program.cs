using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LandCost.Entities;
using System.Globalization;

namespace LandCostTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dt = DateTime.Now;
            DateTimeFormatInfo fmt = (new CultureInfo("uk-UA")).DateTimeFormat;
            Console.WriteLine(dt.ToString("D", fmt));
            Console.ReadKey();
        }
    }
}
