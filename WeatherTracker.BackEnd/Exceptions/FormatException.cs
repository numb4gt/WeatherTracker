using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace WeatherTracker.BackEnd.Exceptions
{
    public class FormatException : Exception
    {
        public FormatException(string message) : base(message) { }
    }
}
