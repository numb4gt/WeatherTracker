using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeatherTracker.BackEnd.Model;
using Newtonsoft.Json;
using System.IO;
using WeatherTracker.BackEnd.StreamEditors;

namespace WeatherTracker.BackEnd.Data
{
    public class DataEditor
    {
        public static string pattern = @"^\$[0-9]{2}(\.[0-9]{2})?,[0-9]{3}(\.[0-9]{2})?\r\n$";
        public void GetStatusAndSerialize(string data)
        {
            if (IsInputValid(data))
            {
                string[] parts = data.Split(',');

                string windSpeedStr = parts[0].Replace("$", "").Trim();
                string windDirectionStr = parts[1].Replace("$", "").Trim();

                if (double.TryParse(windSpeedStr, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double windSpeed) &&
                    double.TryParse(windDirectionStr, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double windDirection))
                {
                    WeatherStatusViewModel weatherStatus = new WeatherStatusViewModel(windSpeed, windDirection);

                    string json = JsonConvert.SerializeObject(weatherStatus);
                    Console.WriteLine(json);

                    StreamEditor editor = new StreamEditor("weather.json");
                    editor.AddToFile(json);
                }
                else
                {
                    throw new FormatException("Incorrect Format of Numbers");
                }
            
            }
            else
            {
                throw new FormatException("Incorrect Format of Massage");
            }
        }
        static bool IsInputValid(string input)
        {
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }
    }
}
