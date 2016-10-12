using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using iBalekaAPI.EventActivationWebJob.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Table;

namespace iBalekaAPI.EventActivationWebJob
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        //public static void ProcessQueueMessage([QueueTrigger("queue")] string message, TextWriter log)
        //{
        //    log.WriteLine(message);
        //}"0 30 9 * * *"
        [NoAutomaticTrigger()]
        public static void ProcessEvents(TextWriter log)
        {
            RetrieveOpenEvents(log);
            RetrieveUpcomingEvents(log);
            ActivateEvents(log);
        }
        public static void RetrieveOpenEvents(TextWriter log)
        {
            List<Event> evnts = new List<Event>();
            using (EventDbContext dbContext = new EventDbContext())
            {
                Console.WriteLine("Getting Open Events from Db...");
                log.WriteLine("Getting Open Events from Db...");
                evnts = dbContext.Event
                          .Where(p => p.Deleted == false && p.EventStatus == EventType.Open)
                          .ToList();
                log.WriteLine("Events Received...");
                Console.WriteLine("Events Received...");
            }

            if (evnts != null)
            {
                log.WriteLine("Saving Open Events to Azure Table...");
                Console.WriteLine("Saving Open Events to Azure Table...");
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureWebJobsStorage"));
                // Create the table client.
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                // Retrieve a reference to the table.                
                CloudTable table = tableClient.GetTableReference("OpenEvents");
                // Create the table if it doesn't exist.
                table.CreateIfNotExists();

                List<EventEntity> openEventEntities = evnts.ToEntities();
                TableQuery<EventEntity> query = new TableQuery<EventEntity>();
                // Create the batch operation.
                TableBatchOperation batchOperation = new TableBatchOperation();
                // Print the fields for each customer.
                IEnumerable<EventEntity> azureEntity = table.ExecuteQuery(query);

                foreach (EventEntity evnt in openEventEntities)
                {
                    if (!azureEntity.Contains(evnt))
                        batchOperation.Insert(evnt);
                }
                // Execute the batch operation.
                table.ExecuteBatch(batchOperation);
                log.WriteLine("Open Events saved to Azure Table...");
                Console.WriteLine("Open Events saved to Azure Table...");
            }
            else
            {
                log.WriteLine("No Open Events at this time...");
                Console.WriteLine("No Open Events at this time...");
            }

        }
        public static void RetrieveUpcomingEvents(TextWriter log)
        {
            log.WriteLine("Getting Open Events from Azure Table...");
            Console.WriteLine("Getting Open Events from Azure Table...");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureWebJobsStorage"));
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            // Retrieve a reference to the table.                
            CloudTable table = tableClient.GetTableReference("OpenEvents");
            // Create the table if it doesn't exist.
            table.CreateIfNotExists();
            TableQuery<EventEntity> query = new TableQuery<EventEntity>();
            // Create the batch operation.
            TableBatchOperation batchOperation = new TableBatchOperation();
            // Print the fields for each customer.
            IEnumerable<EventEntity> azureEntity = table.ExecuteQuery(query);

            //get localization info
        }
        public static void ActivateEvents(TextWriter log)
        {

        }
    }
}
