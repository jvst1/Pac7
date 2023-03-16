using WeatherPrediction.Domain.Base.Interfaces.Domain;

namespace WeatherPrediction.Domain.Base.Interfaces.Repositories
{
    public interface ICrudRepository<TEntity> : IDisposable where TEntity : IEntityBase
    {
        TEntity GetById(Guid id, bool nolock = false);

        void Insert(TEntity entity);

        void BulkInsert(IEnumerable<TEntity> entity);

        void BulkUpdate(IEnumerable<TEntity> entity);

        void BulkDelete(IEnumerable<TEntity> entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        int Count();

        int SaveChanges();
    }
}