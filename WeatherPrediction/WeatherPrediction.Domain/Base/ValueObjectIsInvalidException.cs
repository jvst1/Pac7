namespace WeatherPrediction.Domain.Base
{
    public class ValueObjectIsInvalidException : Exception
    {
        public ValueObjectIsInvalidException(string message) : base(message)
        {
        }
    }
}