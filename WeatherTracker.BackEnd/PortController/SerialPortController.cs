using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherTracker.BackEnd.Data;

namespace WeatherTracker.BackEnd.PortController
{
    public class SerialPortController : SerialPort
    {
        private const int DataSize = 15; 
        private readonly byte[] buffer = new byte[DataSize];
        private int stepIndex;
        private bool startRead;
        private byte endByte = 10;
        private byte startByte = 36;
        public DataEditor dataEditor = new DataEditor();

        public SerialPortController() : base()
        {
            base.BaudRate = 2800;
            base.DataBits = 8;
            base.StopBits = StopBits.One;
            base.ReadTimeout = 10000;
            base.DataReceived += SerialPort_DataReceived;
        }

        public void Open(string portName)
        {
            if (base.IsOpen)
            {
                base.Close();
            }
            base.PortName = portName;
            base.Open();
        }

        public void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var port = (SerialPort)sender;
            try
            {
                int buferSize = port.BytesToRead;

                for (int i = 0; i < buferSize; ++i)
                {
                    byte bt = (byte)port.ReadByte();

                    if (startByte == bt)
                    {
                        stepIndex = 0;
                        startRead = true;
                    }
                    if (startRead)
                    {
                        buffer[stepIndex] = bt;
                        ++stepIndex;
                    }
                    if (stepIndex == DataSize && startRead)
                    {
                        if (buffer[stepIndex - 1] == endByte) 
                        {
                            string data = Encoding.ASCII.GetString(buffer);
                            dataEditor.GetStatusAndSerialize(data);
                            startRead = false;
                        }
                        else
                        {
                            throw new FormatException("Incorrect Format");
                        }
                        
                    }
                }

            }
            catch (Exception ex) 
            {
                Console.WriteLine("Problems with Message Reading" + ex);
            }
        }
    }
}