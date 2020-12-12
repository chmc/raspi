using System;

namespace GrafanaAPI.Application.Core.Models.Entity
{
    public class WeatherData
    {
        public DateTimeOffset Date { get; set; }
        public string Day { get { return $"{Date.DayOfWeek.ToString().Substring(0, 3)} {Date.Day}"; } }
        public double Temp { get; set; }
        public double? TempNight { get; set; }
        public double RainProbability { get; set; }
        public double WindSpeed { get; set; }
        public int WindDeg { get; set; }
        public string Icon { get; set; }
    }
}
