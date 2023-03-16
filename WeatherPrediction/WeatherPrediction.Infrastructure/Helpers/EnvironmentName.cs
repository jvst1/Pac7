namespace WeatherPrediction.Infrastructure.Helpers
{
    public static class EnvironmentNameHelper
    {
        public static string Value { get; set; }

        public const string Staging = "Staging";
        public const string Development = "Development";
        public const string Production = "Production";
    }
}