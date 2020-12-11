using GrafanaAPI.Application.Core.Interfaces.Scrapers;
using GrafanaAPI.Application.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GrafanaAPI.Infrastructure.IoC
{
    public class DependencyContainer
    {
        /// <summary>
        /// Configure dependencies
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IWeatherService, WeatherService>();

            return services;
        }
    }
}
