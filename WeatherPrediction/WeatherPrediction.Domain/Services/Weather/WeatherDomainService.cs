using WeatherPrediction.Domain.Base.Interfaces.Data;
using WeatherPrediction.Domain.Repositories;

namespace WeatherPrediction.Domain.Services.Weather
{
    public class WeatherDomainService
    {
        private readonly IWeatherRepository _weatherRepository;

        public WeatherDomainService(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public List<Entities.Weather> GetAll()
        {
            return _weatherRepository.GetAll();
        }
    }
}
