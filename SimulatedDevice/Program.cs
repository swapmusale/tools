// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="swap tech">
//   swap tech
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SimulatedDevice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Azure.Devices.Client;

    using Newtonsoft.Json;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        public static DeviceClient Client { get; set; }

        /// <summary>
        /// Gets or sets the IOT hub uri.
        /// </summary>
        public static string IotHubUri => "SwapTest.azure-devices.net";

        /// <summary>
        /// Gets or sets the device key.
        /// </summary>
        public static string DeviceKey => "XwXkNeePt9RjZubk7oyfHn4wQAXMkIxuV9wVct4Iy4s=";

        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Simulated device\n");
            Client = DeviceClient.Create(IotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("myFirstDevice", DeviceKey));
            SendDeviceToCloudMessagesAsync();
            Console.ReadLine();
        }

        /// <summary>
        /// The send device to cloud messages async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private static async Task SendDeviceToCloudMessagesAsync()
        {
            double avgWindSpeed = 10;
            Random random = new Random();
            while (true)
            {
                var currentWindSpeed = avgWindSpeed + random.NextDouble() * 4 - 2;

                var telemetryDatapoint = new { deviceId = "myFirstDevice", windSpeed = currentWindSpeed };

                var messageString = JsonConvert.SerializeObject(telemetryDatapoint);

                var message = new Message(Encoding.ASCII.GetBytes(messageString));
                await Client.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);
                Thread.Sleep(1000);
            }
        }

    }
}
