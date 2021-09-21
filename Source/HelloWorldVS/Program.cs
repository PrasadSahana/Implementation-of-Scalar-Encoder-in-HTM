
using CalculatorLib;
using System;

namespace HelloWorldVS
{
    class Program
    {
        /// <summary>
        /// Calculator demo.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            BetterCalculator bCalc = new BetterCalculator();

            int res = bCalc.Add(221, 42343);
            res = bCalc.Mul(221, 42343);
            var res3 = bCalc.Div(221, 42343);

            Console.WriteLine(res);

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }
    }
}
