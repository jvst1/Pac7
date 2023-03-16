using WeatherPrediction.Infrastructure.Extensions;

namespace WeatherPrediction.Infrastructure.Attributes
{
    /// <inheritdoc />
    /// <summary>
    /// Atributo usado para marcar propriedades como Truncate. 
    /// Atributo necessário para o funcionamento do método <see cref="Apply"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class Truncate : Attribute
    {
        public int MaxLenght { get; }

        public Truncate(int maxLenght)
        {
            MaxLenght = maxLenght;
        }

        /// <summary>
        /// Método para aplicar as alterações de caixa nas propriedades marcadas como <see cref="Truncate"/> de um objeto.
        /// </summary>
        /// <param name="value">>O valor da propriedade para alteração.</param>
        /// 
        public string Apply(string value)
        {
            return value.Truncate(MaxLenght);
        }
    }
}