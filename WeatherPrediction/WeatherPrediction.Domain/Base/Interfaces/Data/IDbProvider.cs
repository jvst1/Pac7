using WeatherPrediction.Infrastructure.Enums;

namespace WeatherPrediction.Domain.Base.Interfaces.Data
{
    public interface IDbProvider
    {
        IDbService GetDbService(Type type, OrmEngine ormEngine);
        IDbService GetDbService<Type>(OrmEngine ormEngine);
    }
}