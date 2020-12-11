using GrafanaAPI.Application.Core.Interfaces.Scrapers;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;

namespace GrafanaAPI.Controllers
{
    [Route("[controller]")]
    public class WeatherController : Controller
    {
        private readonly IWeatherService _forecaWeatherScraper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="forecaWeatherScraper"></param>
        public WeatherController(IWeatherService forecaWeatherScraper)
        {
            _forecaWeatherScraper = forecaWeatherScraper;
        }

        public async Task<IActionResult> Index()
        {
            await _forecaWeatherScraper.GetCurrentWeatherForecastAsync("Herttoniemenranta");

            var html = @"<center> <img src=""http://grafana.org/assets/img/logo_new_transparent_200x48.png"" /> </center><p>Terveisiä APIsta</p>";
            var contentType = "text/xml";
            var content = html;
            var bytes = Encoding.UTF8.GetBytes(content);
            var result = new FileContentResult(bytes, contentType);
            result.FileDownloadName = "data.xml";

            return result;
        }
    }
}
