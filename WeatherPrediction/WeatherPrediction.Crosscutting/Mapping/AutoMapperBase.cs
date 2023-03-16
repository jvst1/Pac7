using AutoMapper;
using WeatherPrediction.Domain.Base.Interfaces.Data;

namespace WeatherPrediction.Crosscutting.Mapping
{
    public class AutoMapperBase<TDomain> : IMappingService<TDomain>
    {
        private readonly IMapper _maper;
        public AutoMapperBase(IMapper mapper)
        {
            _maper = mapper;
        }

        public TDomain ConvertToDomain<T>(T type)
        {
            return _maper.Map<T, TDomain>(type);
        }

        public T ConvertFromDomain<T>(TDomain domain)
        {
            return _maper.Map<TDomain, T>(domain);
        }

        public TOther ConvertOther<T, TOther>(T domain)
        {
            return _maper.Map<T, TOther>(domain);
        }

        public void UpdateDomain<T>(TDomain destination, T source)
        {
            _maper.Map(source, destination);
        }

        public List<T> ConvertFromDomain<T>(List<TDomain> domains)
        {
            return _maper.Map<List<TDomain>, List<T>>(domains);
        }

        public List<TDomain> ConvertToDomain<T>(List<T> types)
        {
            return _maper.Map<List<T>, List<TDomain>>(types);
        }

        public List<TOther> ConvertOther<T, TOther>(List<T> domain)
        {
            return _maper.Map<List<T>, List<TOther>>(domain);
        }
    }
}