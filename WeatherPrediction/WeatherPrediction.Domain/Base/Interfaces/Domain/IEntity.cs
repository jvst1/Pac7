using System;

namespace WeatherPrediction.Domain.Base.Interfaces.Domain
{
    public interface IEntity : IEntityBase
    {
        Guid Codigo { get; set; }
    }
}