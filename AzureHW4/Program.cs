using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure; 
using Microsoft.WindowsAzure.Storage; 
using Microsoft.WindowsAzure.Storage.Queue;
using System.IO; 

namespace AzureHW4
{
    class Program
    {
        static void Main(string[] args)
        {
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a container.
            CloudQueue queue = queueClient.GetQueueReference("myqueue");

            #region Создание очереди
            // Create the queue if it doesn't already exist
            queue.CreateIfNotExists();
            Console.WriteLine("Queue created");
            #endregion
            #region Вставка сообщения в очередь
            // Create a message and add it to the queue.
            CloudQueueMessage message = new CloudQueueMessage("Hello, World");
            queue.AddMessage(message);
            #endregion

            #region Просмотр следующего сообщения
            // Peek at the next message
            CloudQueueMessage peekedMessage = queue.PeekMessage();

            // Display message.
            Console.WriteLine(peekedMessage.AsString);
            #endregion

            //#region Изменение содержимого сообщения в очереди
            //// Get the message from the queue and update the message contents.
            //CloudQueueMessage message = queue.GetMessage();
            //message.SetMessageContent("Updated contents.");
            //queue.UpdateMessage(message,
            //    TimeSpan.FromSeconds(60.0),  // Make it invisible for another 60 seconds.
            //    MessageUpdateFields.Content | MessageUpdateFields.Visibility);
            //#endregion

            //#region Удаление следующего сообщения из очереди
            //// Get the next message
            //CloudQueueMessage retrievedMessage = queue.GetMessage();

            ////Process the message in less than 30 seconds, and then delete the message
            //queue.DeleteMessage(retrievedMessage);
            //#endregion

            //#region Дополнительные параметры для удаления сообщений из очереди
            //foreach (CloudQueueMessage message in queue.GetMessages(20, TimeSpan.FromMinutes(5)))
            //{
            //    // Process all messages in less than 5 minutes, deleting each message after processing.
            //    queue.DeleteMessage(message);
            //}
            //#endregion

            //#region Получение длины очереди
            //// Fetch the queue attributes.
            //queue.FetchAttributes();

            //// Retrieve the cached approximate message count.
            //int? cachedMessageCount = queue.ApproximateMessageCount;

            //// Display number of messages.
            //Console.WriteLine("Number of messages in queue: " + cachedMessageCount);
            //#endregion

            //#region Удаление очереди
            //// Delete the queue.
            //queue.Delete();
            //#endregion
        }
    }
}
