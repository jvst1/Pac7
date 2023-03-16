namespace WeatherPrediction.Infrastructure.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetInnermostExceptionMessage(this Exception ex)
        {
            return ex.InnerException != null ? ex.InnerException.GetInnermostExceptionMessage() : ex.Message;
        }
    }
}