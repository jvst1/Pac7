using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WeatherPrediction.Crosscutting.Mapping;
using WeatherPrediction.Domain.Base.Interfaces.Data;
using WeatherPrediction.Data;
using WeatherPrediction.Application.ApplicationServices;
using WeatherPrediction.Domain.Services.Weather;
using WeatherPrediction.Data.Repositories.Weather;
using WeatherPrediction.Domain.Repositories;

namespace WeatherPrediction.Crosscutting.DI
{
    public static class ApiDiConfig
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            ConfigureHttpContexts(services);

            ConfigureScoped(services);

            ConfigureTransientBase(services);

            ConfigureApplicationServices(services);

            ConfigureDomainServices(services);

            ConfigureServices(services);

            ConfigureRepositories(services);

            ConfigureMiddlewares(services);
        }

        private static void ConfigureHttpContexts(IServiceCollection services)
        {
            services.AddSingleton(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        private static void ConfigureScoped(IServiceCollection services)
        {
        }

        private static void ConfigureTransientBase(IServiceCollection services)
        {
            services.AddTransient(typeof(IMappingService<>), typeof(AutoMapperBase<>));
            services.AddTransient<IDbProvider, DbProvider>();
        }

        private static void ConfigureApplicationServices(IServiceCollection services)
        {
			services.AddTransient<WeatherApplicationService>();		 									
        }

        private static void ConfigureDomainServices(IServiceCollection services)
        {
			services.AddTransient<WeatherDomainService>();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
        }

        private static void ConfigureMiddlewares(IServiceCollection services)
        {
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
			services.AddTransient<IWeatherRepository, WeatherRepository>();
        }
    }
}
