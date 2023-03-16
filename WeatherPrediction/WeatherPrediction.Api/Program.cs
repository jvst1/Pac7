using WeatherPrediction.Crosscutting.DI;
using WeatherPrediction.Infrastructure.Extensions;
using WeatherPrediction.Infrastructure.Helpers;

namespace WeatherPrediction.Api
{
    public class Program
    {
        public static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();
        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
            .ConfigureAppConfiguration((hostContext, config) =>
            {
                config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true)
                .AddJsonFile($"appsettingsBase.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddAppSettings(hostContext.Configuration);

                EnvironmentNameHelper.Value = hostContext.Configuration.GetSection("EnvironmentName").Value;

                ApiDiConfig.AddDependencyInjection(services);
            });
    }
}