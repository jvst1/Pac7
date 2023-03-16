using Microsoft.Data.SqlClient;
using System.Dynamic;
using System.Reflection;
using WeatherPrediction.Domain.Base.Interfaces.Data;

namespace WeatherPrediction.Data.Queries
{
    public abstract class QueryBase<T> : IQuery<T, SqlParameter>
    {
        private List<string> _sqlWhereList { get; set; }
        private List<string> _sqlGroupBy { get; set; }
        private List<string> _sqlOrderby { get; set; }

        protected QueryBase()
        {
            _sqlWhereList = new List<string>();
            _sqlGroupBy = new List<string>();
            _sqlOrderby = new List<string>();
        }

        public abstract string GetSql();

        public object BuildObject()
        {
            var props = GetParameterProperties();

            var x = new ExpandoObject() as IDictionary<string, object>;

            foreach (var prop in props)
            {
                x.Add(prop.Name, prop.GetValue(this));
            }

            return x;
        }

        public SqlParameter[] BuildParameterList()
        {
            var props = GetParameterProperties();

            return props.Where(p => p.GetValue(this) != null).Select(p => new SqlParameter(p.Name, p.GetValue(this))).ToArray();
        }

        public string GetWhere()
        {
            if (_sqlWhereList != null && _sqlWhereList.Any())
                return " WHERE " + string.Join(" AND \n ", _sqlWhereList);
            return "";
        }

        public void AddCondition(string condition)
        {
            if (_sqlWhereList == null)
                _sqlWhereList = new List<string>();
            if (!string.IsNullOrEmpty(condition))
                _sqlWhereList.Add(condition);
        }

        public void AddGroupBy(string groupBy)
        {
            if (_sqlGroupBy == null)
                _sqlGroupBy = new List<string>();
            if (!string.IsNullOrEmpty(groupBy))
                _sqlGroupBy.Add(groupBy);
        }

        public void AddOrderBy(string orderby)
        {
            if (_sqlOrderby == null)
                _sqlOrderby = new List<string>();
            if (!string.IsNullOrEmpty(orderby))
                _sqlOrderby.Add(orderby);
        }

        public string GetGroupBy()
        {
            if (_sqlGroupBy != null && _sqlGroupBy.Any())
                return " GROUP BY " + string.Join(" , \n ", _sqlGroupBy);
            return "";
        }

        public string GetOrderBy()
        {
            if (_sqlOrderby != null && _sqlOrderby.Any())
                return " ORDER BY " + string.Join(" , \n ", _sqlOrderby);
            return "";
        }

        public string GetSearch(string value, params string[] fields)
        {
            if (!string.IsNullOrEmpty(value) && fields != null && fields.Any())
                return "(" + string.Join(" + ' ' +", fields) + $") COLLATE Latin1_general_CI_AI Like '%{value}%' COLLATE Latin1_general_CI_AI";
            return "";
        }

        public string IsNull(string value, bool cast = false, bool isDate = false, int convertDateFormat = 103)
        {
            if (cast)
            {
                if (isDate)
                    return "ISNULL(CONVERT(VARCHAR(20), " + isDate + ", " + convertDateFormat + "), '') ";

                return "ISNULL(CAST(" + value + " AS VARCHAR), '')";
            }
            else
                return "ISNULL(" + value + ", '')";
        }

        private List<PropertyInfo> GetParameterProperties()
        {
            var properties = GetType().GetProperties()
                .Where(p => p.Name != nameof(_sqlWhereList) && p.Name != nameof(_sqlGroupBy) && p.Name != nameof(_sqlOrderby)).ToList();

            return properties;
        }

        public string GetPagination(int page, int itemsPerPage, string[] sortBy, bool[] sortDesc)
        {
            if (sortBy != null && sortBy.Length > 0)
            {
                _sqlOrderby = new List<string>();
                for (var i = 0; i < sortBy.Length; i++)
                {
                    string sort = sortBy[i];
                    if (sortDesc != null && sortDesc.Length > i)
                        sort += sortDesc[i] ? " DESC " : "";
                    _sqlOrderby.Add(sort);
                }
            }
            else if (_sqlOrderby.Count == 0)
                _sqlOrderby.Add("1");

            string sql = GetOrderBy();
            if (itemsPerPage > 0)
                sql += $@"
                OFFSET {(page - 1) * itemsPerPage} ROWS 
                FETCH NEXT {itemsPerPage} ROWS ONLY;";
            return sql;
        }
    }
}