using AutoMapper;
using WeatherPrediction.Domain.Entities;
using WeatherPrediction.Domain.Messaging.Api.Weather.Response;
using WeatherPrediction.Infrastructure.Extensions;

namespace WeatherPrediction.Crosscutting.Mapping
{
    public static class AutoMapperConfig
    {
        public static Action<IMapperConfigurationExpression> GetAllMappings()
        {
            return cfg =>
            {
                #region Weather

                cfg.CreateMap<Weather, WeatherResponse>();
                cfg.CreateMap<WeatherResponse, Weather>();

                #endregion


                cfg.IgnoreUnmapped();
            };
        }
    }
}