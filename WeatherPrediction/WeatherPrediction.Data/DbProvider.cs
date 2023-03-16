using System.Collections.Concurrent;
using System.Reflection;
using WeatherPrediction.Domain.Base;
using WeatherPrediction.Domain.Base.Interfaces.Data;
using WeatherPrediction.Infrastructure.Attributes;
using WeatherPrediction.Infrastructure.Enums;

namespace WeatherPrediction.Data
{
    public class DbProvider : IDbProvider
    {
        private static readonly ConcurrentDictionary<Guid, IDbMapping> LoadedDbMappings = new ConcurrentDictionary<Guid, IDbMapping>();
        private static readonly ConcurrentDictionary<Guid, Type> LoadedDbServices = new ConcurrentDictionary<Guid, Type>();
        private readonly IServiceProvider _serviceProvider;

        public DbProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDbService GetDbService(Type type, OrmEngine ormEngine)
        {
            var tipoGenericoGuid = type.GUID;

            Type dbServiceType;
            if (LoadedDbServices.ContainsKey(tipoGenericoGuid))
            {
                var loaded = LoadedDbServices[tipoGenericoGuid];
                dbServiceType = loaded;
            }
            else
            {
                var foundType = FindDbServiceType(type, ormEngine);
                LoadedDbServices[tipoGenericoGuid] = foundType;
                dbServiceType = foundType;
            }

            return (IDbService)_serviceProvider.GetService(dbServiceType);
        }

        public IDbService GetDbService<T>(OrmEngine ormEngine)
        {
            return GetDbService(typeof(T), ormEngine);
        }

        public static IDbMapping FindDbMapping(Type type)
        {
            if (LoadedDbServices.ContainsKey(type.GUID))
            {
                return LoadedDbMappings[type.GUID];
            }

            if (typeof(EntityBase<>).IsAssignableFrom(type))
                throw new ArgumentException("Tipo genérico não é uma entidade");

            var assemblyTypes = Assembly.GetExecutingAssembly().GetTypes();
            var dbMappingType = typeof(IDbMapping<>);

            var mappingTypes =
                (from x in assemblyTypes
                 from z in x.GetInterfaces()
                 let y = x.BaseType
                 where
                     y != null && y.IsGenericType &&
                      dbMappingType.IsAssignableFrom(y.GetGenericTypeDefinition()) &&
                      y.GenericTypeArguments[0] == type ||
                     z.IsGenericType &&
                      dbMappingType.IsAssignableFrom(z.GetGenericTypeDefinition()) &&
                      z.GenericTypeArguments[0] == type
                 select x).ToList();

            if (mappingTypes.Count != 1)
                throw new ArgumentException("Mais de um ou nenhum mapeamento foi encontrado para o tipo informado.");

            var mappingType = mappingTypes[0];

            dynamic mappingObj = Activator.CreateInstance(mappingType);

            LoadedDbMappings[type.GUID] = mappingObj;

            return mappingObj;
        }

        private static Type FindDbServiceType(Type tipoGenerico, OrmEngine ormEngine)
        {
            var assemblyTypes = Assembly.GetExecutingAssembly().GetTypes();

            var mappingObj = FindDbMapping(tipoGenerico);
            var dbService = FindDbService(mappingObj, assemblyTypes, ormEngine);

            return dbService;
        }

        private static Type FindDbService(IDbMappingBase mappingObj, Type[] assemblyTypes, OrmEngine ormEngine)
        {
            var identificadorDbService = mappingObj.DbService;
            var dbServices = (from x in assemblyTypes
                              let identificadorDbServiceAttribute =
                                  x.GetCustomAttribute(typeof(IdentificadorDbService)) as IdentificadorDbService
                              where x.IsClass &&
                                    typeof(IDbService).IsAssignableFrom(x) &&
                                    identificadorDbServiceAttribute?.Identificador == identificadorDbService &&
                                    identificadorDbServiceAttribute?.OrmEngine == ormEngine
                              select x).ToList();

            if (dbServices.Count != 1)
                throw new ArgumentException("Mais de um ou nenhum DbService foi encontrado para o tipo informado.");

            var dbService = dbServices[0];
            return dbService;
        }
    }
}