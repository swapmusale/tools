// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="swap tech">
//   swap tech
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ReadDeviceToCloudMessages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        public static string ConnectionString => "HostName=SwapTest.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=jCUrLFpSlho9ZREf63dQU7PvMOTi1gENeUd8dqhzd1Y=";

        public static string IotHubD2CEndpoint => "messages/events";

        public static EventHubClient Client { get; set; }
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Receive messages\n");
            Client=EventHubClient.CreateFromConnectionString(ConnectionString,IotHubD2CEndpoint);
            var d2CPartitions = Client.GetRuntimeInformation().PartitionIds;
            foreach (var item in d2CPartitions)
            {
                ReceiveMessagesFromDeviceAsync(item);
            }
            Console.ReadLine();
        }

        private static async Task ReceiveMessagesFromDeviceAsync(string partition)
        {
            var eventHubReceiver = Client.GetDefaultConsumerGroup().CreateReceiver(
                partition,
                DateTime.Now.ToUniversalTime());
            while (true)
            {
                EventData eventData = await eventHubReceiver.ReceiveAsync();
                if(eventData==null) continue;
                string data = Encoding.UTF8.GetString(eventData.GetBytes());
                Console.WriteLine("Message Recived partition: {0} data: {1}", partition, data);
            }

        }
    }
}
