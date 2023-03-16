namespace WeatherPrediction.Infrastructure.Attributes
{
    /// <inheritdoc />
    /// <summary>
    /// Atributo usado para marcar propriedades como ForceCase. 
    /// Atributo necessário para o funcionamento do método <see cref="Apply"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class ForceCase : Attribute
    {
        private static readonly Dictionary<CaseConversionType, Func<string, string>> Conversions =
            new Dictionary<CaseConversionType, Func<string, string>>
            {
                {CaseConversionType.Upper, str => str.ToUpperInvariant()},
                {CaseConversionType.Lower, str => str.ToLowerInvariant()}
            };

        public CaseConversionType ConversionType { get; }

        public ForceCase(CaseConversionType conversionType)
        {
            ConversionType = conversionType;
        }

        /// <summary>
        /// Método para aplicar as alterações de caixa nas propriedades marcadas como <see cref="ForceCase"/> de um objeto.
        /// </summary>
        /// <param name="value">O valor da propriedade para alteração.</param>
        public string Apply(string value)
        {
            return Conversions[ConversionType](value);
        }
    }

    /// <summary>
    /// Especifica o tipo de conversão de caixa a se aplicar.
    /// </summary>
    public enum CaseConversionType
    {
        /// <summary>
        /// Realiza conversão para caixa alta.
        /// </summary>
        Upper,
        /// <summary>
        /// Realiza conversão para caixa baixa.
        /// </summary>
        Lower
    }
}