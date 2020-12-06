using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace GrafanaAPI.Controllers
{
    [Route("[controller]")]
    public class WeatherController : Controller
    {
        public IActionResult Index()
        {
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
