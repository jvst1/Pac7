using WeatherPrediction.Data.Queries.Weather;
using WeatherPrediction.Data.Repositories.Base;
using WeatherPrediction.Domain.Base.Interfaces.Data;
using WeatherPrediction.Domain.Repositories;

namespace WeatherPrediction.Data.Repositories.Weather
{
    public class WeatherRepository : DapperCrudRepositoryBase<Domain.Entities.Weather>, IWeatherRepository
    {
        public WeatherRepository(IDbProvider dbProvider) : base(dbProvider)
        {
        }

        public List<Domain.Entities.Weather> GetAll()
        {
            return Context.GetAll<Domain.Entities.Weather>().ToList();
        }
    }
}
