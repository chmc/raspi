using GrafanaAPI.Application.Core.Interfaces.Scrapers;
using GrafanaAPI.Application.Core.Models.Dto;
using GrafanaAPI.Application.Core.Models.Entity;
using GrafanaAPI.Application.Core.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GrafanaAPI.Application.Core.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IOptionsSnapshot<ApiOptions> _options;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        public WeatherService(IOptionsSnapshot<ApiOptions> options, HttpClient httpClient)
        {
            _options = options;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Get current weather for given city
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public async Task<string> GetCurrentWeatherForecastAsync(string city)
        {
            // Daily forecast for 7 days
            // https://openweathermap.org/api/one-call-api
            // https://api.openweathermap.org/data/2.5/onecall?lat=60.1695&lon=24.9355&exclude=current,minutely,hourly,alerts&appid=32ccf0c3b90b5e7ef5c93125af02611e

            var url = $"https://api.openweathermap.org/data/2.5/onecall?lat=60.1695&lon=24.9355&exclude=current,minutely,alerts&units=metric&appid={_options.Value.OpenWeatherMapApiKey}";
            var json = await _httpClient.GetStringAsync(url);
            var response = JsonConvert.DeserializeObject<WeatherDataDto>(json);

            // Get hourly forecast
            var hourlyList = new List<WeatherData>();
            foreach (var data in response.Hourly.Take(8))
            {
                hourlyList.Add(new WeatherData
                {
                    Date = data.Date.AddHours(_options.Value.UtcOffset),
                    Icon = string.Format(_options.Value.IconUrl, data.Weather[0].Icon),
                    Temp = Math.Round(data.Temp),
                    RainProbability = Math.Round(data.Pop * 100),
                    WindSpeed = data.WindSpeed,
                    WindDeg = data.WindDeg
                });
            }

            // Get daily forecast
            var dailyList = new List<WeatherData>();
            foreach (var data in response.Daily)
            {
                dailyList.Add(new WeatherData
                {
                    Date = data.Date.AddHours(_options.Value.UtcOffset),
                    Icon = string.Format(_options.Value.IconUrl, data.Weather[0].Icon),
                    Temp = Math.Round(data.Temp.Day),
                    TempNight = Math.Round(data.Temp.Night),
                    RainProbability = Math.Round(data.Pop * 100),
                    WindSpeed = data.WindSpeed,
                    WindDeg = data.WindDeg
                });
            }

            var html = @"<center style=""background-color: #000; color: #fff; font-family: Arial, Helvetica, sans-serif;""><table>";
            // 7h forecast
            html += "<tr>";
            foreach (var wd in hourlyList)
            {
                html += @$"<td><div><b><center>{wd.Date.Hour}: {wd.Temp}&deg; {wd.RainProbability}%</center></b></div><div><center><img src=""{wd.Icon}"" /></center></div></td>";
            }
            html += "</tr>";

            // 7 day forecast
            html += "<tr>";
            foreach (var wd in dailyList)
            {
                html += @$"<td><div><center><img src=""{wd.Icon}"" /></center></div><div><b><center>{wd.Temp}&deg; / {wd.TempNight}&deg; {wd.RainProbability}%</center></b></div><div><center>{wd.Day}</center></div></td>";
            }
            html += "</tr>";
            html += "</table></center>";

            return html;
        }
    }
}