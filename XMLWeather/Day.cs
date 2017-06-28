using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLWeather
{
    public class Day
    {
        public string city, date, condition, currentTemp, maxTemp, minTemp, windSpeed, windDirection, precipitation;

        public Day()
        {
            city = date = condition = currentTemp = maxTemp = minTemp = windSpeed = windDirection = precipitation = "";
        }
    }
}
