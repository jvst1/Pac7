using System.Data;
using WeatherPrediction.Domain.Base.Interfaces.Data;
using WeatherPrediction.Domain.Base.Interfaces.Domain;
using WeatherPrediction.Domain.Base.Interfaces.Repositories;
using WeatherPrediction.Infrastructure.Enums;

namespace WeatherPrediction.Data.Repositories.Base
{
    public abstract class DapperCrudRepositoryBase<TEntity> : ICrudRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly IDbService Context;

        public DapperCrudRepositoryBase(IDbProvider dbProvider)
        {
            var dbService = dbProvider.GetDbService<TEntity>(OrmEngine.Dapper);

            Context = dbService;
        }

        #region Public Methods

        public virtual TEntity GetById(Guid id, bool nolock = false)
        {
            return Context.GetById<TEntity>(id, nolock);
        }

        public virtual void Insert(TEntity entity)
        {
            Context.Insert(entity);
        }

        public virtual void BulkInsert(IEnumerable<TEntity> entities)
        {
            Context.BulkInsert(entities);
        }

        public void BulkUpdate(IEnumerable<TEntity> entities)
        {
            Context.BulkUpdate(entities);
        }

        public virtual void BulkDelete(IEnumerable<TEntity> entities)
        {
            Context.BulkDelete(entities);
        }

        public virtual void BulkDelete(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            Context.BulkDelete<TEntity>(whereConditions, transaction, commandTimeout);
        }

        public virtual void BulkDelete(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            Context.BulkDelete<TEntity>(conditions, parameters, transaction, commandTimeout);
        }

        public virtual void Update(TEntity entity)
        {
            Context.Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            Context.Remove(entity);
        }

        public virtual int Count()
        {
            return Context.Count<TEntity>();
        }

        public virtual int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Context?.Dispose();
        }
    }
}