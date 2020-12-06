using Microsoft.AspNetCore.Mvc;

namespace GrafanaAPI.Controllers
{
    [Route("[controller]")]
    public class WeatherController : Controller
    {
        public IActionResult Index()
        {
            var html = @"<center> img src = ""http://grafana.org/assets/img/logo_new_transparent_200x48.png"" /> </center><p>Terveisiä APIsta</p>";
            return new OkObjectResult(html);
        }
    }
}
