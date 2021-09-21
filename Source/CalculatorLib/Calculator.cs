using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorLib
{
    public class Calculator 
    {

        /// <summary>
        /// Adds two numbers.
        /// </summary>
        /// <param name="a">Operand 1</param>
        /// <param name="b">Operand 2</param>
        /// <returns>Sum of a and b.</returns>
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Sub(int a, int b)
        {
            return a - b;
        }


        public double Div(int a, int b)
        {
            if (b != 0)
                return a / b;
            else
                throw new DivideByZeroException("Don't make me crazy :) :)");
        }
    }
}
