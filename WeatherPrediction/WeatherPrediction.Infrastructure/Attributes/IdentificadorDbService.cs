using WeatherPrediction.Infrastructure.Enums;

namespace WeatherPrediction.Infrastructure.Attributes
{
    public class IdentificadorDbService : Attribute
    {
        public string Identificador { get; }

        public OrmEngine OrmEngine { get; }

        public IdentificadorDbService(string identificador, OrmEngine ormEngine)
        {
            Identificador = identificador;
            OrmEngine = ormEngine;
        }
    }
}