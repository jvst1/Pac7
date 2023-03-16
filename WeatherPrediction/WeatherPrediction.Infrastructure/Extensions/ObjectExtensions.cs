using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WeatherPrediction.Infrastructure.Attributes;

namespace WeatherPrediction.Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static void UpdateAllProperties<T>(this T currentEntity, T newEntity, bool automaticUpdateIgnore = true)
        {
            var currentEntityProperties = currentEntity.GetType().GetProperties();
            var newEntityProperties = newEntity.GetType().GetProperties();

            for (var i = 0; i < currentEntityProperties.Length; i++)
            {
                var propAttrs = currentEntityProperties[i].GetCustomAttributes();
                if (automaticUpdateIgnore && propAttrs.Any(a => a.GetType() == typeof(AutomaticUpdateIgnore))) continue;

                if (currentEntityProperties[i].GetSetMethod() != null)
                    currentEntityProperties[i].SetValue(
                        currentEntity, newEntityProperties[i].GetValue(newEntity, null), null);
            }
        }

        public static void UpdateAllPropertiesOnlyIfNull<T>(this T currentEntity, T newEntity, bool automaticUpdateIgnore = true)
        {
            var currentEntityProperties = currentEntity.GetType().GetProperties();
            var newEntityProperties = newEntity.GetType().GetProperties();

            for (var i = 0; i < currentEntityProperties.Length; i++)
            {
                var propAttrs = currentEntityProperties[i].GetCustomAttributes();
                if (automaticUpdateIgnore && propAttrs.Any(a => a.GetType() == typeof(AutomaticUpdateIgnore))) continue;

                if (currentEntityProperties[i].GetSetMethod() != null && currentEntityProperties[i].GetValue(currentEntity) == null)
                    currentEntityProperties[i].SetValue(
                        currentEntity, newEntityProperties[i].GetValue(newEntity, null), null);
            }
        }

        public static bool IsBetween<T>(this T item, T start, T end)
        {
            return Comparer<T>.Default.Compare(item, start) >= 0
                   && Comparer<T>.Default.Compare(item, end) <= 0;
        }
    }
}