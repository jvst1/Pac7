using Dapper;
using Microsoft.Extensions.Configuration;
using WeatherPrediction.Domain.Base.Interfaces.Data;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using WeatherPrediction.Domain.Base;

namespace WeatherPrediction.Data.Context.Dapper
{
    public class DapperDbServiceBase : IDbService, IDisposable
    {
        private readonly string _connString;

        protected DapperDbServiceBase(IConfiguration configuration, string identificador)
        {
            _connString = configuration.GetConnectionString(identificador);
        }

        public IQueryable<TEntity> GetAll<TEntity>(bool nolock = false) where TEntity : class
        {
            using var db = new SqlConnection(_connString);

            return db.GetList<TEntity>().AsQueryable();
        }

        public TEntity GetById<TEntity>(Guid id, bool nolock = false) where TEntity : class
        {
            using var db = new SqlConnection(_connString);

            return db.Get<TEntity>(id);
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            using var db = new SqlConnection(_connString);

            db.Insert(entity);
        }

        public void BulkInsert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            using var db = new SqlConnection(_connString);

            db.BulkInsert(entities);
        }

        public void BulkUpdate<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            using var db = new SqlConnection(_connString);

            db.BulkUpdate(entities);
        }

        public void BulkDelete<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            using var db = new SqlConnection(_connString);

            foreach (var entity in entities)
            {
                db.Delete(entity);
            }
        }

        public void BulkDelete<TEntity>(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            using var db = new SqlConnection(_connString);

            db.DeleteList<TEntity>(whereConditions, transaction, commandTimeout);
        }

        public void BulkDelete<TEntity>(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            using var db = new SqlConnection(_connString);

            db.DeleteList<TEntity>(conditions, parameters, transaction, commandTimeout);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            using var db = new SqlConnection(_connString);

            db.Update(entity);
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            using var db = new SqlConnection(_connString);

            db.Delete(entity);
        }

        public IQueryable<TResult> RawQuery<TResult>(IQuery<TResult> query) where TResult : class
        {
            using var db = new SqlConnection(_connString);

            var sql = query.GetSql();
            var paramsObj = query.BuildObject();

            return db.Query<TResult>(sql, paramsObj, commandTimeout: 300).AsQueryable();
        }

        public int RawCommand(string command, params object[] parameters)
        {
            using var db = new SqlConnection(_connString);

            if (parameters.Length > 1)
                throw new ValidationException("Informe apenas um parâmetro. ");

            return parameters.Any() ? db.Execute(command, parameters[0]) : db.Execute(command);
        }

        public IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> filter, bool nolock = false) where TEntity : class
        {
            throw new NotSupportedException();
        }

        public TEntity SingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> filter, bool nolock = false) where TEntity : class
        {
            throw new NotSupportedException();
        }

        public bool HasAny<TEntity>(Expression<Func<TEntity, bool>> filter, bool nolock = false) where TEntity : class
        {
            throw new NotSupportedException();
        }

        public int Count<TEntity>() where TEntity : class
        {
            using var db = new SqlConnection(_connString);

            return db.RecordCount<TEntity>();
        }

        public int SaveChanges()
        {
            return 0;
        }

        void IDbService.Dispose()
        {
        }

        void IDisposable.Dispose()
        {
        }

        public IRawQueryResponsePaged<TResult> RawQuery<TResult>(IQuery<TResult> query, int page, int itemsPerPage, string[] sortBy, bool[] sortDesc) where TResult : class
        {
            using var db = new SqlConnection(_connString);

            var sql = query.GetSql();
            sql += query.GetPagination(page, itemsPerPage, sortBy, sortDesc);
            var paramsObj = query.BuildObject();

            var result = db.QueryMultiple(sql, paramsObj);
            int count = result.Read<int>().First();
            IQueryable<TResult> data = result.Read<TResult>().AsQueryable();

            return new IRawQueryResponsePaged<TResult> { Count = count, Data = data };
        }

        public IQueryable<TResult> CustomRawQuery<TResult>(IQuery<TResult> query) where TResult : class
        {
            using var db = new SqlConnection(_connString);

            var sql = query.GetSql();
            var paramsObj = query.BuildObject();

            return db.Query<TResult>(sql, paramsObj, commandTimeout: 300).AsQueryable();
        }
    }
}