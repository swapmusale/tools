// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Swap tech">
//   Swap tech
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AzureIOTDevice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Azure.Devices;
    using Microsoft.Azure.Devices.Common.Exceptions;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {

        public static RegistryManager ManagerreRegistry { get; set; }

        public static string ConnectionString => "HostName=SwapTest.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=jCUrLFpSlho9ZREf63dQU7PvMOTi1gENeUd8dqhzd1Y=";

        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            ManagerreRegistry=RegistryManager.CreateFromConnectionString(ConnectionString);
            AddDeviceAsync().Wait();
            Console.ReadLine();
        }

        public static async Task AddDeviceAsync()
        {
            string deviceId = "myFirstDevice";
            Task<Device> device;
            try
            {
                device = ManagerreRegistry.AddDeviceAsync(new Device(deviceId));
            }
            catch (DeviceAlreadyExistsException)
            {

                device = ManagerreRegistry.GetDeviceAsync(deviceId);
            }
            Console.WriteLine("Generated device key: {0}", device.Result.Authentication.SymmetricKey.PrimaryKey);
        }
    }
}
