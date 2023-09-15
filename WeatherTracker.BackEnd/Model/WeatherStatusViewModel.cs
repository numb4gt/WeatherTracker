using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTracker.BackEnd.Model
{
    [Serializable]
    public class WeatherStatusViewModel
    {
        public double WindSpeed { get; set; }
        public double WindDirection { get; set; }
        public string SensorName { get; set; }
        public DateTime DateTime { get; set; }

        public WeatherStatusViewModel(double windSpeed, double windDirection) { 
            WindSpeed = windSpeed;
            WindDirection = windDirection;
            SensorName = "Vaisala WMT700";
            DateTime = DateTime.Now;
        }
    }
}
