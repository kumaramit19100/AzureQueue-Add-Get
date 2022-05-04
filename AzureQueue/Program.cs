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
            Console.WriteLine("Select '1' for Add Message or Select '2' for Get Message");
            int a = Convert.ToInt32(Console.ReadLine());
            if (a == 1)
            {
                Console.WriteLine("You Have Selected for Add Message!!!");
                AddMessgae();
            }
            if (a == 2)
            {
                Console.WriteLine("You Have Selected for Get Message!!!");
                Console.WriteLine();
                GetMessage();
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
    }
}
