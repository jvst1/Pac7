using System.Linq.Expressions;
using WeatherPrediction.Domain.Base.Interfaces.Domain;

namespace WeatherPrediction.Domain.Base.Interfaces.Data
{
    public interface IDbMapping<TEntity> : IDbMapping
    {
    }

    public interface IDbMapping : IDbMappingBase
    {
        public string Schema { get; }
        virtual bool SaveTrail => true;

        Expression<Func<IEntity, Guid>> GetCodigo();
    }

    public interface IDbMappingBase
    {
        public string DbService { get; }
    }
}