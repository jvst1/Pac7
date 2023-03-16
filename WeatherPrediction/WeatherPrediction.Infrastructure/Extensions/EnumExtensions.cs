using System.ComponentModel;

namespace WeatherPrediction.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static string GetValueAsString<T>(this T enumerationValue)
            where T : Enum
        {
            return enumerationValue.ToString("D");
        }

        public static string EnumToJson<T>(Type valueType = null) where T : Enum
        {
            if (valueType == null)
                valueType = typeof(int);
            Type type = typeof(T);
            if (!type.IsEnum)
                throw new InvalidOperationException("enum expected");

            var results =
                Enum.GetValues(type).Cast<object>()
                    .Select(p => new { name = p.ToString(), value = Convert.ChangeType(p, valueType), description = GetDescription<T>(p.ToString()) });

            return string.Format("{{ \"{0}\" : {1} }}", type.Name, Newtonsoft.Json.JsonConvert.SerializeObject(results));
        }

        public static string GetDescription<T>(string name)
            where T : Enum
        {
            Type type = typeof(T);
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", nameof(type));
            }

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            var memberInfo = type.GetMember(name);
            if (memberInfo.Length <= 0) return type.ToString();
            var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attrs.Length > 0 ? ((DescriptionAttribute)attrs[0]).Description : type.ToString();
            //If we have no description attribute, just return the ToString of the enum
        }
    }
}