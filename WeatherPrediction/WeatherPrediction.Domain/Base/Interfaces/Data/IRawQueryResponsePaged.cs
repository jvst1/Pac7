namespace WeatherPrediction.Domain.Base.Interfaces.Data
{
    public class IRawQueryResponsePaged<T>
    {
        public int Count { get; set; }
        public IQueryable<T> Data { get; set; }
    }
}
