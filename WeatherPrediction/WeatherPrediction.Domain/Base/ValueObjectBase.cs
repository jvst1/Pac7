using System.Text;

namespace WeatherPrediction.Domain.Base
{
    public abstract class ValueObjectBase
    {
        private readonly List<BusinessRule> _brokenRules = new List<BusinessRule>();

        protected abstract void Validate();

        public void ThrowExceptionIfInvalid()
        {
            _brokenRules.Clear();
            Validate();
            if (!_brokenRules.Any()) return;

            var issues = new StringBuilder();
            foreach (var businessRule in _brokenRules)
            {
                issues.AppendLine(businessRule.RuleDescription);
            }

            throw new ValueObjectIsInvalidException(issues.ToString());
        }

        protected void AddBrokenRule(BusinessRule businessRule)
        {
            _brokenRules.Add(businessRule);
        }
    }
}