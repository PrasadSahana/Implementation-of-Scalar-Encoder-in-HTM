using MyFirstProject;
using System;

namespace MyFirstApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Calculator!");

            Calculator calc = new Calculator();
            
            var result = calc.Add(1.2, 4564.4);

            Console.WriteLine(result);

            Console.ReadLine();

        }
    }
}
