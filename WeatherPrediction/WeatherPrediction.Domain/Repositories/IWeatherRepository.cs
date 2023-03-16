using WeatherPrediction.Domain.Base.Interfaces.Data;
using WeatherPrediction.Domain.Base.Interfaces.Repositories;
using WeatherPrediction.Domain.Entities;

namespace WeatherPrediction.Domain.Repositories
{
    public interface IWeatherRepository : ICrudRepository<Weather>
    {
        public List<Weather> GetAll();
    }
}
