namespace WeatherPrediction.Domain.Messaging.Api.Weather.Request
{
    internal class WeatherRequest
    {
        public Guid Codigo { get; set; }
        public string Name { get; set; }
    }
}
