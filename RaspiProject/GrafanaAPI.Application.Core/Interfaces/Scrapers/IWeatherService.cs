using System.Threading.Tasks;

namespace GrafanaAPI.Application.Core.Interfaces.Scrapers
{
    public interface IWeatherService
    {
        /// <summary>
        /// Get current weather for given city
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        Task GetCurrentWeatherForecastAsync(string city);
    }
}
