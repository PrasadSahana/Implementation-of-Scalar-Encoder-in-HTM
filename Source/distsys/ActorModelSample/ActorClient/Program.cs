using AkkaSb.Net;
using SharedLibrary;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ActorClient
{
    class Program
    {
        static void Main(string[] args)
        {
            
            long sum = 10000000000;

            Console.WriteLine($"Calculating {sum}");
                      
            Stopwatch sw = new Stopwatch();

            sw.Start();

            //CalculateSumSingleActor(sum);
            CalculateSumMultipleActors(sum);
            sw.Stop();

            Console.WriteLine($"Elapsed time: {sw.ElapsedMilliseconds}");


            Console.ReadLine();
        }      

        public static void CalculateSumSingleActor(long sum)
        {
            var cfg = Helpers.GetClientConfig();

            ActorSystem sysLocal = new AkkaSb.Net.ActorSystem($"{nameof(CalculateSumSingleActor)}/local", cfg);

            ActorReference actorRef1 = sysLocal.CreateActor<SumCalculusActor>(1);
            var res = actorRef1.Ask<double>(new SumMsg() { SumStart = 0, SumEnd = sum }).Result;

            Console.WriteLine(res);
        }

        public static void CalculateSumMultipleActors(long sum)
        {
            var cfg = Helpers.GetClientConfig();

            ActorSystem sysLocal = new AkkaSb.Net.ActorSystem($"{nameof(CalculateSumSingleActor)}/local", cfg);

            Task<double>[] tasks = new Task<double>[2];

            ActorReference actorRef1 = sysLocal.CreateActor<SumCalculusActor>(1);
            tasks[0] = actorRef1.Ask<double>(new SumMsg() { SumStart = 0, SumEnd = sum / 2});

            ActorReference actorRef2 = sysLocal.CreateActor<SumCalculusActor>(2);
            tasks[1] = actorRef2.Ask<double>(new SumMsg() { SumStart = sum/ 2, SumEnd = sum });
            
            Task.WaitAll(tasks);

            Console.WriteLine($"{tasks[0].Result + tasks[0].Result}");
        }
    }
}
