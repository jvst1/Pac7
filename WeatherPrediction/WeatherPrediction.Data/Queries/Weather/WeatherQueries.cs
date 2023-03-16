namespace WeatherPrediction.Data.Queries.Weather
{
    public static class WeatherQueries
    {
        public class GetAll : QueryBase<Domain.Entities.Weather>
		{
			public string? Search { get; }

			public GetAll(string search = null)
			{
				Search = search;
			}

			public override string GetSql()
			{
				var sql = @"SELECT * FROM dbo.Weather (NOLOCK)";

				if (!string.IsNullOrEmpty(Search))
					AddCondition(GetSearch(Search, "Name"));

				var rawSql = sql + GetWhere();

				return rawSql;
			}
		}
	}
}
