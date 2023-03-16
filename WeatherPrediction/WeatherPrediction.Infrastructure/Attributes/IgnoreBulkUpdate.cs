namespace WeatherPrediction.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreBulkUpdate : Attribute
    {
    }
}