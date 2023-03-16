namespace WeatherPrediction.Domain.Base.Interfaces.Data
{
    public interface IMappingService<TDomain>
    {
        TDomain ConvertToDomain<T>(T type);
        T ConvertFromDomain<T>(TDomain domain);
        TOther ConvertOther<T, TOther>(T domain);
        void UpdateDomain<T>(TDomain destination, T source);
        List<T> ConvertFromDomain<T>(List<TDomain> domains);
        List<TDomain> ConvertToDomain<T>(List<T> types);
        List<TOther> ConvertOther<T, TOther>(List<T> domain);
    }
}