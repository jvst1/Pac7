using WeatherPrediction.Domain.Base.Interfaces.Data;
using WeatherPrediction.Domain.Entities;
using WeatherPrediction.Domain.Interfaces.Application;
using WeatherPrediction.Domain.Messaging.Api.Weather.Response;
using WeatherPrediction.Domain.Services.Weather;

namespace WeatherPrediction.Application.ApplicationServices
{
    public class WeatherApplicationService : IApplicationServiceBase
    {
        private readonly WeatherDomainService _weatherDomainService;
        private readonly IMappingService<Weather> _mapper;

        public WeatherApplicationService(WeatherDomainService weatherDomainService)
        {
            _weatherDomainService = weatherDomainService;
        }

        public List<WeatherResponse> GetAll()
        {
            var entities = _weatherDomainService.GetAll();
            return _mapper.ConvertFromDomain<WeatherResponse>(entities);
        }
    }
}
