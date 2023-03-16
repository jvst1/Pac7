namespace WeatherPrediction.Domain.Base.Interfaces.Data
{
    public interface IQuery<T>
    {
        string GetSql();
        object BuildObject();
        string GetPagination(int page, int itemsPerPage, string[] sortBy, bool[] sortDesc);
    }

    public interface IQuery<T, TParameter> : IQuery<T>
    {
        TParameter[] BuildParameterList();
    }
}