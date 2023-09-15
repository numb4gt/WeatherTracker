using System.IO;
using System.IO.Ports;
using System.Security.Cryptography.X509Certificates;
using WeatherTracker.BackEnd.PortController;

internal class Program
{
    private static void Main(string[] args)
    {
        PortStartUp.StartPort();
    }
}