using System;

namespace Queue
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Azure Storage Queue Sample\n");

            // Create queue, insert message, peek message, read message, change contents of queued message, 
            //    queue 20 messages, get queue length, read 20 messages, delete queue.
            GettingStarted getStarted = new GettingStarted();
            getStarted.RunQueueStorageOperationsAsync().Wait();

            // Get list of queues in storage account.
            Advanced advMethods = new Advanced();
            advMethods.RunQueueStorageAdvancedOpsAsync().Wait();

            Console.WriteLine("Press any key to exit.");
            Console.Read();
        }


    }
}
