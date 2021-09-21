using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.Logging.Console;
using System.Threading;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace MyCloudProject.Common
{
    public static class InitHelpers
    {
        /// <summary>
        /// Create Logging infrastructure in the Trainer Workload
        /// </summary>
        /// <returns></returns>
        public static ILoggerFactory InitLogging(IConfigurationRoot configRoot)
        {
            //create logger from the appsettings addConsole Debug to Logg 
            return LoggerFactory.Create(logBuilder =>
            {
                ConsoleLoggerOptions logCfg = new ConsoleLoggerOptions();

                logBuilder.AddConfiguration(configRoot.GetSection("Logging"));

                logBuilder.AddConsole((opts) =>
                {
                    opts.IncludeScopes = true;
                }).AddDebug();
            });
        }

        /// <summary>
        /// Look into appconfig.json and initialize configurations for the Training Workload
        /// </summary>
        /// <returns></returns>
        public static IConfigurationRoot InitConfiguration(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("MYCLOUDPROJECT_ENVIRONMENT");

            ConfigurationBuilder builder = new ConfigurationBuilder();
            if (string.IsNullOrEmpty(environmentName))
            {
                builder.AddJsonFile(System.IO.Path.Combine(AppContext.BaseDirectory, "appsettings.json"), false, true);
            }
            else
            {
                builder.AddJsonFile(System.IO.Path.Combine(AppContext.BaseDirectory, $"appsettings.{environmentName}.json"), false, true);
            }

            if (args != null)
                builder.AddCommandLine(args);
            builder.AddEnvironmentVariables();
            var configRoot = builder.Build();

           
            return configRoot;
        }

        /// <summary>
        /// Validate the connection string information in app.config and throws an exception if it looks like 
        /// the user hasn't updated this to valid values. 
        /// </summary>
        /// <param name="storageConnectionString">The storage connection string</param>
        /// <returns>CloudStorageAccount object</returns>
        private static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }


        /// <summary>
        /// Create a queue for the sample application to process messages in. 
        /// </summary>
        /// <returns>A CloudQueue object</returns>
        private static async Task<CloudQueue> CreateQueueAsync(MyConfig config)
        {
            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(config.StorageConnectionString);

            // Create a queue client for interacting with the queue service
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            Console.WriteLine("1. Create a queue for the demo");

            CloudQueue queue = queueClient.GetQueueReference(config.Queue);
            try
            {
                await queue.CreateIfNotExistsAsync();
            }
            catch
            {
                Console.WriteLine("If you are running with the default configuration please make sure you have started the storage emulator.  ess the Windows key and type Azure Storage to select and run it from the list of applications - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return queue;
        }

        public static async Task RunQueueListener(IExperiment experiment, IStorageProvider storageProvider, MyConfig config, CancellationToken cancelToken, ILogger log)
        {
            CloudQueue queue = await CreateQueueAsync(config);

            while (cancelToken.IsCancellationRequested == false)
            {
                CloudQueueMessage message = await queue.GetMessageAsync();
                if (message != null)
                {
                    log?.LogInformation($"Received the message {message.AsString}");

                    // TODO...
                    // ExerimentRequestMessage = deserialize it from msg. See WIKI.
                    ExerimentRequestMessage msg = null;

                    ExperimentResult result = await experiment.Run(msg);

                    await storageProvider.UploadExperimentResult(result);

                    await queue.DeleteMessageAsync(message);
                }
            }

            log?.LogInformation("Cancel pressed. Exiting the listener loop.");
        }
    }
}
