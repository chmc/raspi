using System;

namespace GrafanaAPI.Application.Core.Models.Entity
{
    public class WeatherData
    {
        public DateTimeOffset Date { get; set; }
        public double Temp { get; set; }
        public double RainProbability { get; set; }
        public double WindSpeed { get; set; }
        public int WindDeg { get; set; }
        public string Icon { get; set; }
    }
}
