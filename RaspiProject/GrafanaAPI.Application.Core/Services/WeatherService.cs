﻿using GrafanaAPI.Application.Core.Interfaces.Scrapers;
using GrafanaAPI.Application.Core.Models.Dto;
using GrafanaAPI.Application.Core.Models.Entity;
using GrafanaAPI.Application.Core.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public async Task GetCurrentWeatherForecastAsync(string city)
        {
            // Daily forecast for 7 days
            // https://openweathermap.org/api/one-call-api
            // https://api.openweathermap.org/data/2.5/onecall?lat=60.1695&lon=24.9355&exclude=current,minutely,hourly,alerts&appid=32ccf0c3b90b5e7ef5c93125af02611e

            // Convert unix time to datetime
            // DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(1000);

            var url = $"https://api.openweathermap.org/data/2.5/onecall?lat=60.1695&lon=24.9355&exclude=current,minutely,alerts&units=metric&appid={_options.Value.OpenWeatherMapApiKey}";
            var json = await _httpClient.GetStringAsync(url);
            var response = JsonConvert.DeserializeObject<WeatherDataDto>(json);

            var dailyData = response.Daily[1];
            var dailyIconUrl = string.Format(_options.Value.IconUrl, dailyData.Weather[0].Icon);
            var dailyTemperature = dailyData.Temp;
            var dailyDate = dailyData.Date;
            var dailyRainProbability = dailyData.Pop;

            var hourdata = response.Hourly[0];
            var hourIconUrl = string.Format(_options.Value.IconUrl, hourdata.Weather[0].Icon);
            var hourTemperature = hourdata.Temp;
            var hourDate = hourdata.Date;
            var hourRainProbability = hourdata.Pop;

            // Get daily forecast
            var daily = new List<WeatherData>();
            foreach (var data in response.Daily)
            {
                daily.Add(new WeatherData
                {
                    Date = data.Date,
                    Icon = string.Format(_options.Value.IconUrl, data.Weather[0].Icon),
                    Temp = data.Temp.Day,
                    RainProbability = data.Pop,
                    WindSpeed = data.WindSpeed,
                    WindDeg = data.WindDeg
                });
            }

            // Get hourly forecast
            var hourly = new List<WeatherData>();
            foreach (var data in response.Hourly)
            {
                hourly.Add(new WeatherData
                {
                    Date = data.Date,
                    Icon = string.Format(_options.Value.IconUrl, data.Weather[0].Icon),
                    Temp = data.Temp,
                    RainProbability = data.Pop,
                    WindSpeed = data.WindSpeed,
                    WindDeg = data.WindDeg
                });
            }

            var html = "<center>";
            foreach (var wd in daily)
            {
                html += @$"<div style=""display: inline-block;""><span>{wd.Temp}</span><br><img src=""{wd.Icon}"" /><br><span>{wd.Date.DayOfWeek}</span></div>";
            }
            html += "</center>";

            var i = 0;
        }

        ///// <summary>
        ///// Get current weather for given city
        ///// </summary>
        ///// <param name="city"></param>
        ///// <returns></returns>
        //public async Task GetCurrentWeatherForecastAsync(string city)
        //{
        //    // Daily forecast for 7 days
        //    // https://openweathermap.org/api/one-call-api
        //    // https://api.openweathermap.org/data/2.5/onecall?lat=60.1695&lon=24.9355&exclude=current,minutely,hourly,alerts&appid=32ccf0c3b90b5e7ef5c93125af02611e

        //    // Convert unix time to datetime
        //    // DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(1000);

        //    var json = await _httpClient.GetStringAsync($"https://api.openweathermap.org/data/2.5/forecast?q=Helsinki&appid={_options.Value.OpenWeatherMapApiKey}&units=metric&lang=fi");
        //    var response = JsonConvert.DeserializeObject<WeatherData3hDto>(json);
        //    var data = response.List[0];
        //    var iconUrl = string.Format(_options.Value.IconUrl, data.Weather[0].Icon);
        //    var temperature = data.Main.Temp;
        //    var date = data.DtTxt;
        //    var rainProbability = data.Pop;

        //    var i = 0;
        //}
    }
}
