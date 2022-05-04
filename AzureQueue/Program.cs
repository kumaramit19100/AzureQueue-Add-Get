using System;
using CloudStorageAccount = Microsoft.WindowsAzure.Storage.CloudStorageAccount;
using Microsoft.WindowsAzure.Storage.Queue;

namespace AzureQueue
{
    class Program
    {
        public static string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=akstaccount;AccountKey=gkGSx+sQsRaUtEU9YAl5cxogSchyKpV7qHl8pkJ+rhuA+yiwllI+0C249yF7cRTu6JrAGX5Dr3Uo8i7rGjQqbA==;EndpointSuffix=core.windows.net";
        public static string QueueName = "task-1";
        static void Main(string[] args)
        {
            Console.WriteLine("Select '1' for Add Message\nSelect '2' for Get Message\nSelect '3' for Delete Messgae from Queue");
            int a = Convert.ToInt32(Console.ReadLine());
            if (a == 1)
            {
                Console.WriteLine("You are Selected for Add Message!!!");
                AddMessgae();
            }
            if (a == 2)
            {
                Console.WriteLine("You are Selected for Get Message!!!");
                Console.WriteLine();
                GetMessage();
            }
            if (a == 3)
            {
                Console.WriteLine("You are selected for Delete Message ");
                DeleteMessage();
            }
            Console.ReadKey();
        }
        public static void AddMessgae()
        {
            Console.WriteLine("Write Some message for add in Queue -");
            string msg = Console.ReadLine();
            CloudStorageAccount cls = CloudStorageAccount.Parse(ConnectionString);
            CloudQueueClient queueClient = cls.CreateCloudQueueClient();
            CloudQueue cloudQueue = queueClient.GetQueueReference("task-1");
            CloudQueueMessage queueMessage = new CloudQueueMessage(msg);
            if (queueMessage.AsString.Length > 0)
            {
                cloudQueue.AddMessage(queueMessage);
                Console.WriteLine("Message Add Successfully!!");
            }
            else
            {
                Console.WriteLine("Please write some messgae for add in Queue !");
                Console.Beep(500, 500);
            }
        }
        public static void GetMessage()
        {
            CloudStorageAccount cls = CloudStorageAccount.Parse(ConnectionString);
            CloudQueueClient queueClient = cls.CreateCloudQueueClient();
            CloudQueue cloudQueue = queueClient.GetQueueReference(QueueName);
            CloudQueueMessage queueMessage = cloudQueue.GetMessage();
            if (queueMessage != null)
            {
                Console.WriteLine(queueMessage.AsString);
            }
            else
            {
                Console.WriteLine("Queue is Empty");
            }
        }

        public static void DeleteMessage()
        {
            CloudStorageAccount cls = CloudStorageAccount.Parse(ConnectionString);
            CloudQueueClient queueClient = cls.CreateCloudQueueClient();
            CloudQueue cloudQueue = queueClient.GetQueueReference(QueueName);
            CloudQueueMessage queueMessage = cloudQueue.GetMessage();
            if (queueMessage != null)
            {
                Console.WriteLine("This Message are Deleted : "+queueMessage.AsString);
                cloudQueue.DeleteMessage(queueMessage.Id, queueMessage.PopReceipt);
            }
            else
            {
                Console.WriteLine("Queue is Empty");
            }
        }
    }
}
