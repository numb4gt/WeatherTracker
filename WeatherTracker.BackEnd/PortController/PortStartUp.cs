using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTracker.BackEnd.PortController
{
    public static class PortStartUp
    {
        private const string portName = "COM10";

        public static async Task StartPort()
        {
            var port1 = new SerialPortController();
            port1.Open(portName);

            Console.ReadLine();
        }
    }
}
