namespace WeatherPrediction.Domain.Base.Interfaces.Domain
{
    public interface IEntityBase
    {
        void ThrowExcpetionIfUpdateInvalid();
        void ThrowExcpetionIfInsertInvalid();
    }
}
