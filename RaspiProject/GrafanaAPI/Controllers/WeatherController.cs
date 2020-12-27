using GrafanaAPI.Application.Core.Interfaces.Scrapers;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;

namespace GrafanaAPI.Controllers
{
    [Route("[controller]")]
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="weatherService"></param>
        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        /// <summary>
        /// Return forecast html in xml file
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var html = await _weatherService.GetCurrentWeatherForecastAsync("Herttoniemenranta");

            var contentType = "text/xml";
            var content = html;
            var bytes = Encoding.UTF8.GetBytes(content);
            var result = new FileContentResult(bytes, contentType);
            result.FileDownloadName = "data.xml";

            return result;
        }
    }
}
