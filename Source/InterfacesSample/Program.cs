using System;

namespace InterfacesSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Intrfaces!");

            double[] data = new double[] {1.2, 3.4, 5.6, 7.8, 3.5 };

            // IMyAlgorithm alg = new MultiplicationAlgorithm();                     
            //IMyAlgorithm alg = new SumAlgorithm();
            IMyAlgorithm alg = new DeepLearning();

            alg.Train(data);

            var result = alg.GetResult();

            Console.WriteLine($"{alg.GetType().Name} result: {result}");
        }
    }
}
