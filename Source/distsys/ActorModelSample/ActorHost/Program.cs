using AkkaSb.Net;
using Microsoft.Extensions.Logging;
using SharedLibrary;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ActorModelSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Actor Host :)");

            LoggerFactory factory = new LoggerFactory();

            factory.AddConsole(LogLevel.Information);
            factory.AddDebug(LogLevel.Information);

            ActorSystem sysRemote = new ActorSystem($"actorhost", Helpers.GetHostConfig(), factory.CreateLogger("host"));

            CancellationTokenSource tokenSource = new CancellationTokenSource();

            var task = Task.Run(() =>
            {
                sysRemote.Start(tokenSource.Token);
            });

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                tokenSource.Cancel();
            };
            
            task.Wait();
        }
    }
}
