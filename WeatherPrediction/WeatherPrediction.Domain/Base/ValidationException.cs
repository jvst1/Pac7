using System.Text;
using WeatherPrediction.Infrastructure.Exceptions;

namespace WeatherPrediction.Domain.Base
{
    public class ValidationException : BaseException
    {
        public List<BusinessRule> BrokenRules { get; set; }

        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(List<BusinessRule> brokenRules) : base(BuildBrokenRulesMessage(brokenRules))
        {
            BrokenRules = brokenRules;
        }

        private static string BuildBrokenRulesMessage(IEnumerable<BusinessRule> brokenRules)
        {
            var brokenRulesBuilder = new StringBuilder();
            foreach (var businessRule in brokenRules)
            {
                brokenRulesBuilder.AppendLine(businessRule.RuleDescription);
            }
            return brokenRulesBuilder.ToString();
        }
    }

    public static class GenericExceptions
    {
        public static readonly ValidationException AlreadyRegistered = new ValidationException("Registro já cadastrado.");
        public static readonly ValidationException InvalidCode = new ValidationException("Código invalido para entidade.");
        public static readonly ValidationException InvalidPassword = new ValidationException("Senha inválida.");
        public static readonly ValidationException Generic = new ValidationException("Falha ao processar requisição.");
        public static readonly ValidationException NotFound = new ValidationException("Registro não encontrado.");
    }
}