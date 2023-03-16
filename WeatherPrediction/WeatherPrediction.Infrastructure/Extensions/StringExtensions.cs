namespace WeatherPrediction.Infrastructure.Extensions
{
    public static class StringExtensions
	{
		public static string Truncate(this string value, int maxLength)
		{
			if (string.IsNullOrEmpty(value)) return value;
			return value.Length <= maxLength ? value : value.Substring(0, maxLength);
		}

		public static bool IsNullOrEmpty(this string s)
		{
			return string.IsNullOrEmpty(s);
		}

		public static bool IsNullOrWhiteSpace(this string s)
		{
			return string.IsNullOrWhiteSpace(s);
		}
		public static string ToCamelCase(this string value)
		{
			if (string.IsNullOrEmpty(value)) return value;
			return char.ToLowerInvariant(value[0]) + value.Substring(1);
		}
	}
}
