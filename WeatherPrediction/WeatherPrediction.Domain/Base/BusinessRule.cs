namespace WeatherPrediction.Domain.Base
{
    public class BusinessRule
    {
        public BusinessRule()
        {
        }

        public BusinessRule(string ruleDescription, string propertyName)
        {
            RuleDescription = ruleDescription;
            PropertyName = propertyName;
        }

        public string RuleDescription { get; set; }
        public string PropertyName { get; set; }
    }
}