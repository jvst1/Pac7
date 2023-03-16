namespace WeatherPrediction.Infrastructure.Attributes
{
    /// <inheritdoc />
    /// <summary>
    /// Atributo para evitar que propriedades sejam atualizadas automaticamente atraves dos métodos de
    /// Update dos Application Services. 
    /// Desta forma caso um objeto venha como nulo, mantém-se o valor anterior, pois não é
    /// atualizado automaticamente. 
    /// Para alterar uma propriedade marcada com este atributo, deve-se atribuir o valor manualmente
    /// e então chamar os métodos do repositório. 
    /// Usualmente utilizado para campos que são atribuidos apenas na inserção. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class AutomaticUpdateIgnore : Attribute
    {
    }
}