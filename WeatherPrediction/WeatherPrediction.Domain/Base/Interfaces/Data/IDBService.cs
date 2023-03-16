using System.Data;
using System.Linq.Expressions;

namespace WeatherPrediction.Domain.Base.Interfaces.Data
{
    public interface IDbService
    {
        IQueryable<TEntity> GetAll<TEntity>(bool nolock = false) where TEntity : class;

        TEntity GetById<TEntity>(Guid id, bool nolock = false) where TEntity : class;

        void Insert<TEntity>(TEntity entity) where TEntity : class;

        void BulkInsert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void BulkUpdate<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void BulkDelete<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void BulkDelete<TEntity>(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null) where TEntity : class;

        void BulkDelete<TEntity>(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null) where TEntity : class;

        void Update<TEntity>(TEntity entity) where TEntity : class;

        void Remove<TEntity>(TEntity entity) where TEntity : class;

        IQueryable<TResult> RawQuery<TResult>(IQuery<TResult> query) where TResult : class;

        IQueryable<TResult> CustomRawQuery<TResult>(IQuery<TResult> query) where TResult : class;

        IRawQueryResponsePaged<TResult> RawQuery<TResult>(IQuery<TResult> query, int page, int itemsPerPage, string[] sortBy, bool[] sortDesc) where TResult : class;

        int RawCommand(string command, params object[] parameters);

        IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> filter, bool nolock = false) where TEntity : class;

        TEntity SingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> filter, bool nolock = false) where TEntity : class;

        bool HasAny<TEntity>(Expression<Func<TEntity, bool>> filter, bool nolock = false) where TEntity : class;

        int Count<TEntity>() where TEntity : class;

        int SaveChanges();

        void Dispose();
    }
}