using GrafanaAPI.Application.Core.Interfaces.Scrapers;
using GrafanaAPI.Application.Core.Models.Dto;
using GrafanaAPI.Application.Core.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
            var json = await _httpClient.GetStringAsync($"https://api.openweathermap.org/data/2.5/forecast?q=Helsinki&appid={_options.Value.OpenWeatherMapApiKey}&units=metric&lang=fi");
            var data = JsonConvert.DeserializeObject<WeatherDataDto>(json);
            var i = 0;
        }
    }
}
