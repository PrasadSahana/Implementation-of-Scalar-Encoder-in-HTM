using MyCloudProject.Common;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Threading;
using MyExperiment;

namespace MyCloudProject
{
    class Program
    {
        /// <summary>
        /// Your project ID from the last semester.
        /// </summary>
        private static string projectName = "ML19/20-5.8";

        
        static void Main(string[] args)
        {
            CancellationTokenSource tokeSrc = new CancellationTokenSource();

            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                tokeSrc.Cancel();
            };

            Console.WriteLine($"Started experiment: {projectName}");

            //init configuration
            var cfgRoot = Common.InitHelpers.InitConfiguration(args);

            var cfgSec = cfgRoot.GetSection("MyConfig");

            MyConfig cfg = new MyConfig();
            cfgSec.Bind(cfg);

            // InitLogging
            var logFactory = InitHelpers.InitLogging(cfgRoot);
            var logger = logFactory.CreateLogger("Train.Console");

            logger?.LogInformation($"{DateTime.Now} -  Started experiment: {projectName}");

            Experiment experiment = new Experiment(/* put some additional config here */);
            IStorageProvider storageProvider = new AzureStorageProvider(cfg);

            InitHelpers.RunQueueListener(experiment, storageProvider, cfg, tokeSrc.Token, logFactory.CreateLogger("QueueListener")).Wait();

            logger?.LogInformation($"{DateTime.Now} -  Experiment exit: {projectName}");
        }


    }
}
