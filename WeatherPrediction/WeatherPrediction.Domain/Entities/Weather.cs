using WeatherPrediction.Domain.Base;

namespace WeatherPrediction.Domain.Entities
{
    public class Weather : EntityBase<int>
    {
        public string Name { get; set; }
        protected override void InsertValidate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                AddBrokenRule(new BusinessRule("Name não pode ser vazio ou nulo.", nameof(Name)));
        }

        protected override void UpdateValidate() => InsertValidate();
    }
}
